using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Master;

namespace ViewModels.TimeTable
{
    public class LectureViewModel
    {
        public int ID { get; set; }
        public int TimeTableID { get; set; }
        public string LectureName { get; set; }
        public BatchViewModel Batch { get; set; }
        public int BatchID { get; set; }
        public int LectureID { get; set; }
        public LectureViewModel Lecture { get; set; }
        public string Time_From { get; set; }
        public string Time_To { get; set; }
        public string Subject { get; set; }
        public int FacultyID { get; set; }
        public string Faculty { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedOnDate { get; set; }
    }

    public class LectureModel
    {
        public int LectureID { get; set; }
        public string Lecture { get; set; }
    }
}
