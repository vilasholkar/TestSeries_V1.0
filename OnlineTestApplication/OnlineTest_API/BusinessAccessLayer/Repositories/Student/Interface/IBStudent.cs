using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Student;

namespace BusinessAccessLayer
{
    public interface IBStudent
    {
        #region Student
        Response<List<StudentViewModel>> GetStudentDetails();        
        #endregion

        #region Attendance
        Response<AttendenceMainModel> GetAttendance(string Date,string EnrollmentNo = null);
        #endregion
    }
}
