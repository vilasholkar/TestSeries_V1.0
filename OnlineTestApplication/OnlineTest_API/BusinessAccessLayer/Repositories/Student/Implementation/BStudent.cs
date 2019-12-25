using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using ViewModels.Student;
using ViewModels;

namespace BusinessAccessLayer
{
    public class BStudent : IBStudent
    {
        private readonly IDStudent _iDStudent;

        #region Student
        public BStudent(IDStudent iDStudent)
        {
            _iDStudent = iDStudent;
        }
        public Response<List<StudentViewModel>> GetStudentDetails()
        {
            var studentData = _iDStudent.GetStudentDetails();
            if (studentData != null)
            {
                return new Response<List<StudentViewModel>>
                {
                    IsSuccessful = true,
                    Object = studentData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<StudentViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }

        public Response<List<StudentReport>> GetFilteredStudent(StudentReport objSR)
        {
            var studentData = _iDStudent.GetFilteredStudent(objSR);
            if (studentData != null)
            {
                return new Response<List<StudentReport>>
                {
                    IsSuccessful = true,
                    Object = studentData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<StudentReport>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }
        #endregion

        #region Attendance
        public Response<AttendenceMainModel> GetAttendance(string Date, string EnrollmentNo = null)
        {
            var studentAttendance = _iDStudent.GetAttendance(Date, EnrollmentNo);
            if (studentAttendance != null)
            {
                return new Response<AttendenceMainModel>()
                {
                    IsSuccessful = true,
                    Message = CommonEnum.Status.Success.ToString(),
                    Object = studentAttendance
                };
            }
            else
            {
                return new Response<AttendenceMainModel>()
                {
                    IsSuccessful = false,
                    Message = CommonEnum.Status.Failed.ToString(),
                    Object = studentAttendance
                };
            }
        }
        #endregion
    }
}
