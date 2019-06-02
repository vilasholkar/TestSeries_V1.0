using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.StudyMaterial
{
    public class StudyMaterialViewModel
    {
        public int StudyMaterialID { get; set; }
        public string Tittle { get; set; }
        public string SubTittle { get; set; }
        public string Description { get; set; }
        public int TopicID { get; set; }
        public int SubTopicID { get; set; }
        public int SessionID { get; set; }
        public int[] StreamID { get; set; }
        public int[] CourseID { get; set; }
        public int[] BatchID { get; set; }
        public int SubjectID { get; set; }
        public string Thumbnail { get; set; }
        public string URL { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedOnDate { get; set; }
    }
}
