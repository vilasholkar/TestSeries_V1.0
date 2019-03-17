using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Result
{
    public class StudentResponseViewModel
    {
        public int StudentResponseID { get; set; }
        public int StudentID { get; set; }
        public int TestID { get; set; }
        public int QuestionID { get; set; }
        public int SubjectID { get; set; }
        public int OptionID { get; set; }
        public int AnswerID { get; set; }
        public bool IsCorrect { get; set; }
        public string TestQuestionNo { get; set; }
        public string Image_English { get; set; }
        public string Image_Hindi { get; set; }

    }
}
