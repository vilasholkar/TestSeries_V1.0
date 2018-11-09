using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DataTable GetOnlineTestResultByTestID(int TestID)
        {
            return _iDResult.GetOnlineTestResultByTestID(TestID);
        }
        public DataTable GetOnlineTestResultByStudentID(int StudentID)
        {
            return _iDResult.GetOnlineTestResultByStudentID(StudentID);
        }
        public DataRow GetPaperAnalysis(int TestID)
        {
            return _iDResult.GetPaperAnalysis(TestID);
        }
        public DataRow GetStudentAttempt(int StudentID, int TestID)
        {
            return _iDResult.GetStudentAttempt(StudentID, TestID);
        }
        public DataRow GetStudentMarksReview(int StudentID, int TestID)
        {
            return _iDResult.GetStudentMarksReview(StudentID, TestID);
        }
        public DataRow GetTopper_Average(int TestID)
        {
            return _iDResult.GetTopper_Average(TestID);
        }
    }
}
