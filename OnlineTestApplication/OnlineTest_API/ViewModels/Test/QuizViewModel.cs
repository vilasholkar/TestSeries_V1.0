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
        public int OnlineTestNo { get; set; }
        public string TestName { get; set; }
        public string TestDuration { get; set; }
        public string Instructions { get; set; }
        public string TestSeries { get; set; }
        public string TestType { get; set; }
        public int TotalMarks { get; set; }
        public string PassingPercentage { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public int StudentID { get; set; }
        public int PhysicsQuestionCount { get; set; }
        public int ChemistryQuestionCount { get; set; }
        public int BiologyQuestionCount { get; set; }
    }
}
