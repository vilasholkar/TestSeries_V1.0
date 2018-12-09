using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Result;

namespace BusinessAccessLayer
{
    public interface IBResult
    {
        string ResultAnalysis(int TestID);
        //DataTable GetOnlineTestResultByTestID(int TestID);
        //DataTable GetOnlineTestResultByStudentID(int StudentID);
        //DataRow GetStudentMarksReview(int StudentID, int TestID);
        Response<ResultAnalysisViewModel> GetResultAnalysis(int StudentID, int TestID);
        PaperAnalysisViewModel GetPaperAnalysis(int TestID);
        StudentAttemptViewModel GetStudentAttempt(int StudentID, int TestID);
        Response<List<OnlineTestResultViewModel>> GetOnlineTestResultByID(int StudentID, int TestID);
        List<Topper_AverageViewModel> GetTopper_Average(int TestID);

    }
}
