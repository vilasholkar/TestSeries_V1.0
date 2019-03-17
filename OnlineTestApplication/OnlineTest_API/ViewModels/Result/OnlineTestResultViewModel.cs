using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Result
{
    public class OnlineTestResultViewModel
    {
        public int ResultID { get; set; }
        public int StudentID { get; set; }
        public string EnrollmentNo { get; set; }
        public string StudentName { get; set; }
        public string StudentCaste { get; set; }
        public int TestID { get; set; }
        public string TestName { get; set; }
        public DateTime TestDate { get; set; }
        public string TestSeriesName { get; set; }
        public string TestTypeName { get; set; }
        public string Physics_Total { get; set; }
        public string Physics_Right { get; set; }
        public string Physics_Wrong { get; set; }
        public string Chemistry_Total { get; set; }
        public string Chemistry_Right { get; set; }
        public string Chemistry_Wrong { get; set; }
        public string Biology_Total { get; set; }
        public string Biology_Right { get; set; }
        public string Biology_Wrong { get; set; }
        public string TotalCorrect { get; set; }
        public string TotalWrong { get; set; }
        public string TotalAttempt { get; set; }
        public string TotalMarksObtained { get; set; }
        public string Percentage { get; set; }
        public string Rank { get; set; }
        public string TotalMarks { get; set; }
        public string QualifyingMarks { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsPresent { get; set; }
    }
}
