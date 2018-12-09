using DataAccessLayer;
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
    public class BResult : IBResult
    {
        private readonly IDResult _iDResult;
        public BResult(IDResult iDResult)
        {
            _iDResult = iDResult;
        }
        public string ResultAnalysis(int TestID)
        {
            return _iDResult.ResultAnalysis(TestID);
        }
        //public DataTable GetOnlineTestResultByTestID(int TestID)
        //{
        //    return _iDResult.GetOnlineTestResultByTestID(TestID);
        //}
        //public DataTable GetOnlineTestResultByStudentID(int StudentID)
        //{
        //    return _iDResult.GetOnlineTestResultByStudentID(StudentID);
        //}
       public Response<ResultAnalysisViewModel> GetResultAnalysis(int StudentID, int TestID)
        {
            
            try
            {
                var resultAnalysisData = _iDResult.GetResultAnalysis(StudentID, TestID);
                if (resultAnalysisData != null)
                {
                    return new Response<ResultAnalysisViewModel>
                    {
                        IsSuccessful = true,
                        Object = resultAnalysisData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<ResultAnalysisViewModel>
                    {
                        IsSuccessful = false,
                        Message = "error",
                        Object = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<ResultAnalysisViewModel>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Object = null
                };
            }
        }
        public PaperAnalysisViewModel GetPaperAnalysis(int TestID)
        {
            return _iDResult.GetPaperAnalysis(TestID);
        }
        public StudentAttemptViewModel GetStudentAttempt(int StudentID, int TestID)
        {
            return _iDResult.GetStudentAttempt(StudentID, TestID);
        }
        public Response<List<OnlineTestResultViewModel>> GetOnlineTestResultByID(int StudentID, int TestID) 
        {
            try
            {
                var onlineTestResultData = _iDResult.GetOnlineTestResultByID(StudentID, TestID);
                if (onlineTestResultData != null)
                {
                    return new Response<List<OnlineTestResultViewModel>>
                    {
                        IsSuccessful = true,
                        Object = onlineTestResultData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<List<OnlineTestResultViewModel>>
                    {
                        IsSuccessful = false,
                        Message = "error",
                        Object = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<OnlineTestResultViewModel>>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Object = null
                };
            }
        }
        //public DataRow GetStudentMarksReview(int StudentID, int TestID)
        //{
        //    return _iDResult.GetStudentMarksReview(StudentID, TestID);
        //}
        public List<Topper_AverageViewModel> GetTopper_Average(int TestID)
        {
            return _iDResult.GetTopper_Average(TestID);
        }
    }
}
