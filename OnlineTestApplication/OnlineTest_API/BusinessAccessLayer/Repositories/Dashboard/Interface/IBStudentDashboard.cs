using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Dashboard;

namespace BusinessAccessLayer
{
   public interface IBStudentDashboard
    {
        Response<StudentDashboardViewModel> GetStudentDashboardDetail(int StudentID);
    }
}
