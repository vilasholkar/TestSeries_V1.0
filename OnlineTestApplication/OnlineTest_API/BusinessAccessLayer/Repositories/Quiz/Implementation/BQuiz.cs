using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Test;

namespace BusinessAccessLayer
{
   public class BQuiz:IBQuiz
    {
       private readonly IDQuiz _iDQuiz;
       public BQuiz(IDQuiz iDQuiz)
       {
           _iDQuiz = iDQuiz;
       }
       public Response<QuizViewModel> GetQuiz(int OnlineTestID,int StudentID)
        {
            var quizData = _iDQuiz.GetQuiz(OnlineTestID, StudentID);
            if (quizData != null)
            {
                return new Response<QuizViewModel>
                {
                    IsSuccessful = true,
                    Object = quizData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<QuizViewModel>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }
       public string SubmitQuiz(QuizViewModel QuizViewModel)
       {
           return _iDQuiz.SubmitQuiz(QuizViewModel);
       }
    }
}
