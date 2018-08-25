using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Master
{
   public class MasterViewModel
    {
    }
   public class SessionViewModel
    {
        public int SessionID { get; set; }
        public string SessionName { get; set; }
    }
   public class StreamViewModel
   {
        public int StreamID { get; set; }
        public string StreamName { get; set; }
    }
   public class CourseViewModel
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int StreamID { get; set; }
    }
   public class BatchViewModel
    {
        public int BatchID { get; set; }
        public string BatchName { get; set; }
        public int CourseID { get; set; }
    }
}
