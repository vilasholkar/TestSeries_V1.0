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
        public ForgetPassword ForgetPassword(ForgetPassword objForgetPassword)
        {
            string MobileNo;
            if (objForgetPassword.Action == "SendOTP")
            {
                string strQuery = string.Format("Select MobileNumber from Student where EnrollmentNo='{0}'", objForgetPassword.UserName);
                MobileNo = Convert.ToString(DGeneric.GetValue(strQuery));
                

                if (string.IsNullOrEmpty(MobileNo))
                {
                    MobileNo = Convert.ToString(DGeneric.GetValue(String.Format("select Mobile from Users where UserName='{0}'", objForgetPassword.UserName)));
                    if (string.IsNullOrEmpty(MobileNo))
                    {
                        objForgetPassword.ConfirmOTP = "0";

                    }
                    else
                    {
                        Random rand = new Random();
                        objForgetPassword.ConfirmOTP = rand.Next(0, 999999).ToString("D6");
                        string strMessage = "Your OTP is: " + objForgetPassword.ConfirmOTP;
                        string response = DSMSGeneric.SendSingleSMS("8871171445", strMessage);
                        objForgetPassword.UserTypeID = 1;
                    }
                }
                else
                {
                    Random rand = new Random();
                    objForgetPassword.ConfirmOTP = rand.Next(0, 999999).ToString("D6");
                    string strMessage = "Your OTP is: " + objForgetPassword.ConfirmOTP;
                    string response = DSMSGeneric.SendSingleSMS("8871171445", strMessage);
                    objForgetPassword.UserTypeID = 2;
                }


            }
            else if (objForgetPassword.Action == "UpdatePassword")
            {
                if(objForgetPassword.UserTypeID==1)
                {
                    string strQuery = string.Format("update users set UserPassword='{0}' where UserName='{1}'", objForgetPassword.NewPassword, objForgetPassword.UserName);
                    DGeneric.ExecQuery(strQuery);
                    objForgetPassword.Status = "Success";
                    
                }
                else if (objForgetPassword.UserTypeID == 2) {
                    int StudentID = Convert.ToInt32(DGeneric.GetValue(String.Format("select StudentID from Student where EnrollmentNo='{0}'", objForgetPassword.UserName)));
                    string strQuery = string.Format("update StudentAccount set Password='{0}' where StudentID='{1}'", objForgetPassword.NewPassword, StudentID);
                    DGeneric.ExecQuery(strQuery);
                    objForgetPassword.Status = "Success";

                }
                
            }
            return objForgetPassword;
        }

    }
}
