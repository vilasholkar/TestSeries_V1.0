using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Test;

namespace DataAccessLayer
{
    public interface IDOnlineTest
    {
        List<OnlineTestViewModel> GetOnlineTest();
        string AddUpdateOnlineTest(OnlineTestViewModel objOnlineTest);
        string DeleteOnlineTest(int OnlineTestId);
        OnlineTestViewModel GetOnlineTestById(int OnlineTestID);
        QuizViewModel GetQuestionsByTestId(int OnlineTestID);
    	List<StudentOnlineTestViewModel> GetOnlineTestByStudentID(int StudentID);
        List<OnlineTestViewModel> GetOnlineTest_ForGenerateResult();

    }
}
