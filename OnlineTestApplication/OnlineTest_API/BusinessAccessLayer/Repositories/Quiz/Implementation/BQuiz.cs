using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
       public QuizViewModel GetQuiz(int OnlineTestID)
        {
            return _iDQuiz.GetQuiz(OnlineTestID);
        }
    }
}
