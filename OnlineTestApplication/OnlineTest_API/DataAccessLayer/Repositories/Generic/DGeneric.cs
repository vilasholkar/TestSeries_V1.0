using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
        public static string RunSP_ReturnScaler(string procedureName, List<SqlParameter> parameters)
        {
            SqlTransaction trans = null;
            try
            {
                using (var sqlConn = new SqlConnection(ConnectionString))
                {
                    sqlConn.Open();
                    using (trans = sqlConn.BeginTransaction())
                    {
                        using (var sqlCommand = new SqlCommand(procedureName, sqlConn, trans))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            if (parameters != null)
                            {
                                sqlCommand.Parameters.AddRange(parameters.ToArray());
                            }
                            try
                            {
                                var returnValue = sqlCommand.ExecuteScalar();
                                trans.Commit();
                                return returnValue.ToString();
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                return ex.Message;
                            }
                            finally
                            {
                                sqlConn.Close();
                                sqlCommand.Dispose();
                                sqlConn.Dispose();
                                trans.Dispose();
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
                                sqlCommand.Dispose();
                                sqlConn.Dispose();
                                trans.Dispose();
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
            try
            {
                using (var sqlConn = new SqlConnection(ConnectionString))
                {
                    await sqlConn.OpenAsync();
                    using (trans = sqlConn.BeginTransaction())
                    {
                        using (var sqlCommand = new SqlCommand(procedureName, sqlConn, trans))
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
                                finally
                                {
                                    sqlConn.Close();
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
        public static DataSet RunSP_ReturnDataSet(string procedureName, List<SqlParameter> parameters, IList<DataTableMapping> dataTableMappingList)
        {
            DataSet dtData = new DataSet();
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (parameters != null && parameters.Count > 0)
                    {
                        sqlCommand.Parameters.AddRange(parameters.ToArray());
                    }
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        if (dataTableMappingList != null && dataTableMappingList.Count > 0)
                        {
                            DataTableMapping[] dataTableMappingArray = dataTableMappingList.ToArray();
                            // Call DataAdapter's TableMappings.Add method                       
                            sqlDataAdapter.TableMappings.AddRange(dataTableMappingArray);
                        }
                        sqlDataAdapter.Fill(dtData);
                    }
                    sqlCommand.Dispose();
                    sqlConn.Dispose();
                }
            }
            return dtData;
        }
        public static List<T> BindDataList<T>(DataTable dt) where T : class
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                // Create object
                var objT = Activator.CreateInstance<T>();

                // Get all properties
 
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) {  }
                    }
                }
                return objT;
            }).ToList();
        }

        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
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
            return DateTime.ParseExact(date.ToString(), "yyyy-MM-dd", CultureInfo.CurrentCulture);
        }
        public static string ConvertDateTimeToString(this string date)
        {
            if (!string.IsNullOrEmpty(date) && !string.IsNullOrWhiteSpace(date))
            {
                return Convert.ToDateTime(date).ToString("dd/MM/yyyy");
                //return Convert.ToDateTime(date).ToShortDateString();
            }
            else
            {
                return string.Empty;
            }
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
                return obj;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
                sqlCommand.Dispose();
                cnn.Dispose();
            }

        }
        public static DataSet GetData(string query)
        {
            SqlConnection cnn = new SqlConnection(DGeneric.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            sqlCommand.Connection = cnn;
            try
            {
                SqlDataAdapter objDa = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                objDa.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
                sqlCommand.Dispose();
                cnn.Dispose();
            }
        }
        public static bool ExecQuery(string query)
        {
            using (SqlConnection cnn = new SqlConnection(DGeneric.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = query;
                cnn.Open();
                sqlCommand.Connection = cnn;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    cnn.Close();
                    sqlCommand.Dispose();
                }
            }
        }


        public static string Add_And(string str1, string str2)
        {
            string result = string.Empty;
            if (str1.Trim().Length > 0)
            {
                result = str1;
            }
            if (str2.Trim().Length > 0)
            {
                if (result.Trim().Length > 0)
                {
                    result = result + " and " + str2;

                }
                else
                {
                    result = str2;
                }
            }
            return result;
        }
        public static string ConvertToSql(this object strDate)
        {
            //string strDay, strMonth, strYear;
            //DateTime datetime = DateTime.ParseExact(strDate.ToString(), "dd/MM/yyyy", CultureInfo.CurrentCulture);
            //string strmonthName = datetime.ToString("MMM", CultureInfo.InvariantCulture).ToUpper();
            //strDay = datetime.Day.ToString().PadLeft(2, '0');
            //strMonth = datetime.Month.ToString().PadLeft(2, '0');
            //strYear = datetime.Year.ToString();
            //String finalDate = strYear + "-" + strMonth + "-" + strDay;
            //return finalDate;
            string strDay, strMonth, strYear, finalDate = string.Empty;
            DateTime datetime;
            if (DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out datetime))
            {
                string strmonthName = datetime.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                strDay = datetime.Day.ToString().PadLeft(2, '0');
                strMonth = datetime.Month.ToString().PadLeft(2, '0');
                strYear = datetime.Year.ToString();
                finalDate = strYear + "-" + strMonth + "-" + strDay;
            }
            if (DateTime.TryParseExact(strDate.ToString(), "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out datetime))
            {
                string strmonthName = datetime.ToString("MMM", CultureInfo.CurrentCulture).ToUpper();
                strDay = datetime.Day.ToString().PadLeft(2, '0');
                strMonth = datetime.Month.ToString().PadLeft(2, '0');
                strYear = datetime.Year.ToString();
                finalDate = strYear + "-" + strMonth + "-" + strDay;
            }
            return finalDate;
        }
        public static string Add_Or(string str1, string str2)
        {
            string result = string.Empty;
            if (str1.Trim().Length > 0)
            {
                result = str1;
            }
            if (str2.Trim().Length > 0)
            {
                if (result.Trim().Length > 0)
                {
                    result = "(" + result + " or " + str2 + ")";

                }
                else
                {
                    result = str2;
                }
            }
            return result;
        }
        public static string MonthStartDate(string strDate)
        {
            string strDay, strMonth, strYear;
            DateTime datetime = DateTime.ParseExact(strDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            strDay = "01";
            strMonth = datetime.Month.ToString().PadLeft(2, '0');
            strYear = datetime.Year.ToString();
            String finalDate = strDay + "/" + strMonth + "/" + strYear;
            return finalDate;
        }
        public static string AddOneDay(string strDate)
        {
            DateTime datetime;
            String finalDate = string.Empty;
            if (DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out datetime))
            {
                finalDate = datetime.AddDays(1).ToShortDateString();
            }
            if (DateTime.TryParseExact(strDate.ToString(), "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out datetime))
            {
                finalDate = datetime.AddDays(1).ToShortDateString();
            }
            return finalDate;
        }

        //public static List<T> BindDataList1<T>(DataTable dt)
        //{
        //    var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
        //    var properties = typeof(T).GetProperties();
        //    return dt.AsEnumerable().Select(row =>
        //    {
        //        var objT = Activator.CreateInstance<T>();
        //        foreach (var pro in properties)
        //        {
        //            if (columnNames.Contains(pro.Name.ToLower()))
        //            {
        //                try
        //                {
        //                    if (pro.PropertyType.IsArray)
        //                    {
        //                        var values = (IEnumerable)pro.GetValue(dt, null);
        //                        var elementType = pro.PropertyType.GetElementType();
        //                        if (elementType != null && !elementType.IsValueType)
        //                        {
        //                            var stringValue = string.Join(", ", (object[])values);
        //                        }
        //                        else
        //                        {
        //                            var stringValue = string.Join(", ", values.OfType<object>());
        //                        }
        //                    }
        //                    else
        //                    pro.SetValue(objT, row[pro.Name]);
        //                }
        //                catch (Exception) { throw; }
        //            }
        //        }
        //        return objT;
        //    }).ToList();
        //}

    }
}
