using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Test
{
    public class EligibleStudentViewModel
    {
        public int StudentID { get; set; }
        public int OnlineTestID { get; set; }
        public string TestName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EnrollmentNo { get; set; }
        public string StudentName { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string FatherMobileNo { get; set; }
        public bool IsEligible { get; set; }
        public int TestStatusID { get; set; }
        public string TestStatus { get; set; }
        public string Stream { get; set; }
        public string Course { get; set; }
        public string Batch { get; set; }
        public string Session { get; set; }
        public bool IsMessageSend { get; set; }
    }
}
