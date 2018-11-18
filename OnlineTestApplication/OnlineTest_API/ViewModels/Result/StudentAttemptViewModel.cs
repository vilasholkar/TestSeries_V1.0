using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Result
{
   public class StudentAttemptViewModel
    {
        public int StudentAttemptID { get; set; }
        public int StudentID { get; set; }
        public int TestID { get; set; }
        public string EasyCorrect { get; set; }
        public string EasyInCorrect { get; set; }
        public string EasyNotAttempt { get; set; }
        public string MediumCorrect { get; set; }
        public string MediumInCorrect { get; set; }
        public string MediumNotAttempt { get; set; }
        public string DifficultCorrect { get; set; }
        public string DifficultInCorrect { get; set; }
        public string DifficultNotAttempt { get; set; }
    }
}
