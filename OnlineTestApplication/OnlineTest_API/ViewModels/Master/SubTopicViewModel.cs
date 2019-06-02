using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Master
{
   public class SubTopicViewModel
    {
        public int SubTopicID { get; set; }
        public string SubTopic { get; set; }
        public string Description { get; set; }
        public int TopicID { get; set; }
        public string Topic { get; set; }
        public bool IsActive { get; set; }
    }
}
