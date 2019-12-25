using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Master;
using ViewModels.Question;
using ViewModels.Test;

namespace DataAccessLayer
{
    public class DOnlineTest : IDOnlineTest
    {
        public List<OnlineTestViewModel> GetOnlineTest()
        {
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetAllOnlineTest", null, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<OnlineTestViewModel>(dt);
            else
                return new List<OnlineTestViewModel>();

            //return dt.AsEnumerable().Select(s => new OnlineTestViewModel()
            //{
            //    OnlineTestID = Convert.ToInt32(s["OnlineTestID"]),
            //    OnlineTestNo = s["OnlineTestNo"].ToString(),
            //    TestSeriesID = Convert.ToInt32(s["TestSeriesID"]),
            //    TestSeries = s["TestSeries"].ToString(),
            //    TestTypeID = Convert.ToInt32(s["TestTypeID"]),
            //    TestType = s["TestType"].ToString(),
            //    TestName = s["TestName"].ToString(),
            //    TestDuration = s["TestDuration"].ToString(),
            //    SessionID = Convert.ToInt32(s["SessionID"]),
            //    //StreamID = s["StreamID"].ToString().Split(',').ToArray(),
            //    //CourseID = Convert.ToInt32(s["CourseID"]),
            //    //BatchID = Convert.ToInt32(s["BatchID"]),
            //    //StreamID = s["StreamID"].ToString().Split(',').Select(int.Parse).ToArray(),
            //    //CourseID = s["CourseID"].ToString() != string.Empty ? dt.Rows[0]["CourseID"].ToString().Split(',').Select(int.Parse).ToArray() : null,
            //    //BatchID = s["BatchID"].ToString() != string.Empty ? dt.Rows[0]["BatchID"].ToString().Split(',').Select(int.Parse).ToArray() : null,
            //   // SubjectID = Convert.ToInt32(s["SubjectID"]),
            //    Topic = s["Topic"].ToString(),
            //    Instructions = s["Instructions"].ToString(),
            //    TestMarks = s["TestMarks"].ToString(),
            //    PassingPercentage = s["PassingPercentage"].ToString(),
            //    IsNegativeMarking = Convert.ToBoolean(s["IsNegativeMarking"]),
            //    IsVisible = Convert.ToBoolean(s["IsVisible"]),
            //    StartTime = s["StartTime"].ToString(),
            //    EndTime = s["EndTime"].ToString(),
            //    StartDate = Convert.ToString(s["StartDate"]).ConvertDateTimeToString(),
            //    EndDate = Convert.ToString(s["EndDate"]).ConvertDateTimeToString()
            //}).ToList();
        }
        public string AddUpdateOnlineTest(OnlineTestViewModel objOnlineTest)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("OnlineTestID", objOnlineTest.OnlineTestID));
            sqlParameterList.Add(new SqlParameter("OnlineTestNo", string.IsNullOrEmpty(objOnlineTest.OnlineTestNo) ? DGeneric.GetNewId("OnlineTest", "OnlineTestID").ToString() : objOnlineTest.OnlineTestNo));
            sqlParameterList.Add(new SqlParameter("TestSeriesID", objOnlineTest.TestSeriesID));
            sqlParameterList.Add(new SqlParameter("TestTypeID", objOnlineTest.TestTypeID));
            sqlParameterList.Add(new SqlParameter("TestName", !string.IsNullOrEmpty(objOnlineTest.TestName) ? objOnlineTest.TestName : string.Empty));
            sqlParameterList.Add(new SqlParameter("TestDuration", !string.IsNullOrEmpty(objOnlineTest.TestDuration) ? objOnlineTest.TestDuration : string.Empty));
            sqlParameterList.Add(new SqlParameter("SessionID", objOnlineTest.SessionID));
            sqlParameterList.Add(new SqlParameter("StreamID", string.Join(",", objOnlineTest.StreamID)));
            sqlParameterList.Add(new SqlParameter("CourseID", objOnlineTest.CourseID != null ? objOnlineTest.CourseID.Length > 0 ? string.Join(",", objOnlineTest.CourseID) : string.Empty : string.Empty));
            sqlParameterList.Add(new SqlParameter("BatchID", objOnlineTest.BatchID != null & objOnlineTest.CourseID != null ? objOnlineTest.BatchID.Length > 0 & objOnlineTest.CourseID.Length > 0 ? string.Join(",", objOnlineTest.BatchID) : string.Empty : string.Empty));
            sqlParameterList.Add(new SqlParameter("SubjectID", objOnlineTest.SubjectID != null ? objOnlineTest.SubjectID.Length > 0 ? string.Join(",", objOnlineTest.SubjectID) : string.Empty : string.Empty));
            sqlParameterList.Add(new SqlParameter("Topic", !string.IsNullOrEmpty(objOnlineTest.Topic) ? objOnlineTest.Topic : string.Empty));
            sqlParameterList.Add(new SqlParameter("Instructions", !string.IsNullOrEmpty(objOnlineTest.Instructions) ? objOnlineTest.Instructions : string.Empty));
            sqlParameterList.Add(new SqlParameter("TestMarks", !string.IsNullOrEmpty(objOnlineTest.TestMarks) ? objOnlineTest.TestMarks : string.Empty));
            sqlParameterList.Add(new SqlParameter("PassingPercentage", !string.IsNullOrEmpty(objOnlineTest.PassingPercentage) ? objOnlineTest.PassingPercentage : string.Empty));
            sqlParameterList.Add(new SqlParameter("IsNegativeMarking", objOnlineTest.IsNegativeMarking));
            sqlParameterList.Add(new SqlParameter("StartDate", Convert.ToDateTime(objOnlineTest.StartDate)));
            sqlParameterList.Add(new SqlParameter("StartTime", !string.IsNullOrEmpty(objOnlineTest.StartTime) ? objOnlineTest.StartTime : string.Empty));
            sqlParameterList.Add(new SqlParameter("EndDate", Convert.ToDateTime(objOnlineTest.EndDate)));
            sqlParameterList.Add(new SqlParameter("EndTime", !string.IsNullOrEmpty(objOnlineTest.EndTime) ? objOnlineTest.EndTime : string.Empty));
            sqlParameterList.Add(new SqlParameter("IsVisible", objOnlineTest.IsVisible));
            sqlParameterList.Add(new SqlParameter("IsActive", true));
            sqlParameterList.Add(new SqlParameter("CreatedByUserID", 1));
            sqlParameterList.Add(new SqlParameter("CreatedOnDate", DGeneric.SystemDateTime));

            return DGeneric.RunSP_ExecuteNonQuery("sp_AddUpdateOnlineTest", sqlParameterList);

            //if (!Directory.Exists("educationbridge.co.in\\" + OnlineTestID + "\\English"))
            // {
            //     Directory.CreateDirectory("C:\\" + OnlineTestID + "\\English");
            // }
            //if (!Directory.Exists("C:\\" + OnlineTestID + "\\Hindi"))
            // {
            //     Directory.CreateDirectory("C:\\" + OnlineTestID + "\\Hindi");
            // }
            //return CommonEnum.Status.Success.ToString();
        }
        public string DeleteOnlineTest(int OnlineTestId)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@OnlineTestID", OnlineTestId));
            return DGeneric.RunSP_ExecuteNonQuery("sp_DeleteOnlineTest", sqlParameterList);
        }
        public OnlineTestViewModel GetOnlineTestById(int OnlineTestID)
        {
            var onlineTestViewModelData = new OnlineTestViewModel();
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@OnlineTestID", OnlineTestID));
            IList<DataTableMapping> dataTableMappingList = new List<DataTableMapping>();
            dataTableMappingList.Add(new DataTableMapping("Table", "OnlineTest"));
            dataTableMappingList.Add(new DataTableMapping("Table1", "Course"));
            dataTableMappingList.Add(new DataTableMapping("Table2", "Batch"));
            dataTableMappingList.Add(new DataTableMapping("Table3", "Subject"));
            DataSet ds = DGeneric.RunSP_ReturnDataSet("sp_GetOnlineTestById", sqlParameterList, dataTableMappingList);

            if (ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        switch (dt.TableName)
                        {
                            case "OnlineTest":
                                onlineTestViewModelData = DGeneric.BindDataList<OnlineTestViewModel>(dt).FirstOrDefault();
                                onlineTestViewModelData.StreamID = dt.Rows[0]["StreamID"].ToString().Split(',').Select(int.Parse).ToArray();
                                //Vaibhav Changed because of empty data  
                                // onlineTestViewModelData.CourseID = dt.Rows[0]["CourseID"].ToString().Split(',').Select(int.Parse).ToArray();
                                // onlineTestViewModelData.BatchID = dt.Rows[0]["BatchID"].ToString().Split(',').Select(int.Parse).ToArray();
                                onlineTestViewModelData.CourseID = dt.Rows[0]["CourseID"].ToString() != string.Empty ? dt.Rows[0]["CourseID"].ToString().Split(',').Select(int.Parse).ToArray() : null;
                                onlineTestViewModelData.BatchID = dt.Rows[0]["BatchID"].ToString() != string.Empty ? dt.Rows[0]["BatchID"].ToString().Split(',').Select(int.Parse).ToArray() : null;
                                onlineTestViewModelData.SubjectID = dt.Rows[0]["SubjectID"].ToString() != string.Empty ? dt.Rows[0]["SubjectID"].ToString().Split(',').Select(int.Parse).ToArray() : null;
                                
                                //onlineTestViewModelData.StartDate = Convert.ToString(dt.Rows[0]["StartDate"]).ConvertDateTimeToString();
                                //onlineTestViewModelData.EndDate = Convert.ToString(dt.Rows[0]["EndDate"]).ConvertDateTimeToString();
                                break;
                            case "Course":
                                onlineTestViewModelData.Course = DGeneric.BindDataList<CourseViewModel>(dt);
                                break;
                            case "Batch":
                                onlineTestViewModelData.Batch = DGeneric.BindDataList<BatchViewModel>(dt);
                                break;
                            case "Subject":
                                onlineTestViewModelData.Subject = DGeneric.BindDataList<SubjectViewModel>(dt);
                                break;
                        }
                    }
                }
            }
            return onlineTestViewModelData;
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow item in dt.Rows)
            //    {
            //        onlineTestViewModelData.OnlineTestID = Convert.ToInt32(item["OnlineTestID"]);
            //        onlineTestViewModelData.OnlineTestNo = item["OnlineTestNo"].ToString();
            //        onlineTestViewModelData.TestSeriesID = Convert.ToInt32(item["TestSeriesID"]);
            //        onlineTestViewModelData.TestTypeID = Convert.ToInt32(item["TestTypeID"]);
            //        onlineTestViewModelData.TestName = item["TestName"].ToString();
            //        onlineTestViewModelData.TestDuration = item["TestDuration"].ToString();
            //        onlineTestViewModelData.SessionID = Convert.ToInt32(item["SessionID"]);
            //        onlineTestViewModelData.StreamID = item["StreamID"].ToString().Split(',').Cast<int>().ToArray();
            //        onlineTestViewModelData.CourseID = item["CourseID"].ToString().Split(',').Cast<int>().ToArray();
            //        onlineTestViewModelData.BatchID = item["BatchID"].ToString().Split(',').Cast<int>().ToArray();
            //        onlineTestViewModelData.SubjectID = Convert.ToInt32(item["SubjectID"]);
            //        onlineTestViewModelData.Topic = item["Topic"].ToString();
            //        onlineTestViewModelData.Instructions = item["Instructions"].ToString();
            //        onlineTestViewModelData.TestMarks = item["TestMarks"].ToString();
            //        onlineTestViewModelData.PassingPercentage = item["PassingPercentage"].ToString();
            //        onlineTestViewModelData.IsNegativeMarking = Convert.ToBoolean(item["IsNegativeMarking"]);
            //        onlineTestViewModelData.IsVisible = Convert.ToBoolean(item["IsVisible"]);
            //        onlineTestViewModelData.StartTime = item["StartTime"].ToString();
            //        onlineTestViewModelData.EndTime = item["EndTime"].ToString();
            //        onlineTestViewModelData.StartDate = Convert.ToDateTime(item["StartDate"]);
            //        onlineTestViewModelData.EndDate = Convert.ToDateTime(item["EndDate"]);
            //    }
            //}
            //return onlineTestViewModelData;
            //return ds.Tables[0].AsEnumerable().Select(s => new OnlineTestViewModel()
            //{
            //    OnlineTestID = Convert.ToInt32(s["OnlineTestID"]),
            //    OnlineTestNo = s["OnlineTestNo"].ToString(),
            //    TestSeriesID = Convert.ToInt32(s["TestSeriesID"]),
            //    TestTypeID = Convert.ToInt32(s["TestTypeID"]),
            //    TestName = s["TestName"].ToString(),
            //    TestDuration = s["TestDuration"].ToString(),
            //    SessionID = Convert.ToInt32(s["SessionID"]),
            //    StreamID = s["StreamID"].ToString().Split(',').Cast<int>().ToArray(),
            //    CourseID = s["CourseID"].ToString().Split(',').Cast<int>().ToArray(),
            //    BatchID = s["BatchID"].ToString().Split(',').Cast<int>().ToArray(),
            //    SubjectID = Convert.ToInt32(s["SubjectID"]),
            //    Topic = s["Topic"].ToString(),
            //    Instructions = s["Instructions"].ToString(),
            //    TestMarks = s["TestMarks"].ToString(),
            //    PassingPercentage = s["PassingPercentage"].ToString(),
            //    IsNegativeMarking = Convert.ToBoolean(s["IsNegativeMarking"]),
            //    IsVisible = Convert.ToBoolean(s["IsVisible"]),
            //    StartTime = s["StartTime"].ToString(),
            //    EndTime = s["EndTime"].ToString(),
            //    StartDate = Convert.ToDateTime(s["StartDate"]),
            //    EndDate = Convert.ToDateTime(s["EndDate"])
            //}).FirstOrDefault();
        }
        public QuizViewModel GetQuestionsByTestId(int OnlineTestID)
        {
            List<SqlParameter> paramerterList = new List<SqlParameter>();
            paramerterList.Add(new SqlParameter("@OnlineTestID", OnlineTestID));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetQuiz", paramerterList, null).Tables[0];
            QuizViewModel quizViewModel = new QuizViewModel();
            quizViewModel.Questions = new List<QuestionViewModel>();
            if (dt.Rows.Count > 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToInt32(dr["OnlineTestID"]) != quizViewModel.OnlineTestID)
                    {
                        quizViewModel.OnlineTestID = Convert.ToInt32(dr["OnlineTestID"]);
                        quizViewModel.TestName = dr["TestName"].ToString();
                        quizViewModel.TestDuration = dr["TestDuration"].ToString();
                        quizViewModel.OnlineTestNo = Convert.ToInt32(dr["OnlineTestNo"]);
                        quizViewModel.TestSeries = dr["TestSeries"].ToString();
                        quizViewModel.TestType = dr["TestType"].ToString();
                        quizViewModel.TotalMarks = dr["TotalMarks"].ToString().Replace(" ", "");
                        quizViewModel.PassingPercentage = dr["PassingPercentage"].ToString().Replace(" ", "");

                        QuestionViewModel questionViewModel = new QuestionViewModel();
                        questionViewModel.QuestionID = Convert.ToInt32(dr["QuestionId"]);
                        questionViewModel.TestQuestionNo = dr["TestQuestionNo"].ToString();
                        questionViewModel.Subject = dr["Subject"].ToString();
                        questionViewModel.Image_English = ConfigurationManager.AppSettings["BaseURL"].ToString() + "/" + dr["Image_English"];
                        questionViewModel.Image_Hindi = ConfigurationManager.AppSettings["BaseURL"].ToString() + "/" + dr["Image_Hindi"];
                        questionViewModel.CorrectAnswer = dr["Answer"].ToString();
                        quizViewModel.Questions.Add(questionViewModel);
                    }
                    else
                    {
                        QuestionViewModel questionViewModel = new QuestionViewModel();
                        questionViewModel.QuestionID = Convert.ToInt32(dr["QuestionId"]);
                        questionViewModel.TestQuestionNo = dr["TestQuestionNo"].ToString();
                        questionViewModel.Subject = dr["Subject"].ToString();
                        questionViewModel.Image_English = ConfigurationManager.AppSettings["BaseURL"].ToString() + "/" + dr["Image_English"];
                        questionViewModel.Image_Hindi = ConfigurationManager.AppSettings["BaseURL"].ToString() + "/" + dr["Image_Hindi"];
                        questionViewModel.CorrectAnswer = dr["Answer"].ToString();
                        quizViewModel.Questions.Add(questionViewModel);
                    }
                }
                return quizViewModel;
            }
            else
                return quizViewModel = null;
        }
        public List<StudentOnlineTestViewModel> GetOnlineTestByStudentID(int StudentID)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@StudentID", StudentID));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetOnlineTestByStudentID", sqlParameterList, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<StudentOnlineTestViewModel>(dt);
            else
                return new List<StudentOnlineTestViewModel>();
            //return dt.AsEnumerable().Select(s => new StudentOnlineTestViewModel()
            //{
            //    EligibleStudentID = Convert.ToInt32(s["EligibleStudentID"]),
            //    StudentID = Convert.ToInt32(s["StudentID"]),
            //    OnlineTestID = Convert.ToInt32(s["OnlineTestID"]),
            //    OnlineTestNo = s["OnlineTestNo"].ToString(),
            //    TestSeriesID = Convert.ToInt32(s["TestSeriesID"]),
            //    TestTypeID = Convert.ToInt32(s["TestTypeID"]),
            //    TestSeriesName = s["TestSeries"].ToString(),
            //    TestTypeName = s["TestType"].ToString(),
            //    TestName = s["TestName"].ToString(),
            //    TestDuration = s["TestDuration"].ToString(),
            //    SessionID = Convert.ToInt32(s["SessionID"]),
            //    //StreamID = Convert.ToInt32(s["StreamID"]),
            //    //CourseID = Convert.ToInt32(s["CourseID"]),
            //    //BatchID = Convert.ToInt32(s["BatchID"]),
            //    SubjectID = Convert.ToInt32(s["SubjectID"]),
            //    Topic = s["Topic"].ToString(),
            //    Instructions = s["Instructions"].ToString(),
            //    TestMarks = s["TestMarks"].ToString(),
            //    PassingPercentage = s["PassingPercentage"].ToString(),
            //    IsNegativeMarking = Convert.ToBoolean(s["IsNegativeMarking"]),
            //    IsVisible = Convert.ToBoolean(s["IsVisible"]),
            //    StartTime = s["StartTime"].ToString(),
            //    EndTime = s["EndTime"].ToString(),
            //    StartDate = Convert.ToDateTime(s["StartDate"]),
            //    EndDate = Convert.ToDateTime(s["EndDate"]),
            //    IsActive = Convert.ToBoolean(s["IsActive"])
            //}).ToList();
        }
        public List<OnlineTestViewModel> GetOnlineTest_ForGenerateResult()
        {
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetOnlineTestForGenerateResult", null, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<OnlineTestViewModel>(dt);
            else
                return new List<OnlineTestViewModel>();
            //return dt.AsEnumerable().Select(s => new OnlineTestViewModel()
            //{
            //    OnlineTestID = Convert.ToInt32(s["OnlineTestID"]),
            //    OnlineTestNo = s["OnlineTestNo"].ToString(),
            //    TestSeriesID = Convert.ToInt32(s["TestSeriesID"]),
            //    TestSeriesName = s["TestSeries"].ToString(),
            //    TestTypeID = Convert.ToInt32(s["TestTypeID"]),
            //    TestTypeName = s["TestType"].ToString(),
            //    TestName = s["TestName"].ToString(),
            //    TestDuration = s["TestDuration"].ToString(),
            //    SessionID = Convert.ToInt32(s["SessionID"]),
            //    //StreamID = s["StreamID"].ToString().Split(',').ToArray(),
            //    //CourseID = Convert.ToInt32(s["CourseID"]),
            //    //BatchID = Convert.ToInt32(s["BatchID"]),
            //    SubjectID = Convert.ToInt32(s["SubjectID"]),
            //    Topic = s["Topic"].ToString(),
            //    Instructions = s["Instructions"].ToString(),
            //    TestMarks = s["TestMarks"].ToString(),
            //    PassingPercentage = s["PassingPercentage"].ToString(),
            //    IsNegativeMarking = Convert.ToBoolean(s["IsNegativeMarking"]),
            //    IsVisible = Convert.ToBoolean(s["IsVisible"]),
            //    StartTime = s["StartTime"].ToString(),
            //    EndTime = s["EndTime"].ToString(),
            //    StartDate = Convert.ToDateTime(s["StartDate"]),
            //    EndDate = Convert.ToDateTime(s["EndDate"])
            //}).ToList();
        }
    }
}
