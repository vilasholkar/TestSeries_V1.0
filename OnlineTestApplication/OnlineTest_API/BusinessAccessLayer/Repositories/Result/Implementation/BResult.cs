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
        public Response<string> ResultAnalysis(int TestID)
        {
            var resultData = _iDResult.ResultAnalysis(TestID);
            if (!string.IsNullOrEmpty(resultData))
            {
               return new Response<string>
                {
                    IsSuccessful = true,
                    Object = resultData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<string>
                {
                    IsSuccessful = false,
                    Object = null,
                    Message = "error"
                };
            }
        }
        public Response<ResultAnalysisViewModel> GetResultAnalysis(int StudentID, int TestID)
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

        public List<Topper_AverageViewModel> GetTopper_Average(int TestID)
        {
            return _iDResult.GetTopper_Average(TestID);
        }

        public Response<List<StudentResponseViewModel>> GetStudentResponse(int StudentID, int TestID)
        {
            var studentResponseData = _iDResult.GetStudentResponse(StudentID, TestID);
            if (studentResponseData != null)
            {
                return new Response<List<StudentResponseViewModel>>
                {
                    IsSuccessful = true,
                    Object = studentResponseData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<StudentResponseViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }
    }
}
