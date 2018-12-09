using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ViewModels;

namespace DataAccessLayer
{
    public static class DGeneric
    {
        private static readonly string CnnStrVariableName = "Cnnstr";
        private static string _ConnectionString = string.Empty;
        public static DateTime SystemDateTime = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.CurrentCulture);

        public static string ConnectionString
        {
            get
            {
                if (_ConnectionString.Trim().Length == 0)
                {
                    _ConnectionString = ConfigurationManager.ConnectionStrings[CnnStrVariableName].ConnectionString;
                }
                return DGeneric._ConnectionString;
            }
            set
            {
                DGeneric._ConnectionString = value;
            }
        }
        public static async Task<object> RunSP_ReturnScalerAsync(string procedureName, List<SqlParameter> parameters)
        {
            SqlTransaction trans = null;
            try
            {
                using (var sqlConn = new SqlConnection(ConnectionString))
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
        public static string RunSP_ExecuteNonQuery(string procedureName, List<SqlParameter> parameters)
        {
            SqlTransaction trans = null;
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
                {
                    sqlConn.Open();
                    using (trans = sqlConn.BeginTransaction())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConn, trans))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            if (parameters != null)
                            {
                                sqlCommand.Parameters.AddRange(parameters.ToArray());
                            }
                            try
                            {
                                sqlCommand.ExecuteNonQuery();
                                trans.Commit();
                                return CommonEnum.Status.Success.ToString();
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                return ex.Message;
                            }
                            finally
                            {
                                sqlConn.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public static async Task<string> RunSP_ExecuteNonQueryAsync(string procedureName, List<SqlParameter> parameters)
        {
            SqlTransaction trans = null;
            object storeCount = string.Empty;
            try
            {
                using (var sqlConn = new SqlConnection(ConnectionString))
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
                                return "Success";
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                return ex.Message;
                            }
                            finally
                            {
                                sqlConn.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public static async Task<DataSet> RunSP_ReturnDataSetAsync(string procedureName, List<SqlParameter> parameters, List<DataTableMapping> dataTableMappingList)
        {
            SqlTransaction trans = null;
            return await Task.Run(() =>
            {
                try
                {
                    DataSet dsData = new DataSet();
                    using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
                    {
                        sqlConn.OpenAsync();
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
                                catch (Exception ex)
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
                    return dsData;
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }
        public static DataSet RunSP_ReturnDataSet(string procedureName, List<SqlParameter> parameters, List<DataTableMapping> dataTableMappingList)
        {
            DataSet dtData = new DataSet();
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters.ToArray());
                    }
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        if (dataTableMappingList != null)
                        {
                            DataTableMapping[] dataTableMappingArray = dataTableMappingList.ToArray();
                            // Call DataAdapter's TableMappings.Add method                       
                            sqlDataAdapter.TableMappings.AddRange(dataTableMappingArray);
                        }
                        sqlDataAdapter.Fill(dtData);
                    }
                }
            }
            return dtData;
        }
        public static List<T> BindDataList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }
        public static string ExtractParameterFromQueryString(string queryString)
        {
            Uri myUri = new Uri(queryString);
            return HttpUtility.ParseQueryString(myUri.Query).Get(0);
        }
        public static object GetNewId(string tableName, string columnName)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string query = "SELECT  ISNULL(MAX(" + columnName + "),0)+1 FROM " + tableName + "";
                using (SqlCommand sqlCommand = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        sqlCommand.CommandType = CommandType.Text;
                        return sqlCommand.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                    finally
                    {
                        con.Close();
                        sqlCommand.Dispose();
                    }
                }
            }
        }
        public static DateTime ConvertDateTimeToDate(this object date)
        {
            return DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", CultureInfo.CurrentCulture);
        }

        public static object GetValue(string query)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            string str = DGeneric.ConnectionString;
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            sqlCommand.Connection = cnn;

            try
            {
            object obj = sqlCommand.ExecuteScalar();
            cnn.Close();
            sqlCommand.Dispose();
            return obj;
            }
            catch (Exception ex)
            {
                cnn.Close();
                sqlCommand.Dispose();
                throw new Exception("DGeneric::GetValue::Error occured.", ex.InnerException);
            }

        }
        public static DataSet GetData(string query)
        {
            SqlConnection cnn = new SqlConnection(DGeneric.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            //sqlCommand.CommandType = CommandType.StoredProcedure;
            //cnn.Open();
            sqlCommand.Connection = cnn;
            try
            {
            SqlDataAdapter objDa = new SqlDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            objDa.Fill(ds);
            //cnn.Close();
            sqlCommand.Dispose();
            return ds;
            }
            catch (Exception ex)
            {
                cnn.Close();
                sqlCommand.Dispose();
                throw new Exception("DGeneric::GetData::Error occured.", ex.InnerException);
            }

        }
    }
}
