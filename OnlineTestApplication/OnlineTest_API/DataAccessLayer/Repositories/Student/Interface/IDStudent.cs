using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Student;
using ViewModels;

namespace DataAccessLayer
{
   public interface IDStudent
   {
       #region Student
       List<StudentViewModel> GetStudentDetails();       
       #endregion

       #region Attendance
       AttendenceMainModel GetAttendance(string Date,string EnrollmentNo = null);
       #endregion
   }
}
