using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Test;

namespace DataAccessLayer
{
    public class DEligibleStudentcs : IDEligibleStudent
    {
        public List<EligibleStudentViewModel> GetEligibleStudent(int OnlineTestID)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@OnlineTestID", OnlineTestID));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetEligibleStudent", sqlParameterList, null).Tables[0];
            if (dt.Rows.Count > 0)
                //return DGeneric.BindDataList<EligibleStudentViewModel>(dt);
                return dt.AsEnumerable().Select(s => new EligibleStudentViewModel()
                {
                    StudentID = Convert.ToInt32(s["StudentID"]),
                    OnlineTestID = Convert.ToInt32(s["OnlineTestID"]),
                    EnrollmentNo = (s["EnrollmentNo"]).ToString(),
                    StudentName = (s["StudentName"]).ToString(),
                    Gender = (s["Gender"]).ToString(),
                    MobileNumber = (s["MobileNumber"]).ToString(),
                    FatherMobileNo = (s["FatherMobileNo"]).ToString(),
                    IsEligible = Convert.ToBoolean((s["IsEligible"])),
                }).ToList();
            else
                return new List<EligibleStudentViewModel>();
           
        }

        public string AddEligibleStudent(List<EligibleStudentViewModel> EligibleStudentData)
        {
            var dt = new DataTable();
            dt.Columns.Add("OnlineTestID", typeof(int));
            dt.Columns.Add("StudentID", typeof(int));
            dt.Columns.Add("TestStatusID", typeof(int));
            foreach (var item in EligibleStudentData)
            {
                dt.Rows.Add(item.OnlineTestID, item.StudentID, Convert.ToInt32(item.IsEligible));
            }
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@EligibleStudentDetails", dt));
            return DGeneric.RunSP_ExecuteNonQuery("sp_AddEligibleStudent", sqlParameterList);
        }

        public List<EligibleStudentViewModel> GetEligibleStudentByTestID(int OnlineTestID)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@OnlineTestID", OnlineTestID));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetEligibleStudentByTestID", sqlParameterList, null).Tables[0];
            if (dt.Rows.Count > 0)
                return dt.AsEnumerable().Select(s => new EligibleStudentViewModel()
                {
                    StudentID = Convert.ToInt32(s["StudentID"]),
                    OnlineTestID = Convert.ToInt32(s["OnlineTestID"]),
                    EnrollmentNo = (s["EnrollmentNo"]).ToString(),
                    StudentName = (s["StudentName"]).ToString(),
                    Gender = (s["Gender"]).ToString(),
                    MobileNumber = (s["MobileNumber"]).ToString(),
                    TestStatusID = Convert.ToInt32((s["TestStatusID"])),
                    TestStatus = (s["TestStatus"]).ToString(),
                }).ToList();
            else
                return new List<EligibleStudentViewModel>();
            
        }
        public string UpdateEligibleStudentTestStatus(List<EligibleStudentViewModel> EligibleStudentData)
        {
            
            List<int> StudentList = new List<int>();
            int OnlineTestID=0;
            foreach (var item in EligibleStudentData)
            {
                StudentList.Add(item.StudentID);
                OnlineTestID = item.OnlineTestID;
            }
            int[] terms = StudentList.ToArray();
            string StudentID = string.Join(",", terms);
            string strquery = string.Format(@"UPDATE EligibleStudent SET TestStatusID = {2} WHERE OnlineTestID={0} and StudentID in ({1});",OnlineTestID,StudentID,'1');
            DGeneric.ExecQuery(strquery);
            return "Success";
            //var dt = new DataTable();
            //dt.Columns.Add("OnlineTestID", typeof(int));
            //dt.Columns.Add("StudentID", typeof(int));
            //dt.Columns.Add("TestStatus", typeof(int));
            //foreach (var item in EligibleStudentData)
            //{
            //    dt.Rows.Add(item.OnlineTestID, item.StudentID, Convert.ToInt32(item.IsEligible));
            //}
            //List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            //sqlParameterList.Add(new SqlParameter("@EligibleStudentDetails", dt));
            //return DGeneric.RunSP_ExecuteNonQuery("sp_AddEligibleStudent1", sqlParameterList);
        }

    }
}
