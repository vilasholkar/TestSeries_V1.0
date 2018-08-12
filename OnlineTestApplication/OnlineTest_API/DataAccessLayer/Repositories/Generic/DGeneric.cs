using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DGeneric
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        public static async Task<object> RunSP_ReturnScaler(string procedureName, List<SqlParameter> parameters)
        {
            SqlTransaction trans = null;
            try
            {
                using (var sqlConn = new SqlConnection(connectionString))
                {
                    await sqlConn.OpenAsync();
                    using (trans = sqlConn.BeginTransaction())
                    {
                        using (var sqlCommand = new SqlCommand(procedureName, sqlConn))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            if (parameters != null)
                            {
                                sqlCommand.Parameters.AddRange(parameters.ToArray());
                            }
                            try
                            {
                                var returnValue = await sqlCommand.ExecuteScalarAsync();
                                trans.Commit();
                                return returnValue;
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                                throw;
                            }
                            finally
                            {
                                sqlConn.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static async Task<bool> RunSP_ExecuteNonQuery(string procedureName, List<SqlParameter> parameters)
        {
            SqlTransaction trans = null;
            object storeCount = string.Empty;
            try
            {
                using (var sqlConn = new SqlConnection(connectionString))
                {
                    await sqlConn.OpenAsync();
                    using (trans = sqlConn.BeginTransaction())
                    {
                        using (var sqlCommand = new SqlCommand(procedureName, sqlConn))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            if (parameters != null)
                            {
                                sqlCommand.Parameters.AddRange(parameters.ToArray());
                            }
                            try
                            {
                                await sqlCommand.ExecuteNonQueryAsync();
                                trans.Commit();
                                return true;
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                                throw;
                            }
                            finally
                            {
                                sqlConn.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static async Task<DataSet> RunSP_ReturnDataSet(string procedureName, List<SqlParameter> parameters, List<DataTableMapping> dataTableMappingList)
        {
            SqlTransaction trans = null;
            return await Task.Run(() =>
            {
                try
                {
                    DataSet dsData = new DataSet();
                    using (SqlConnection sqlConn = new SqlConnection(connectionString))
                    {
                        using (trans = sqlConn.BeginTransaction())
                        {
                            using (SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConn))
                            {
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                if (parameters != null)
                                    sqlCommand.Parameters.AddRange(parameters.ToArray());
                                try
                                {
                                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                                    {
                                        // Create a DataTableMapping object
                                        //List<DataTableMapping> dataTableMappingList = new List<DataTableMapping>();
                                        //dataTableMappingList.Add(new DataTableMapping("Table1", "TestType"));
                                        //dataTableMappingList.Add(new DataTableMapping("Table2", "Batch"));
                                        DataTableMapping[] dataTableMappingArray = dataTableMappingList.ToArray();
                                        // Call DataAdapter's TableMappings.Add method                       
                                        sqlDataAdapter.TableMappings.AddRange(dataTableMappingArray);
                                        sqlDataAdapter.Fill(dsData);
                                        trans.Commit();
                                    }
                                }
                                catch (Exception)
                                {
                                    trans.Rollback();
                                    throw;
                                }
                            }
                        }
                    }
                    return dsData;
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}
