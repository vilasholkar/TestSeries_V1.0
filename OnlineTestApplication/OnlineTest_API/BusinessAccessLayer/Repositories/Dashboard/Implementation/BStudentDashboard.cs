using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Dashboard;

namespace BusinessAccessLayer
{
   public class BStudentDashboard : IBStudentDashboard
    {
        private readonly IDStudentDashboard _iDStudentDashboard;
        public BStudentDashboard(IDStudentDashboard iDStudentDashboard)
        {
            _iDStudentDashboard = iDStudentDashboard;
        }
        public Response<StudentDashboardViewModel> GetStudentDashboardDetail(int StudentID)
        {
            var studentDashboardDetail = _iDStudentDashboard.GetStudentDashboardDetail(StudentID);
            if (studentDashboardDetail != null)
            {
                return new Response<StudentDashboardViewModel>
                {
                    IsSuccessful = true,
                    Object = studentDashboardDetail,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<StudentDashboardViewModel>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }
    }
}
