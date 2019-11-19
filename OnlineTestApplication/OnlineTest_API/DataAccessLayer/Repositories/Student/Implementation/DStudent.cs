using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Student;
using ViewModels;
using System.Data.Common;

namespace DataAccessLayer
{
    public class DStudent : IDStudent
    {
        #region Student
        public List<StudentViewModel> GetStudentDetails()
        {
            string strQuery = @"SELECT  dbo.StudentAccount.StudentAccountID, dbo.StudentAccount.StudentID, dbo.StudentAccount.Password, dbo.StudentAccount.IsActive, 
                      dbo.StudentAccount.CreatedByUserID, dbo.StudentAccount.CreatedOnDate, dbo.Student.EnrollmentNo, dbo.Student.EnrollmentDate, dbo.Student.FirstName, 
                      dbo.Student.MiddleName, dbo.Student.LastName, dbo.Student.Gender, dbo.Student.DOB, dbo.Student.MobileNumber, dbo.Student.PhoneNumber, dbo.Student.Email, 
                      dbo.Student.Address, dbo.Student.Landmark, dbo.Student.CityID, dbo.Student.Pin, dbo.Student.Medium, dbo.Student.Cast, dbo.Student.PhotoUrl, 
                      dbo.Student.StreamID, dbo.Student.CourseID, dbo.Student.BatchID, dbo.Student.SessionID, dbo.Student.[School/College], dbo.Student.FatherName, 
                      dbo.Student.FatherOccupation, dbo.Student.FatherMobile, dbo.Student.FatherEmail
                    FROM dbo.Student RIGHT OUTER JOIN
                      dbo.StudentAccount ON dbo.Student.StudentID = dbo.StudentAccount.StudentID";

            DataTable dt = DGeneric.GetData(strQuery).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<StudentViewModel>(dt);
            else
                return new List<StudentViewModel>();
            //return dt.AsEnumerable().Select(x => new StudentViewModel()
            //{
            //    StudentAccountID = Convert.ToInt32(x["StudentAccountID"]),
            //    StudentID = Convert.ToInt32(x["StudentID"]),
            //    EnrollmentNo = x["EnrollmentNo"].ToString(),
            //    EnrollmentDate = x["EnrollmentDate"].ToString().ConvertDateTimeToString(),
            //    FirstName = x["FirstName"].ToString(),
            //    LastName = x["LastName"].ToString(),
            //    Gender = x["Gender"].ToString(),
            //    DOB = x["DOB"].ToString().ConvertDateTimeToString(),
            //    MobileNo = x["MobileNumber"].ToString(),
            //    Email = x["Email"].ToString(),
            //    Address = x["Address"].ToString(),
            //    Landmark = x["Landmark"].ToString(),
            //    Pincode = x["Pin"].ToString(),
            //    CityID = Convert.ToInt32(x["CityID"]),
            //    Caste = x["Cast"].ToString(),
            //    PhotoUrl = x["PhotoUrl"].ToString(),
            //    Medium = x["Medium"].ToString(),
            //    FatherName = x["FatherName"].ToString(),
            //    FatherMobile = x["FatherMobile"].ToString(),
            //    FatherEmail = x["FatherEmail"].ToString(),
            //    //SessionID = Convert.ToInt32(x["SessionID"]),
            //    //StreamID = Convert.ToInt32(x["StreamID"]),
            //    //CourseID = Convert.ToInt32(x["CourseID"]),
            //    //BatchID = Convert.ToInt32(x["BatchID"]),

            //}).ToList();
        }
        #endregion

        #region Attendance
        public AttendenceMainModel GetAttendance(string Date, string EnrollmentNo = null)
        {
            var sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@Date", Date));
            sqlParameterList.Add(new SqlParameter("@EnrollmentNo", EnrollmentNo));
            DataTable dtAttendance = DGeneric.RunSP_ReturnDataSet("sp_GetAttendance", sqlParameterList, null).Tables[0];
            if (dtAttendance.Rows.Count > 0)
            {
                var attendanceObj = new AttendenceMainModel();
                var studentList = new List<StudentViewModel>();
                foreach (DataRow dr in dtAttendance.Rows)
                {
                    var studentData = studentList.FirstOrDefault(f => f.EnrollmentNo.Equals(dr["EnrollmentNo"].ToString()));
                    if (studentData != null)
                    {
                        studentData.AttendenceList.Add(new Attendance()
                        {
                            AttendanceId = Convert.ToInt64(dr["AttendanceId"]),
                            PunchDate = dr["PunchDate"].ToString(),
                            PunchIn = dr["PunchIn"].ToString(),
                            PunchOut = dr["PunchOut"].ToString(),
                            ArrivalDeparture = dr["ArrivalDeparture"].ToString(),
                            Status = dr["Status"].ToString()
                        });
                    }
                    else
                    {
                        var studentModel = new StudentViewModel();
                        studentModel.EnrollmentNo = dr["EnrollmentNo"].ToString();
                        studentModel.FirstName = dr["FirstName"].ToString();
                        studentModel.LastName = dr["LastName"].ToString();
                        if (!string.IsNullOrEmpty(dr["AttendanceID"].ToString()))
                        {
                            studentModel.AttendenceList.Add(new Attendance()
                            {
                                AttendanceId = Convert.ToInt64(dr["AttendanceId"]),
                                PunchDate = dr["PunchDate"].ToString(),
                                PunchIn = dr["PunchIn"].ToString(),
                                PunchOut = dr["PunchOut"].ToString(),
                                ArrivalDeparture = dr["ArrivalDeparture"].ToString(),
                                Status = dr["Status"].ToString()
                            });
                        }
                        studentList.Add(studentModel);
                    }
                }
                attendanceObj.StudentList = studentList;
                attendanceObj.DateRange = GetDates(Date.ConvertDateTimeToDate().Year, Date.ConvertDateTimeToDate().Month);
                return attendanceObj;
            }
            else
                return new AttendenceMainModel();
        }

        private List<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList(); // Load dates into a list
        }
        #endregion
    }
}
