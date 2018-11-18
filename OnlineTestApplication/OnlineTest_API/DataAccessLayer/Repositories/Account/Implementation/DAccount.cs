using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Account;

namespace DataAccessLayer
{
   public class DAccount :IDAccount
    {

       public Login GetUserDetails(Login user)
       {
           List<SqlParameter> parameter = new List<SqlParameter>();
           parameter.Add(new SqlParameter("@UserName", user.UserName));
           parameter.Add(new SqlParameter("@Password", user.UserPassword));
           DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_UserLogin", parameter,null).Tables[0];

           Login Userlist = null;
           foreach (DataRow dr in dt.Rows)
           {
               Userlist = new Login()
               {
                   UserID = Convert.ToInt32(dr["UserID"]),
                   UserName = dr["UserName"].ToString(),
                   UserPassword = Convert.ToString(dr["UserPassword"]),
                   UserType = Convert.ToString(dr["UserType"])
               };
           }
           return Userlist;
       }

       public static Login GetUserDetails1(Login user)
       {
           List<SqlParameter> parameter = new List<SqlParameter>();
           parameter.Add(new SqlParameter("@UserName", user.UserName));
           parameter.Add(new SqlParameter("@Password", user.UserPassword));
           DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_UserLogin", parameter, null).Tables[0];

           Login Userlist = null;
           foreach (DataRow dr in dt.Rows)
           {
               Userlist = new Login()
               {
                   UserID = Convert.ToInt32(dr["UserID"]),
                   UserName = dr["UserName"].ToString(),
                   UserPassword = Convert.ToString(dr["UserPassword"]),
                   UserType = Convert.ToString(dr["UserType"])
               };
           }
           return Userlist;
       }

    }
}
