using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public sealed class DLog : IDLog
    {
        //Lazy initilization for DLog object for thread safety
        private static readonly Lazy<DLog> instance = new Lazy<DLog>(() => new DLog());
        private DLog() { }

        public static DLog GetInsatance
        {
            get
            {
                return instance.Value;
            }
        }

        public void LogException(string methodName, string exceptionMessage, string stackTrace, string userName)
        {
            var sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@MethodName", methodName));
            sqlParameterList.Add(new SqlParameter("@ErrorMessage", exceptionMessage));
            sqlParameterList.Add(new SqlParameter("@StackTrace", stackTrace));
            sqlParameterList.Add(new SqlParameter("@UserName", userName));
            sqlParameterList.Add(new SqlParameter("@CreateDate", DateTime.Now));
          string response =  DGeneric.RunSP_ExecuteNonQuery("sp_AddElmahError", sqlParameterList);
        }
    }
}
