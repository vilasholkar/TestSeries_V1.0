using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Result
{
   public class Topper_AverageViewModel
    {
        public int Topper_AverageID { get; set; }
        public string Topper_Average { get; set; }
        public int TestID { get; set; }
        public string Physics_Right { get; set; }
        public string Physics_Wrong { get; set; }
        public string Chemistry_Right { get; set; }
        public string Chemistry_Wrong { get; set; }
        public string Biology_Right { get; set; }
        public string Biology_Wrong { get; set; }
        public string TotalCorrect { get; set; }
        public string TotalWrong { get; set; }
        public string TotalAttempt { get; set; }
        public string TotalMarksObtained { get; set; }
        public string Percentage { get; set; }
    }
}
