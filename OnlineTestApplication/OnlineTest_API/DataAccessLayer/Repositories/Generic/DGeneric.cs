using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DGeneric
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        public async Task<object> RunSP_ReturnScaler(string procedureName, List<SqlParameter> parameters)
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

        public async Task<bool> RunSP_ExecuteNonQuery(string procedureName, List<SqlParameter> parameters)
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
    }
}
