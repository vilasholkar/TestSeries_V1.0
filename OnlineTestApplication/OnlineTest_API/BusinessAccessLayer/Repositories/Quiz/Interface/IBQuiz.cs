using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Test;

namespace BusinessAccessLayer
{
    public interface IBQuiz
    {
        Response<QuizViewModel> GetQuiz(int OnlineTestID,int StudentID);
        string SubmitQuiz(QuizViewModel QuizViewModel);
    }
}
