using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Test;

namespace ViewModels.Master
{
    public class MasterViewModel
    {
        public List<SessionViewModel> Session { get; set; }

        public List<StreamViewModel> Stream { get; set; }

        public List<TestSeriesViewModel> TestSeries { get; set; }

        public List<TestTypeViewModel> TestType { get; set; }
    }
    public class SessionViewModel
    {
        public int SessionID { get; set; }
        public string Session { get; set; }
    }
    public class StreamViewModel
    {
        public int StreamID { get; set; }
        public string Stream { get; set; }
    }
    public class CourseViewModel
    {
        public int CourseID { get; set; }
        public string Course { get; set; }
        public int StreamID { get; set; }
    }
    public class BatchViewModel
    {
        public int BatchID { get; set; }
        public string Batch { get; set; }
        public int CourseID { get; set; }
    }
}
