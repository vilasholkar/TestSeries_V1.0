using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Question
{
   public class OptionViewModel
    {
        public int OptionID { get; set; }
        public int QuestionID { get; set; }
        public string Option { get; set; }
        public bool IsAnswer { get; set; }
    }
}
