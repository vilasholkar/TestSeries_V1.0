using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Question
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public string Image_English { get; set; }
        public string Image_Hindi { get; set; }
        public List<OptionViewModel> Options { get; set; }
        public QuestionTypeViewModel QuestionType { get; set; }

    }
}
