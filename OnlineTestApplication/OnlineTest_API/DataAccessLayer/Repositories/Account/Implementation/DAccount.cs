using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ViewModels.Account;

namespace DataAccessLayer
{
    public class DAccount : IDAccount
    {
        public Login GetUserDetails(Login user)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@UserName", user.UserName));
            parameter.Add(new SqlParameter("@Password", user.UserPassword));
            parameter.Add(new SqlParameter("@UserTypeID", user.UserTypeID));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_Login", parameter, null).Tables[0];

            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<Login>(dt).FirstOrDefault();
            else
                return new Login();
            //Login Userlist = null;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Userlist = new Login()
            //    {
            //        UserID = Convert.ToInt32(dr["UserID"]),
            //        FirstName = dr["FirstName"].ToString(),
            //        LastName = dr["LastName"].ToString(),
            //        MobileNo = dr["Mobile"].ToString(),
            //        UserName = dr["UserName"].ToString(),
            //        //  UserPassword = Convert.ToString(dr["UserPassword"]),
            //        UserTypeID = Convert.ToInt32(dr["UserTypeID"]),
            //        UserType = Convert.ToString(dr["UserType"])
            //    };
            //}
            //return Userlist;
        }

        public static Login GetUserDetails1(Login user)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@UserName", user.UserName));
            parameter.Add(new SqlParameter("@Password", user.UserPassword));
            parameter.Add(new SqlParameter("@UserTypeID", user.UserTypeID));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_Login", parameter, null).Tables[0];

            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<Login>(dt).FirstOrDefault();
            else
                return new Login();

            //Login Userlist = null;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Userlist = new Login()
            //    {
            //        UserID = Convert.ToInt32(dr["UserID"]),
            //        FirstName = dr["FirstName"].ToString(),
            //        LastName = dr["LastName"].ToString(),
            //        MobileNo = dr["Mobile"].ToString(),
            //        UserName = dr["UserName"].ToString(),
            //      //  UserPassword = Convert.ToString(dr["UserPassword"]),
            //        UserTypeID = Convert.ToInt32(dr["UserTypeID"]),
            //        UserType = Convert.ToString(dr["UserType"])
            //    };
            //}
            //return Userlist;
        }
    }
}
