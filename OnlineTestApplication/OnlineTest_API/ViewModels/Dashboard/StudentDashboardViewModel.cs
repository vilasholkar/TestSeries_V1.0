using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Dashboard
{
   public class StudentDashboardViewModel
    {
        public int TotalTest { get; set; }
        public int NotStarted { get; set; }
        public int Started { get; set; }
        public int Completed { get; set; }
        public List<TestPercentage> TestPercentage { get; set; }
    }

    public class TestPercentage
    {
        public int ResultID { get; set; }
        public string TestName { get; set; }
        public string Percentage { get; set; }
    }
}
