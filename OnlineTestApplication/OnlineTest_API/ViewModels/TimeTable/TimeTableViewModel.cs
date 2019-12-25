using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Master;

namespace ViewModels.TimeTable
{
    public class TimeTableViewModel
    {
        public int TimeTableID { get; set; }
        public string Description { get; set; }
        public string DateType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int ShiftID { get; set; }
        public int SessionID { get; set; }
        public string Session { get; set; }
        public int[] BatchID { get; set; }
        public int[] LectureID { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public string CreatedOnDate { get; set; }
        public List<LectureViewModel> LectureList { get; set; }
        public List<LectureModel> Lecture { get; set; }
        public List<BatchViewModel> Batch { get; set; }
    }
}
