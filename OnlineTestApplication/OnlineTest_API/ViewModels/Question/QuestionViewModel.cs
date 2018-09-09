using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Question
{
    public class QuestionViewModel
    {
        public int QuestionID { get; set; }
        public int QuestionTypeID { get; set; }
        public string Image_English { get; set; }
        public string Image_Hindi { get; set; }
        public string CorrectAnswer { get; set; }
        public List<OptionViewModel> Options { get; set; }
        public QuestionTypeViewModel QuestionType { get; set; }

    }
}
