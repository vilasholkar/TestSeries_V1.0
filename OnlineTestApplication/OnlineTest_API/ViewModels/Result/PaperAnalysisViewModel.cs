using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Result
{
   public class PaperAnalysisViewModel
    {
        public int PaperAnalysisID { get; set; }
        public int TestID { get; set; }
        public string TotalEasy { get; set; }
        public string TotalMedium { get; set; }
        public string TotalDifficult { get; set; }
        public string EasyQuestionList { get; set; }
        public string MediumQuestionList { get; set; }
        public string DifficultQuestionList { get; set; }
    }
}
