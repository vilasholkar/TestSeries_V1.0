using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
   public interface IBResult
    {
       string ResultAnalysis(int TestID);
       DataTable GetOnlineTestResultByTestID(int TestID);
       DataTable GetOnlineTestResultByStudentID(int StudentID);

       DataRow GetPaperAnalysis(int TestID);
       DataRow GetStudentAttempt(int StudentID, int TestID);
       DataRow GetStudentMarksReview(int StudentID, int TestID);
       DataRow GetTopper_Average(int TestID);

    }
}
