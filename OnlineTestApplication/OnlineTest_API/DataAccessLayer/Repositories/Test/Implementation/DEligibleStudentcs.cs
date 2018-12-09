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
            try
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("@OnlineTestID", OnlineTestID));
                DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetEligibleStudent", sqlParameterList, null).Tables[0];
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
            }
            catch (Exception)
            {
                return new List<EligibleStudentViewModel>();
            }

        }

        public string AddEligibleStudent(List<EligibleStudentViewModel> EligibleStudentData)
        {
            try
            {
                var dt = new DataTable();
                dt.Columns.Add("OnlineTestID", typeof(int));
                dt.Columns.Add("StudentID", typeof(int));
                dt.Columns.Add("TestStatus", typeof(int));
                foreach (var item in EligibleStudentData)
                {
                    dt.Rows.Add(item.OnlineTestID, item.StudentID, Convert.ToInt32(item.IsEligible));
                }
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("@EligibleStudentDetails", dt));
                return DGeneric.RunSP_ExecuteNonQuery("sp_AddEligibleStudent", sqlParameterList);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
