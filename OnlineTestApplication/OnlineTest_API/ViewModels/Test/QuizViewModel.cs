using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Question;

namespace ViewModels.Test
{
    public class QuizViewModel
    {
        public int OnlineTestID { get; set; }
        public string TestName { get; set; }
        public string TestDuration { get; set; }
        public string Instructions { get; set; }
        public List<QuestionViewModel> Question { get; set; }
        //public string ErrorMessage { get; set; }
    }
}
