using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public PaperAnalysisViewModel GetPaperAnalysis(int TestID)
        {
            return _iDResult.GetPaperAnalysis(TestID);
        }
        public StudentAttemptViewModel GetStudentAttempt(int StudentID, int TestID)
        {
            return _iDResult.GetStudentAttempt(StudentID, TestID);
        }
        List<OnlineTestResultViewModel> GetOnlineTestResultByID(int StudentID, int TestID) 
        {
            return _iDResult.GetOnlineTestResultByID(StudentID, TestID);
        
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
