using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Dashboard;

namespace DataAccessLayer
{
    public interface IDStudentDashboard
    {
        StudentDashboardViewModel GetStudentDashboardDetail(int StudentID);
    }
}
