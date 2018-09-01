using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Test;

namespace DataAccessLayer
{
    public interface IDQuiz
    {
        QuizViewModel GetQuiz(int OnlineTestID);
    }
}
