using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Result
{
    public class ResultAnalysisViewModel
    {
        public PaperAnalysisViewModel PaperAnalysis { get; set; }
        public StudentAttemptViewModel StudentAttempt { get; set; }
        public List<OnlineTestResultViewModel> OnlineTestResult { get; set; }
        public List<Topper_AverageViewModel> Topper_Average { get; set; }
        public StudentRankViewModel StudentRank { get; set; }

    }
}
