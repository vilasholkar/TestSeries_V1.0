using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Test
{
    public class OnlineTestViewModel
    {
        public int OnlineTestID { get; set; }
        public string OnlineTestNo { get; set; }
        public int TestSeriesID { get; set; }
        public string TestSeriesName { get; set; }
        public int TestTypeID { get; set; }
        public string TestTypeName { get; set; }
        public string TestName { get; set; }
        public string TestDuration { get; set; }
        public int SessionID { get; set; }
        public int StreamID { get; set; }
        public int CourseID { get; set; }
        public int BatchID { get; set; }
        public int SubjectID { get; set; }
        public string Topic { get; set; }
        public string Instructions { get; set; }
        public string TestMarks { get; set; }
        public string PassingPercentage { get; set; }
        public bool IsNegativeMarking { get; set; }
        public DateTime StartDate { get; set; }
        public string StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public string EndTime { get; set; }
        public bool IsVisible { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedOnDate { get; set; }

    }
}
