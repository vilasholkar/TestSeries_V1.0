using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Test;

namespace DataAccessLayer
{
    public class DOnlineTest : IDOnlineTest
    {
        public List<OnlineTestViewModel> GetOnlineTest()
        {
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetAllOnlineTest", null, null).Tables[0];
            return dt.AsEnumerable().Select(s => new OnlineTestViewModel()
            {
                OnlineTestID = Convert.ToInt32(s["OnlineTestID"]),
                OnlineTestNo = s["OnlineTestNo"].ToString(),
                TestSeriesID = Convert.ToInt32(s["TestSeriesID"]),
                TestSeriesName = s["TestSeries"].ToString(),
                TestTypeID = Convert.ToInt32(s["TestTypeID"]),
                TestTypeName = s["TestType"].ToString(),
                TestName = s["TestName"].ToString(),
                TestDuration = s["TestDuration"].ToString(),
                SessionID = Convert.ToInt32(s["SessionID"]),
                StreamID = Convert.ToInt32(s["StreamID"]),
                CourseID = Convert.ToInt32(s["CourseID"]),
                BatchID = Convert.ToInt32(s["BatchID"]),
                SubjectID = Convert.ToInt32(s["SubjectID"]),
                Topic = s["Topic"].ToString(),
                Instructions = s["Instructions"].ToString(),
                TestMarks = s["TestMarks"].ToString(),
                PassingPercentage = s["PassingPercentage"].ToString(),
                IsNegativeMarking = Convert.ToBoolean(s["IsNegativeMarking"]),
                IsVisible = Convert.ToBoolean(s["IsVisible"]),
                StartTime = s["StartTime"].ToString(),
                EndTime = s["EndTime"].ToString(),
                StartDate = Convert.ToDateTime(s["StartDate"]),
                EndDate = Convert.ToDateTime(s["EndDate"])
            }).ToList();
        }
        public string AddUpdateOnlineTest(OnlineTestViewModel objOnlineTest)
        {
            try
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("OnlineTestID", objOnlineTest.OnlineTestID));
                sqlParameterList.Add(new SqlParameter("OnlineTestNo", string.IsNullOrEmpty(objOnlineTest.OnlineTestNo) ? DGeneric.GetNewId("OnlineTest", "OnlineTestID").ToString() : objOnlineTest.OnlineTestNo));
                sqlParameterList.Add(new SqlParameter("TestSeriesID", objOnlineTest.TestSeriesID));
                sqlParameterList.Add(new SqlParameter("TestTypeID", objOnlineTest.TestTypeID));
                sqlParameterList.Add(new SqlParameter("TestName", !string.IsNullOrEmpty(objOnlineTest.TestName) ? objOnlineTest.TestName : string.Empty));
                sqlParameterList.Add(new SqlParameter("TestDuration", !string.IsNullOrEmpty(objOnlineTest.TestDuration) ? objOnlineTest.TestDuration : string.Empty));
                sqlParameterList.Add(new SqlParameter("SessionID", objOnlineTest.SessionID));
                sqlParameterList.Add(new SqlParameter("StreamID", objOnlineTest.StreamID));
                sqlParameterList.Add(new SqlParameter("CourseID", objOnlineTest.CourseID));
                sqlParameterList.Add(new SqlParameter("BatchID", objOnlineTest.BatchID));
                sqlParameterList.Add(new SqlParameter("SubjectID", objOnlineTest.SubjectID));
                sqlParameterList.Add(new SqlParameter("Topic", !string.IsNullOrEmpty(objOnlineTest.Topic) ? objOnlineTest.Topic : string.Empty));
                sqlParameterList.Add(new SqlParameter("Instructions", !string.IsNullOrEmpty(objOnlineTest.Instructions) ? objOnlineTest.Instructions : string.Empty));
                sqlParameterList.Add(new SqlParameter("TestMarks", !string.IsNullOrEmpty(objOnlineTest.TestMarks) ? objOnlineTest.TestMarks : string.Empty));
                sqlParameterList.Add(new SqlParameter("PassingPercentage", !string.IsNullOrEmpty(objOnlineTest.PassingPercentage) ? objOnlineTest.PassingPercentage : string.Empty));
                sqlParameterList.Add(new SqlParameter("IsNegativeMarking", objOnlineTest.IsNegativeMarking));
                sqlParameterList.Add(new SqlParameter("StartDate", objOnlineTest.StartDate));
                sqlParameterList.Add(new SqlParameter("StartTime", !string.IsNullOrEmpty(objOnlineTest.StartTime) ? objOnlineTest.StartTime : string.Empty));
                sqlParameterList.Add(new SqlParameter("EndDate", objOnlineTest.EndDate));
                sqlParameterList.Add(new SqlParameter("EndTime", !string.IsNullOrEmpty(objOnlineTest.EndTime) ? objOnlineTest.EndTime : string.Empty));
                sqlParameterList.Add(new SqlParameter("IsVisible", objOnlineTest.IsVisible));
                sqlParameterList.Add(new SqlParameter("IsActive", true));
                sqlParameterList.Add(new SqlParameter("CreatedByUserID", 1));
                sqlParameterList.Add(new SqlParameter("CreatedOnDate", DGeneric.SystemDateTime));

                return DGeneric.RunSP_ExecuteNonQuery("sp_AddUpdateOnlineTest", sqlParameterList);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeleteOnlineTest(int OnlineTestId)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@OnlineTestID", OnlineTestId));
            return DGeneric.RunSP_ExecuteNonQuery("sp_DeleteOnlineTest", sqlParameterList);
        }
        public OnlineTestViewModel GetOnlineTestById(int OnlineTestId)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@OnlineTestID", OnlineTestId));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetOnlineTestById", sqlParameterList, null).Tables[0];
            return dt.AsEnumerable().Select(s => new OnlineTestViewModel()
            {
                OnlineTestID = Convert.ToInt32(s["OnlineTestID"]),
                OnlineTestNo = s["OnlineTestNo"].ToString(),
                TestSeriesID = Convert.ToInt32(s["TestSeriesID"]),
                TestTypeID = Convert.ToInt32(s["TestTypeID"]),
                TestName = s["TestName"].ToString(),
                TestDuration = s["TestDuration"].ToString(),
                SessionID = Convert.ToInt32(s["SessionID"]),
                StreamID = Convert.ToInt32(s["StreamID"]),
                CourseID = Convert.ToInt32(s["CourseID"]),
                BatchID = Convert.ToInt32(s["BatchID"]),
                SubjectID = Convert.ToInt32(s["SubjectID"]),
                Topic = s["Topic"].ToString(),
                Instructions = s["Instructions"].ToString(),
                TestMarks = s["TestMarks"].ToString(),
                PassingPercentage = s["PassingPercentage"].ToString(),
                IsNegativeMarking = Convert.ToBoolean(s["IsNegativeMarking"]),
                IsVisible = Convert.ToBoolean(s["IsVisible"]),
                StartTime = s["StartTime"].ToString(),
                EndTime = s["EndTime"].ToString(),
                StartDate = Convert.ToDateTime(s["StartDate"]),
                EndDate = Convert.ToDateTime(s["EndDate"])
            }).FirstOrDefault();
        }
    }
}
