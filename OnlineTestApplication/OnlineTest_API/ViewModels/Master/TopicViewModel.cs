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
        public bool IsActive { get; set; }
    }
}
