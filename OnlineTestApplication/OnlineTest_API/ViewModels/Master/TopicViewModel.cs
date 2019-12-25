using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Master
{
    public class TopicViewModel
    {
        public int TopicID { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public int SubjectID { get; set; }
        public string Subject { get; set; }
        public int SessionID { get; set; }
        public string Session { get; set; }
        public int[] StreamID { get; set; }
        public int[] CourseID { get; set; }
        public int[] BatchID { get; set; }
        public bool IsActive { get; set; }
        public List<CourseViewModel> Course { get; set; }
        public List<BatchViewModel> Batch { get; set; }
    }
    public class TopicMasterViewModel
    {
        public TopicViewModel TopicData { get; set; }

        public MasterViewModel MasterData { get; set; }
    }
}
