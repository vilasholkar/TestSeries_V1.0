using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Test;

namespace BusinessAccessLayer
{
    public interface IBOnlineTest
    {
        Response<List<OnlineTestViewModel>> GetOnlineTest();
        string AddUpdateOnlineTest(OnlineTestViewModel objOnlineTest);
        string DeleteOnlineTest(int OnlineTestId);
        Response<OnlineTestViewModel> GetOnlineTestById(int OnlineTestId);
        Response<QuizViewModel> GetQuestionsByTestId(int OnlineTestID);
Response<List<StudentOnlineTestViewModel>> GetOnlineTestByStudentID(int StudentID);
        Response<OnlineTestMasterViewModel> GetOnlineTestMasterDataByTestID(int OnlineTestID);
        Response<List<OnlineTestViewModel>> GetOnlineTest_ForGenerateResult();


    }
}
