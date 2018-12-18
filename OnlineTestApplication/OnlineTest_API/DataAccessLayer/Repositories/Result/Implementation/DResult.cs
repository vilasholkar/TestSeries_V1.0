using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Result;

namespace DataAccessLayer
{
    public class DResult : IDResult
    {
        int TestID = 0;
        int TotalMarks = 0;
        string QualifyingMarks = string.Empty;

        public ResultAnalysisViewModel GetResultAnalysis(int StudentID, int TestID)
        {
            ResultAnalysisViewModel objResultAnalysis = new ResultAnalysisViewModel();
            objResultAnalysis.PaperAnalysis = GetPaperAnalysis(TestID);
            objResultAnalysis.StudentAttempt = GetStudentAttempt(StudentID, TestID);
            objResultAnalysis.OnlineTestResult = GetOnlineTestResultByID(StudentID, TestID);
            objResultAnalysis.Topper_Average = GetTopper_Average(TestID);
            objResultAnalysis.StudentRank = StudentRank(Convert.ToInt32( objResultAnalysis.OnlineTestResult[0].TotalMarksObtained),Convert.ToString( objResultAnalysis.OnlineTestResult[0].StudentCaste));
            return objResultAnalysis;
        }
        public PaperAnalysisViewModel GetPaperAnalysis(int TestID)
        {
            string query = string.Format("select * from PaperAnalysis where TestID = {0} ", TestID);
            DataRow dr = DGeneric.GetData(query).Tables[0].Rows[0];
            PaperAnalysisViewModel obj = new PaperAnalysisViewModel();

            obj.PaperAnalysisID = Convert.ToInt32(dr["PaperAnalysisID"]);
            obj.TestID = Convert.ToInt32(dr["TestID"]);
            obj.TotalEasy = dr["TotalEasy"].ToString();
            obj.TotalMedium = dr["TotalMedium"].ToString();
            obj.TotalDifficult = dr["TotalDifficult"].ToString();
            obj.EasyQuestionList = dr["EasyQuestionList"].ToString();
            obj.MediumQuestionList = dr["MediumQuestionList"].ToString();
            obj.DifficultQuestionList = dr["DifficultQuestionList"].ToString();

            return obj;

            //return dt.AsEnumerable().Select(s => new PaperAnalysisViewModel()
            //{
            //    PaperAnalysisID = Convert.ToInt32(s["OnlineTestID"]),
            //    TestID = Convert.ToInt32(s["OnlineTestID"]),
            //    TotalEasy = s["OnlineTestNo"].ToString(),
            //    TotalMedium = s["TestSeries"].ToString(),
            //    TotalDifficult = s["TestType"].ToString(),
            //    EasyQuestionList = s["TestName"].ToString(),
            //    MediumQuestionList = s["TestDuration"].ToString(),
            //    DifficultQuestionList = s["SessionID"].ToString()
            //}).ToList();
        }
        public StudentAttemptViewModel GetStudentAttempt(int StudentID, int TestID)
        {
            string query = string.Format("select * from StudentAttempt where StudentID= {0} and TestID = {1} ", StudentID, TestID);
            DataRow dr = DGeneric.GetData(query).Tables[0].Rows[0];
            StudentAttemptViewModel obj = new StudentAttemptViewModel();
            obj.StudentAttemptID = Convert.ToInt32(dr["StudentAttemptID"]);
            obj.TestID = Convert.ToInt32(dr["TestID"]);
            obj.StudentID = Convert.ToInt32(dr["StudentID"]);
            obj.EasyCorrect = Convert.ToString(dr["EasyCorrect"]);
            obj.EasyInCorrect = Convert.ToString(dr["EasyInCorrect"]);
            obj.EasyNotAttempt = Convert.ToString(dr["EasyNotAttempt"]);
            obj.MediumCorrect = Convert.ToString(dr["MediumCorrect"]);
            obj.MediumInCorrect = Convert.ToString(dr["MediumInCorrect"]);
            obj.MediumNotAttempt = Convert.ToString(dr["MediumNotAttempt"]);
            obj.DifficultCorrect = Convert.ToString(dr["DifficultCorrect"]);
            obj.DifficultInCorrect = Convert.ToString(dr["DifficultInCorrect"]);
            obj.DifficultNotAttempt = Convert.ToString(dr["DifficultNotAttempt"]);

            return obj;
        }
        public List<OnlineTestResultViewModel> GetOnlineTestResultByID(int StudentID, int TestID)
        {
            string basicquery = @"SELECT     dbo.OnlineTestResult.ResultID, dbo.OnlineTestResult.StudentID, dbo.OnlineTestResult.TestID, dbo.OnlineTestResult.Physics_Total, 
                      dbo.OnlineTestResult.Physics_Right, dbo.OnlineTestResult.Physics_Wrong, dbo.OnlineTestResult.Chemistry_Total, dbo.OnlineTestResult.Chemistry_Right, 
                      dbo.OnlineTestResult.Chemistry_Wrong, dbo.OnlineTestResult.Biology_Total, dbo.OnlineTestResult.Biology_Right, dbo.OnlineTestResult.Biology_Wrong, 
                      dbo.OnlineTestResult.TotalCorrect, dbo.OnlineTestResult.TotalWrong, dbo.OnlineTestResult.TotalAttempt, dbo.OnlineTestResult.TotalMarksObtained, 
                      dbo.OnlineTestResult.Percentage, dbo.OnlineTestResult.Rank, dbo.OnlineTestResult.TotalMarks, dbo.OnlineTestResult.QualifyingMarks, 
                      dbo.OnlineTestResult.CreatedOnDate, dbo.OnlineTestResult.IsActive, dbo.Student.EnrollmentNo, dbo.Student.FirstName, dbo.Student.LastName, 
                      dbo.Student.MobileNumber,dbo.Student.Cast, dbo.OnlineTest.OnlineTestNo, dbo.OnlineTest.TestSeriesID, dbo.OnlineTest.TestTypeID, dbo.OnlineTest.TestName,  dbo.OnlineTest.StartDate,
                      dbo.OnlineTest.TestDuration, dbo.TestSeries.TestSeries, dbo.TestType.TestType
                        FROM dbo.OnlineTestResult LEFT OUTER JOIN
                      dbo.OnlineTest ON dbo.OnlineTestResult.TestID = dbo.OnlineTest.OnlineTestID LEFT OUTER JOIN
                      dbo.Student ON dbo.OnlineTestResult.StudentID = dbo.Student.StudentID LEFT OUTER JOIN
                      dbo.TestSeries ON dbo.OnlineTest.TestSeriesID = dbo.TestSeries.TestSeriesID LEFT OUTER JOIN
                      dbo.TestType ON dbo.OnlineTest.TestTypeID = dbo.TestType.TestTypeID";
            string strquery = string.Empty;
            if (TestID > 0 && StudentID == 0)
            {
                strquery = string.Format("{0} where OnlineTestResult.TestID={1} ", basicquery, TestID);
            }
            else if (TestID == 0 && StudentID > 0)
            {
                strquery = string.Format("{0} where OnlineTestResult.StudentID = {1} ", basicquery, StudentID);
            }
            else if (TestID > 0 && StudentID > 0)
            {
                strquery = string.Format("{0} where OnlineTestResult.StudentID= {1} and OnlineTestResult.TestID = {2} ", basicquery, StudentID, TestID);
            }

            DataTable dt = DGeneric.GetData(strquery).Tables[0];
            return dt.AsEnumerable().Select(s => new OnlineTestResultViewModel()
            {
                ResultID = Convert.ToInt32(s["ResultID"]),
                TestID = Convert.ToInt32(s["TestID"]),
                StudentID = Convert.ToInt32(s["StudentID"]),
                EnrollmentNo = Convert.ToString(s["EnrollmentNo"]),
                StudentName = Convert.ToString(s["FirstName"] + " " + s["LastName"]),
                StudentCaste = Convert.ToString(s["Cast"]),
                TestName = Convert.ToString(s["TestName"]),
                TestDate = Convert.ToDateTime(s["StartDate"]),
                TestSeriesName = Convert.ToString(s["TestSeries"]),
                TestTypeName = Convert.ToString(s["TestType"]),
                Physics_Total = Convert.ToString(s["Physics_Total"]),
                Physics_Right = Convert.ToString(s["Physics_Right"]),
                Physics_Wrong = Convert.ToString(s["Physics_Wrong"]),
                Chemistry_Total = Convert.ToString(s["Chemistry_Total"]),
                Chemistry_Right = Convert.ToString(s["Chemistry_Right"]),
                Chemistry_Wrong = Convert.ToString(s["Chemistry_Wrong"]),
                Biology_Total = Convert.ToString(s["Biology_Total"]),
                Biology_Right = Convert.ToString(s["Biology_Right"]),
                Biology_Wrong = Convert.ToString(s["Biology_Wrong"]),
                TotalCorrect = Convert.ToString(s["TotalCorrect"]),
                TotalWrong = Convert.ToString(s["TotalWrong"]),
                TotalAttempt = Convert.ToString(s["TotalAttempt"]),
                TotalMarksObtained = Convert.ToString(s["TotalMarksObtained"]),
                Percentage = Convert.ToString(s["Percentage"]),
                Rank = Convert.ToString(s["Rank"]),
                TotalMarks = Convert.ToString(s["TotalMarks"]),
                QualifyingMarks = Convert.ToString(s["QualifyingMarks"]),
                CreatedOnDate = Convert.ToDateTime(s["CreatedOnDate"]),
                IsActive = Convert.ToBoolean(s["IsActive"])
            }).ToList();
        }
        public List<Topper_AverageViewModel> GetTopper_Average(int TestID)
        {
            string query = string.Format("select * from Topper_Average where TestID = {0} ", TestID);
            DataTable dt = DGeneric.GetData(query).Tables[0];
            return dt.AsEnumerable().Select(s => new Topper_AverageViewModel()
            {
                Topper_AverageID = Convert.ToInt32(s["Topper_AverageID"]),
                Topper_Average = Convert.ToString(s["Topper_Average"]),
                TestID = Convert.ToInt32(s["TestID"]),
                Physics_Right = Convert.ToString(s["Physics_Right"]),
                Physics_Wrong = Convert.ToString(s["Physics_Wrong"]),
                Chemistry_Right = Convert.ToString(s["Chemistry_Right"]),
                Chemistry_Wrong = Convert.ToString(s["Chemistry_Wrong"]),
                Biology_Right = Convert.ToString(s["Biology_Right"]),
                Biology_Wrong = Convert.ToString(s["Biology_Wrong"]),
                TotalCorrect = Convert.ToString(s["TotalCorrect"]),
                TotalWrong = Convert.ToString(s["TotalWrong"]),
                TotalAttempt = Convert.ToString(s["TotalAttempt"]),
                TotalMarksObtained = Convert.ToString(s["TotalMarksObtained"]),
                Percentage = Convert.ToString(s["Percentage"]),
            }).ToList();
        }
        public StudentRankViewModel StudentRank(int StudentMarks, string StudentCategory)
        {
            DataTable dtStudentRank = new DataTable();

            DataTable dtRankList = DGeneric.GetData("select * from RankList").Tables[0];
            List<RankViewModel> objRankList = ConvertDataTable<RankViewModel>(dtRankList);

            //int StudentMarks = 360;
            //Model.MarksReviewList.Find(x => x.RollNo == data.RollNo).TotalMarks;
           // string StudentCategory = "OBC";
            //Model.StudentList.Find(x => x.RollNo == data.RollNo).Category;
            int AIR_UR = 0, SR_UR = 0, AIR_CAT_RANK = 0, SR_CAT_RANK = 0;
            AIR_UR = objRankList.OrderBy(item => Math.Abs(StudentMarks - item.AIR_UR_S)).First().AIR_UR;
            //AIR_UR = Model.RankList.OrderBy(item => Math.Abs(StudentMarks - item.AIR_UR_S)).First().AIR_UR;
            SR_UR = objRankList.OrderBy(item => Math.Abs(StudentMarks - item.SR_UR_S)).First().SR_UR;

            if (StudentCategory == "OBC")
            {
                AIR_CAT_RANK = objRankList.OrderBy(item => Math.Abs(StudentMarks - item.AIR_OBC_S)).First().AIR_OBC;
                SR_CAT_RANK = objRankList.OrderBy(item => Math.Abs(StudentMarks - item.SR_OBC_S)).First().SR_OBC;
            }
            else if (StudentCategory == "SC")
            {
                AIR_CAT_RANK = objRankList.OrderBy(item => Math.Abs(StudentMarks - item.AIR_SC_S)).First().AIR_SC;
                SR_CAT_RANK = objRankList.OrderBy(item => Math.Abs(StudentMarks - item.SR_SC_S)).First().SR_SC;
            }
            else if (StudentCategory == "ST")
            {
                AIR_CAT_RANK = objRankList.OrderBy(item => Math.Abs(StudentMarks - item.AIR_ST_S)).First().AIR_ST;
                SR_CAT_RANK = objRankList.OrderBy(item => Math.Abs(StudentMarks - item.SR_ST_S)).First().SR_ST;
            }
            else
            {
                AIR_CAT_RANK = objRankList.OrderBy(item => Math.Abs(StudentMarks - item.AIR_UR_S)).First().AIR_UR;
                SR_CAT_RANK = objRankList.OrderBy(item => Math.Abs(StudentMarks - item.SR_UR_S)).First().SR_UR;
            }
            StudentRankViewModel objRank = new StudentRankViewModel();
            objRank.AIR_UR = AIR_UR;
            objRank.SR_UR = SR_UR;
            objRank.AIR_CAT_RANK = AIR_CAT_RANK;
            objRank.SR_CAT_RANK = SR_CAT_RANK;

            return objRank;
        }


        #region Generate Result Analysis

        public string ResultAnalysis(int TestID)
        {
            this.TestID = TestID;
            DataRow dr = GetOnlineTestByID(TestID);
            TotalMarks = Convert.ToInt32(dr["TestMarks"]);
            QualifyingMarks = Convert.ToString(dr["PassingPercentage"]);
            DataTable dtEligibleStudent = GetEligibleStudentByTestID(TestID);
            DataTable dtStudentResponse = GetStudentResponseByTestID(TestID);
            if (dtStudentResponse.Rows.Count != 0)
            {
                int TotalEasy = 0, TotalMedium = 0, TotalDifficult = 0;
                string EasyQuestionList = string.Empty, MediumQuestionList = string.Empty, DifficultQuestionList = string.Empty;
                DataTable dtPaperAnalysis = PaperAnalysis(dtStudentResponse, out TotalEasy, out TotalMedium, out TotalDifficult, out EasyQuestionList, out MediumQuestionList, out DifficultQuestionList);
                DataTable dtStudentAttempt = StudentAttempt(dtStudentResponse, dtPaperAnalysis);
                DataTable dtStudentMarksReview = MarksReview(dtStudentResponse, dtPaperAnalysis);
                DataTable dtTopper_Average = Topper_Average(dtStudentMarksReview);
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("@TestID", TestID));
                sqlParameterList.Add(new SqlParameter("@TotalEasy", TotalEasy));
                sqlParameterList.Add(new SqlParameter("@TotalMedium", TotalMedium));
                sqlParameterList.Add(new SqlParameter("@TotalDifficult", TotalDifficult));
                sqlParameterList.Add(new SqlParameter("@EasyQuestionList", EasyQuestionList));
                sqlParameterList.Add(new SqlParameter("@MediumQuestionList", MediumQuestionList));
                sqlParameterList.Add(new SqlParameter("@DifficultQuestionList", DifficultQuestionList));
                string temp = DGeneric.RunSP_ExecuteNonQuery("sp_AddPaperAnalysis", sqlParameterList);

                List<SqlParameter> sqlParameterList1 = new List<SqlParameter>();
                sqlParameterList1.Add(new SqlParameter("@StudentAttempt", dtStudentAttempt));
                string temp1 = DGeneric.RunSP_ExecuteNonQuery("sp_AddStudentAttempt", sqlParameterList1);

                List<SqlParameter> sqlParameterList2 = new List<SqlParameter>();
                sqlParameterList2.Add(new SqlParameter("@OnlineTestResult", dtStudentMarksReview));
                string temp2 = DGeneric.RunSP_ExecuteNonQuery("sp_AddOnlineTestResult", sqlParameterList2);

                List<SqlParameter> sqlParameterList3 = new List<SqlParameter>();
                sqlParameterList3.Add(new SqlParameter("@Topper_Average", dtTopper_Average));
                string temp3 = DGeneric.RunSP_ExecuteNonQuery("sp_AddTopper_Average", sqlParameterList3);

                return "Result Generated Sucessfully.";
            }
            else {
                return "No student had given test.";
            }
            
        }
        public DataRow GetOnlineTestByID(int TestID)
        {
            string query = string.Format("select * from OnlineTest where OnlineTestID={0} ", TestID);
            DataRow dr = DGeneric.GetData(query).Tables[0].Rows[0];
            return dr;
        }
        public DataTable GetEligibleStudentByTestID(int TestID)
        {
            string query = string.Format("select * from EligibleStudent where OnlineTestID={0} ", TestID);
            DataTable dt = DGeneric.GetData(query).Tables[0];
            return dt;
        }
        public DataTable GetStudentResponseByTestID(int TestID)
        {
            string query = string.Format("select * from StudentResponse where TestID={0} ", TestID);
            DataTable dt = DGeneric.GetData(query).Tables[0];
            return dt;
        }

        public DataTable PaperAnalysis(DataTable dtStudentResponse, out int TotalEasy, out int TotalMedium, out int TotalDifficult, out string EasyQuestionList, out string MediumQuestionList, out string DifficultQuestionList)
        {
            //  DataTable dtExcel = dtStudentResponse;

            DataTable dtCorrectCount = new DataTable();
            DataRow drCorrectCount;
            dtCorrectCount.Columns.Add("SubjectID", typeof(string));
            dtCorrectCount.Columns.Add("QuestionID", typeof(string));
            dtCorrectCount.Columns.Add("CorrectCount", typeof(string));
            dtCorrectCount.Columns.Add("Level", typeof(string));

            DataView view = new DataView(dtStudentResponse);
            DataTable dtDistinctQuestions = view.ToTable(true, "QuestionID", "SubjectID");
            int TotalQuestion = view.ToTable(true, "QuestionID").Rows.Count;
            int TotalStudents = view.ToTable(true, "StudentID").Rows.Count;

            int Easy = Convert.ToInt32(Math.Round((double)TotalStudents / 2));
            int Medium = Convert.ToInt32(Math.Round((double)TotalStudents / 3));
            int Difficult = Convert.ToInt32(Math.Round((double)TotalStudents / 5));
            int Count = 0;

            foreach (DataRow dr in dtDistinctQuestions.Rows)
            {
                Count = dtStudentResponse.AsEnumerable().Where(a => Convert.ToInt32(a["QuestionID"]) == Convert.ToInt32(dr["QuestionID"]) && Convert.ToBoolean(a["IsCorrect"]) == true).Count();

                drCorrectCount = dtCorrectCount.NewRow();
                drCorrectCount["SubjectID"] = dr["SubjectID"];
                drCorrectCount["QuestionID"] = dr["QuestionID"];
                drCorrectCount["CorrectCount"] = Count;
                drCorrectCount["Level"] = Count > Easy ? "Easy" : Count > Medium ? "Medium" : "Difficult";
                dtCorrectCount.Rows.Add(drCorrectCount);
                Count = 0;
            }

            TotalEasy = 0; TotalMedium = 0; TotalDifficult = 0;
            StringBuilder builderEasy = new StringBuilder();
            StringBuilder builderMedium = new StringBuilder();
            StringBuilder builderDifficult = new StringBuilder();
            for (int i = 0; i < dtCorrectCount.Rows.Count; i++)
            {
                if (dtCorrectCount.Rows[i]["Level"].Equals("Easy"))
                {
                    TotalEasy++;
                    builderEasy.Append(dtCorrectCount.Rows[i]["QuestionID"]).Append(",");

                }
                else if (dtCorrectCount.Rows[i]["Level"].Equals("Medium"))
                {
                    TotalMedium++;
                    builderMedium.Append(dtCorrectCount.Rows[i]["QuestionID"]).Append(",");

                }
                else if (dtCorrectCount.Rows[i]["Level"].Equals("Difficult"))
                {
                    TotalDifficult++;
                    builderDifficult.Append(dtCorrectCount.Rows[i]["QuestionID"]).Append(",");

                }
            }
            EasyQuestionList = builderEasy.Length > 0 ? builderEasy.Remove(builderEasy.Length - 1, 1).ToString() : string.Empty;
            MediumQuestionList = builderMedium.Length > 0 ? builderMedium.Remove(builderMedium.Length - 1, 1).ToString() : string.Empty;
            DifficultQuestionList = builderDifficult.Length > 0 ? builderDifficult.Remove(builderDifficult.Length - 1, 1).ToString() : string.Empty;
            // string TotalList = EasyQuestionList + "," + MediumQuestionList + "," + DifficultQuestionList;
            int TotalQuestion1 = TotalEasy + TotalMedium + TotalDifficult;

            return dtCorrectCount;
        }

        public DataTable StudentAttempt(DataTable dtStudentResponse, DataTable dtPaper)
        {
            DataTable dtDistinctStudent = dtStudentResponse.AsDataView().ToTable(true, "StudentID");

            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID", typeof(string));
            dt.Columns.Add("TestID", typeof(string));
            dt.Columns.Add("EasyCorrect", typeof(string));
            dt.Columns.Add("EasyInCorrect", typeof(string));
            dt.Columns.Add("EasyNotAttempt", typeof(string));
            dt.Columns.Add("MediumCorrect", typeof(string));
            dt.Columns.Add("MediumInCorrect", typeof(string));
            dt.Columns.Add("MediumNotAttempt", typeof(string));
            dt.Columns.Add("DifficultCorrect", typeof(string));
            dt.Columns.Add("DifficultInCorrect", typeof(string));
            dt.Columns.Add("DifficultNotAttempt", typeof(string));
            DataRow dr;

            foreach (DataRow drDistinctStudent in dtDistinctStudent.Rows)
            {
                int EC = 0, EIC = 0, ENA = 0, MC = 0, MIC = 0, MNA = 0, DC = 0, DIC = 0, DNA = 0;
                string StudentID = drDistinctStudent["StudentID"].ToString();

                foreach (DataRow drPaper in dtPaper.Rows)
                {
                    foreach (DataRow drStudentResponse in dtStudentResponse.Rows)
                    {
                        if (drPaper["QuestionID"].ToString().Equals(drStudentResponse["QuestionID"].ToString()) && Convert.ToString(drStudentResponse["StudentID"]).Equals(StudentID))
                        {
                            if (drPaper["Level"].ToString().Equals("Easy"))
                            {
                                if (Convert.ToBoolean(drStudentResponse["IsCorrect"]))
                                {
                                    EC++;
                                }
                                else if (!(Convert.ToBoolean(drStudentResponse["IsCorrect"])) && Convert.ToInt32(drStudentResponse["AnswerID"]) != 0)
                                {
                                    EIC++;
                                }
                                else
                                {
                                    ENA++;
                                }
                            }
                            else if (drPaper["Level"].ToString().Equals("Medium"))
                            {
                                if (Convert.ToBoolean(drStudentResponse["IsCorrect"]))
                                {
                                    MC++;
                                }
                                else if (!(Convert.ToBoolean(drStudentResponse["IsCorrect"])) && Convert.ToInt32(drStudentResponse["AnswerID"]) != 0)
                                {
                                    MIC++;
                                }
                                else
                                {
                                    MNA++;
                                }
                            }
                            else if (drPaper["Level"].ToString().Equals("Difficult"))
                            {
                                if (Convert.ToBoolean(drStudentResponse["IsCorrect"]))
                                {
                                    DC++;
                                }
                                else if (!(Convert.ToBoolean(drStudentResponse["IsCorrect"])) && Convert.ToInt32(drStudentResponse["AnswerID"]) != 0)
                                {
                                    DIC++;
                                }
                                else
                                {
                                    DNA++;
                                }
                            }
                            break;
                        }
                    }
                }
                dr = dt.NewRow();
                dr["StudentID"] = StudentID;
                dr["TestID"] = TestID;
                dr["EasyCorrect"] = EC;
                dr["EasyInCorrect"] = EIC;
                dr["EasyNotAttempt"] = ENA;
                dr["MediumCorrect"] = MC;
                dr["MediumInCorrect"] = MIC;
                dr["MediumNotAttempt"] = MNA;
                dr["DifficultCorrect"] = DC;
                dr["DifficultInCorrect"] = DIC;
                dr["DifficultNotAttempt"] = DNA;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable MarksReview(DataTable dtStudentResponse, DataTable dtPaper)
        {
            DataTable dtDistinctStudent = dtStudentResponse.AsDataView().ToTable(true, "StudentID");

            DataTable dtTestResult = virtualTable();
            DataRow dr;

            foreach (DataRow drDistinctStudent in dtDistinctStudent.Rows)
            {
                int PR = 0, PW = 0, PNA = 0, CR = 0, CW = 0, CNA = 0, BR = 0, BW = 0, BNA = 0;
                // int TotalRight = 0, TotalWrong = 0, Attempt = 0, NotAttempt = 0;
                string StudentID = drDistinctStudent["StudentID"].ToString();

                foreach (DataRow drPaper in dtPaper.Rows)
                {
                    foreach (DataRow drStudentResponse in dtStudentResponse.Rows)
                    {
                        if (drPaper["QuestionID"].ToString().Equals(drStudentResponse["QuestionID"].ToString()) && Convert.ToString(drStudentResponse["StudentID"]).Equals(StudentID))
                        {
                            if (drPaper["SubjectID"].ToString().Equals("1"))
                            {
                                if (Convert.ToBoolean(drStudentResponse["IsCorrect"]))
                                {
                                    PR++;
                                }
                                else if (!(Convert.ToBoolean(drStudentResponse["IsCorrect"])) && Convert.ToInt32(drStudentResponse["AnswerID"]) != 0)
                                {
                                    PW++;
                                }
                                else
                                {
                                    PNA++;
                                }
                            }
                            else if (drPaper["SubjectID"].ToString().Equals("2"))
                            {
                                if (Convert.ToBoolean(drStudentResponse["IsCorrect"]))
                                {
                                    CR++;
                                }
                                else if (!(Convert.ToBoolean(drStudentResponse["IsCorrect"])) && Convert.ToInt32(drStudentResponse["AnswerID"]) != 0)
                                {
                                    CW++;
                                }
                                else
                                {
                                    CNA++;
                                }
                            }
                            else if (drPaper["SubjectID"].ToString().Equals("3"))
                            {
                                if (Convert.ToBoolean(drStudentResponse["IsCorrect"]))
                                {
                                    BR++;
                                }
                                else if (!(Convert.ToBoolean(drStudentResponse["IsCorrect"])) && Convert.ToInt32(drStudentResponse["AnswerID"]) != 0)
                                {
                                    BW++;
                                }
                                else
                                {
                                    BNA++;
                                }
                            }
                            break;
                        }
                    }
                }
                dr = dtTestResult.NewRow();
                dr["StudentID"] = !string.IsNullOrEmpty(StudentID) ? StudentID : "0";
                dr["TestID"] = TestID;
                dr["Physics_Total"] = PR + PW + PNA;
                dr["Physics_Right"] = PR;
                dr["Physics_Wrong"] = PW;
                dr["Chemistry_Total"] = CR + CW + CNA;
                dr["Chemistry_Right"] = CR;
                dr["Chemistry_Wrong"] = CW;
                dr["Biology_Total"] = BR + BW + BNA;
                dr["Biology_Right"] = BR;
                dr["Biology_Wrong"] = BW;
                dr["TotalCorrect"] = PR + CR + BR;
                dr["TotalWrong"] = PW + CW + BW;
                dr["TotalAttempt"] = PR + CR + BR + PW + CW + BW;
                dr["TotalMarksObtained"] = ((PR + CR + BR) * 4) - (PW + CW + BW);
                dr["Percentage"] = Convert.ToInt32(dr["TotalMarksObtained"]) > 0 ? Math.Round((Convert.ToDouble(dr["TotalMarksObtained"]) / TotalMarks) * 100, 4) : 0;
                dr["Rank"] = 0;
                dr["TotalMArks"] = TotalMarks;
                dr["QualifyingMarks"] = QualifyingMarks;
                dr["CreatedOnDate"] = DGeneric.SystemDateTime;
                dr["IsActive"] = true;

                dtTestResult.Rows.Add(dr);
            }
            return dtTestResult;
        }

        public DataTable Topper_Average(DataTable dtTestResult)
        {
            DataTable dtTA = virtualTable();
            DataView view = dtTestResult.DefaultView;
            view.Sort = "TotalMarksObtained DESC";
            dtTestResult = view.ToTable();

            DataRow drTopper = dtTA.NewRow();
            int maxTotalMarks = Convert.ToInt32(dtTestResult.AsEnumerable().Max(x => x["TotalMarksObtained"]));
            var SameTopperList = dtTestResult.AsEnumerable().Where(x => Convert.ToInt32(x["TotalMarksObtained"]) == maxTotalMarks).ToList();
            if (SameTopperList.Count > 0)
            {
                int maxBiologyMarks = Convert.ToInt32(SameTopperList.AsEnumerable().Max(x => x["Biology_Right"]));
                var BiologyMarksList = SameTopperList.Where(x => Convert.ToInt32(x["Biology_Right"]) == maxBiologyMarks).ToList();
                if (BiologyMarksList.Count > 1)
                {
                    int maxChemistryMarks = Convert.ToInt32(BiologyMarksList.AsEnumerable().Max(x => x["Chemistry_Right"]));
                    var ChemistryMarksList = BiologyMarksList.Where(x => Convert.ToInt32(x["Chemistry_Right"]) == maxChemistryMarks).ToList();
                    if (ChemistryMarksList.Count > 1)
                    {
                        int maxPhysicsMarks = Convert.ToInt32(ChemistryMarksList.Max(x => x["Physics_Right"]));
                        var PhysicsMarksList = ChemistryMarksList.Where(x => Convert.ToInt32(x["Physics_Right"]) == maxPhysicsMarks).ToList();
                        if (PhysicsMarksList.Count >= 1)
                        {
                            drTopper = PhysicsMarksList.FirstOrDefault();
                        }
                    }
                    else
                    {
                        drTopper = ChemistryMarksList.FirstOrDefault();
                    }
                }
                else
                {
                    drTopper = BiologyMarksList.FirstOrDefault();
                }
            }
            dtTA.Rows.Add(drTopper.ItemArray);
            dtTA.Rows[0]["StudentID"] = "Topper";
            int TotalStudents = dtTestResult.Rows.Count;
            DataRow drAverage = dtTA.NewRow();
            drAverage["StudentID"] = "Average";
            drAverage["TestID"] = TestID;
            drAverage["Physics_Total"] = Convert.ToInt32(dtTestResult.Rows[0]["Physics_Total"]);
            //Convert.ToInt32(Convert.ToInt32(dtTestResult.Compute("Sum(Physics_Right)", string.Empty)) / TotalStudents);
            drAverage["Physics_Right"] = Convert.ToInt32(Convert.ToInt32(dtTestResult.Compute("Sum(Physics_Right)", string.Empty)) / TotalStudents);
            drAverage["Physics_Wrong"] = Convert.ToInt32(Convert.ToInt32(dtTestResult.Compute("Sum(Physics_Wrong)", string.Empty)) / TotalStudents);
            drAverage["Chemistry_Total"] = Convert.ToInt32(dtTestResult.Rows[0]["Physics_Total"]);
            drAverage["Chemistry_Right"] = Convert.ToInt32(Convert.ToInt32(dtTestResult.Compute("Sum(Chemistry_Right)", string.Empty)) / TotalStudents);
            drAverage["Chemistry_Wrong"] = Convert.ToInt32(Convert.ToInt32(dtTestResult.Compute("Sum(Chemistry_Wrong)", string.Empty)) / TotalStudents);
            drAverage["Biology_Total"] = Convert.ToInt32(dtTestResult.Rows[0]["Physics_Total"]);
            drAverage["Biology_Right"] = Convert.ToInt32(Convert.ToInt32(dtTestResult.Compute("Sum(Biology_Right)", string.Empty)) / TotalStudents);
            drAverage["Biology_Wrong"] = Convert.ToInt32(Convert.ToInt32(dtTestResult.Compute("Sum(Biology_Wrong)", string.Empty)) / TotalStudents);
            drAverage["TotalCorrect"] = Convert.ToInt32(Convert.ToInt32(dtTestResult.Compute("Sum(TotalCorrect)", string.Empty)) / TotalStudents);
            drAverage["TotalWrong"] = Convert.ToInt32(Convert.ToInt32(dtTestResult.Compute("Sum(TotalWrong)", string.Empty)) / TotalStudents);
            drAverage["TotalAttempt"] = Convert.ToInt32(Convert.ToInt32(dtTestResult.Compute("Sum(TotalAttempt)", string.Empty)) / TotalStudents);
            drAverage["TotalMarksObtained"] = Convert.ToInt32(Convert.ToInt32(dtTestResult.Compute("Sum(TotalMarksObtained)", string.Empty)) / TotalStudents);
            drAverage["Percentage"] = Convert.ToInt32(Convert.ToInt32(dtTestResult.Compute("Sum(Percentage)", string.Empty)) / TotalStudents);
            drAverage["Rank"] = 0;
            drAverage["TotalMarks"] = Convert.ToInt32(dtTestResult.Rows[0]["TotalMarks"]);
            drAverage["QualifyingMarks"] = Convert.ToString(dtTestResult.Rows[0]["QualifyingMarks"]);
            drAverage["CreatedOnDate"] = Convert.ToDateTime(dtTestResult.Rows[0]["CreatedOnDate"]);
            drAverage["IsActive"] = true;

            dtTA.Rows.Add(drAverage);

            dtTA.Columns.Remove("Physics_Total");
            dtTA.Columns.Remove("Chemistry_Total");
            dtTA.Columns.Remove("Biology_Total");
            dtTA.Columns.Remove("Rank");
            dtTA.Columns.Remove("TotalMarks");
            dtTA.Columns.Remove("QualifyingMarks");
            dtTA.Columns.Remove("CreatedOnDate");
            dtTA.Columns.Remove("IsActive");
            return dtTA;
        }

      
        //public DataTable TopTen(DataTable dt1)
        //{
        //    //DataTable dtTA = virtualTable();
        //    DataTable dt = dt1.Copy();
        //    dt.Rows.RemoveAt(0);
        //    //DataView view = dt.DefaultView;
        //    //view.Sort = "TotalMarks DESC";
        //    //dt = view.ToTable();

        //    DataTable dt2 = dt.Clone();
        //    //get only the rows you want
        //    DataRow[] results = dt.Select("", "TotalMarks DESC");

        //    //populate new destination table
        //    for (var i = 0; i < 10; i++)
        //        dt2.ImportRow(results[i]);
        //    return dt2;
        //}
        public DataTable virtualTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID", typeof(string));
            dt.Columns.Add("TestID", typeof(string));
            dt.Columns.Add("Physics_Total", typeof(int));
            dt.Columns.Add("Physics_Right", typeof(int));
            dt.Columns.Add("Physics_Wrong", typeof(int));
            dt.Columns.Add("Chemistry_Total", typeof(int));
            dt.Columns.Add("Chemistry_Right", typeof(int));
            dt.Columns.Add("Chemistry_Wrong", typeof(int));
            dt.Columns.Add("Biology_Total", typeof(int));
            dt.Columns.Add("Biology_Right", typeof(int));
            dt.Columns.Add("Biology_Wrong", typeof(int));
            dt.Columns.Add("TotalCorrect", typeof(int));
            dt.Columns.Add("TotalWrong", typeof(int));
            dt.Columns.Add("TotalAttempt", typeof(int));
            dt.Columns.Add("TotalMarksObtained", typeof(int));
            dt.Columns.Add("Percentage", typeof(double));
            dt.Columns.Add("Rank", typeof(int));
            dt.Columns.Add("TotalMarks", typeof(int));
            dt.Columns.Add("QualifyingMarks", typeof(string));
            dt.Columns.Add("CreatedOnDate", typeof(DateTime));
            dt.Columns.Add("IsActive", typeof(bool));
            return dt;
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        #endregion
        #region Old Analysis
        //public DataTable RankList(HttpPostedFileBase RankFile)
        //{
        //    var fileName = Path.GetFileName(RankFile.FileName);
        //    var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
        //    RankFile.SaveAs(path);
        //    //ExcelDataReader works on binary excel file
        //    Stream stream = RankFile.InputStream;
        //    //We need to written the Interface.
        //    IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //    reader.IsFirstRowAsColumnNames = true;
        //    DataTable dtRank = reader.AsDataSet().Tables[0];

        //    DataTable dtClone = dtRank.Clone();
        //    for (int i = 0; i < dtClone.Columns.Count; i++)
        //    {
        //        dtClone.Columns[i].DataType = typeof(int);
        //    }
        //    foreach (DataRow dr in dtRank.Rows)
        //    {
        //        dtClone.ImportRow(dr);
        //    }
        //    //  dtClone = reader.AsDataSet().Tables[0];
        //    return dtClone;
        //}
        //public DataTable StudentList(IExcelDataReader reader1)
        //{
        //    IExcelDataReader reader = reader1;
        //    reader.IsFirstRowAsColumnNames = true;
        //    DataTable dtExcel = reader.AsDataSet().Tables[0];
        //    dtExcel.Rows.RemoveAt(0);
        //    dtExcel.Columns["NAME"].ColumnName = "Name";
        //    dtExcel.Columns["Roll NO"].ColumnName = "RollNo";
        //    dtExcel.Columns["Cat"].ColumnName = "Category";
        //    for (int index = dtExcel.Columns.Count - 1; index >= 3; index--)
        //    {
        //        dtExcel.Columns.RemoveAt(index);
        //    }
        //    DataTable dtCloned = dtExcel.Clone();
        //    dtCloned.Columns["Name"].DataType = typeof(string);
        //    dtCloned.Columns["RollNo"].DataType = typeof(int);
        //    dtCloned.Columns["Category"].DataType = typeof(string);
        //    foreach (DataRow row in dtExcel.Rows)
        //    {
        //        dtCloned.ImportRow(row);
        //    }
        //    //DataView view = dtCloned.DefaultView;
        //    //view.Sort = "RollNo ASC";
        //    //dtCloned = view.ToTable();
        //    return dtCloned;
        //}
        //public DataTable PaperAnalysis(IExcelDataReader reader1, out int TotalEasy, out int TotalMedium, out int TotalDifficult, out string EasyQuestionList, out string MediumQuestionList, out string DifficultQuestionList)
        //{
        //    IExcelDataReader reader = reader1;
        //    reader.IsFirstRowAsColumnNames = true;
        //    DataTable dtExcel = reader.AsDataSet().Tables[0];
        //    // reader.Close();

        //    DataTable dtCorrectCount = new DataTable();
        //    DataRow drCorrectCount;
        //    dtCorrectCount.Columns.Add("Subject", typeof(string));
        //    dtCorrectCount.Columns.Add("QuestionNo", typeof(string));
        //    dtCorrectCount.Columns.Add("CorrectCount", typeof(string));
        //    dtCorrectCount.Columns.Add("CorrectAnswer", typeof(string));
        //    dtCorrectCount.Columns.Add("Level", typeof(string));



        //    int TotalQuestion = dtExcel.Columns.Count - 2;
        //    int TotalStudents = dtExcel.Rows.Count;
        //    int Easy = Convert.ToInt32(Math.Round((double)dtExcel.Rows.Count / 2));
        //    int Medium = Convert.ToInt32(Math.Round((double)dtExcel.Rows.Count / 3));
        //    int Difficult = Convert.ToInt32(Math.Round((double)dtExcel.Rows.Count / 5));
        //    int Count = 0;
        //    // dtExcel.Columns.RemoveAt(0);    //Removing Stundent Name
        //    dtExcel.Columns.Remove("NAME");    //Removing Stundent RollNo.
        //    dtExcel.Columns.Remove("Cat");    //Removing Stundent RollNo.
        //    DataRow drCorrect = dtExcel.Rows[0];    //First Row Will be correct Answer.
        //    // dtExcel.Rows.RemoveAt(0);
        //    for (int i = 1; i < dtExcel.Columns.Count; i++)
        //    {
        //        foreach (DataRow dr in dtExcel.Rows)
        //        {
        //            if (dr[i].ToString() == drCorrect[i].ToString())
        //            {
        //                Count++;    //To count the Correct Answer.
        //            }
        //        }
        //        if (i <= 45)    //Physics Question
        //        {
        //            drCorrectCount = dtCorrectCount.NewRow();
        //            drCorrectCount["Subject"] = "Physics";
        //            drCorrectCount["QuestionNo"] = i;
        //            drCorrectCount["CorrectCount"] = Count - 1; // (-1) is done to remove the result count.
        //            drCorrectCount["CorrectAnswer"] = drCorrect[i].ToString();
        //            drCorrectCount["Level"] = Count > Easy ? "Easy" : Count > Medium ? "Medium" : "Difficult";
        //            dtCorrectCount.Rows.Add(drCorrectCount);
        //            Count = 0;
        //        }
        //        if (i > 45 && i <= 90)  //Chemistry Question
        //        {
        //            drCorrectCount = dtCorrectCount.NewRow();
        //            drCorrectCount["Subject"] = "Chemistry";
        //            drCorrectCount["QuestionNo"] = i;
        //            drCorrectCount["CorrectCount"] = Count - 1;
        //            drCorrectCount["CorrectAnswer"] = drCorrect[i].ToString();
        //            drCorrectCount["Level"] = Count > Easy ? "Easy" : Count > Medium ? "Medium" : "Difficult";
        //            dtCorrectCount.Rows.Add(drCorrectCount);
        //            Count = 0;
        //        }

        //        else if (i > 90)    //Biology Question
        //        {
        //            drCorrectCount = dtCorrectCount.NewRow();
        //            drCorrectCount["Subject"] = "Biology";
        //            drCorrectCount["QuestionNo"] = i;
        //            drCorrectCount["CorrectCount"] = Count - 1;
        //            drCorrectCount["CorrectAnswer"] = drCorrect[i].ToString();
        //            drCorrectCount["Level"] = Count > Easy ? "Easy" : Count > Medium ? "Medium" : "Difficult";
        //            dtCorrectCount.Rows.Add(drCorrectCount);
        //            Count = 0;
        //        }
        //    }
        //    TotalEasy = 0; TotalMedium = 0; TotalDifficult = 0;
        //    // string EasyQuestionList, MediumQuestionList, DifficultQuestionList;
        //    StringBuilder builderEasy = new StringBuilder();
        //    StringBuilder builderMedium = new StringBuilder();
        //    StringBuilder builderDifficult = new StringBuilder();
        //    for (int i = 0; i < dtCorrectCount.Rows.Count; i++)
        //    {
        //        if (dtCorrectCount.Rows[i]["Level"].Equals("Easy"))
        //        {
        //            TotalEasy++;
        //            builderEasy.Append(dtCorrectCount.Rows[i]["QuestionNo"]).Append(",");

        //        }
        //        else if (dtCorrectCount.Rows[i]["Level"].Equals("Medium"))
        //        {
        //            TotalMedium++;
        //            builderMedium.Append(dtCorrectCount.Rows[i]["QuestionNo"]).Append(",");

        //        }
        //        else if (dtCorrectCount.Rows[i]["Level"].Equals("Difficult"))
        //        {
        //            TotalDifficult++;
        //            builderDifficult.Append(dtCorrectCount.Rows[i]["QuestionNo"]).Append(",");

        //        }
        //    }
        //    EasyQuestionList = builderEasy.Remove(builderEasy.Length - 1, 1).ToString();
        //    MediumQuestionList = builderMedium.Remove(builderMedium.Length - 1, 1).ToString();
        //    DifficultQuestionList = builderDifficult.Remove(builderDifficult.Length - 1, 1).ToString();
        //    string TotalList = EasyQuestionList + "," + MediumQuestionList + "," + DifficultQuestionList;
        //    int TotalQuestion1 = TotalEasy + TotalMedium + TotalDifficult;

        //    return dtCorrectCount;
        //}
        //public DataTable StudentAttempt(IExcelDataReader reader1, DataTable dtPaper)
        //{
        //    IExcelDataReader reader = reader1;
        //    reader.IsFirstRowAsColumnNames = true;
        //    DataTable dtStudent = reader1.AsDataSet().Tables[0];
        //    //reader.Close();
        //    //int j = 90;
        //    //for (int i = 1; i <= 90; i++)
        //    //{
        //    //    dtStudent.Columns[i + "_1"].ColumnName = Convert.ToString(j + i);   //CHange the Column Name
        //    //}
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("RollNo", typeof(string));
        //    dt.Columns.Add("EasyCorrect", typeof(string));
        //    dt.Columns.Add("EasyInCorrect", typeof(string));
        //    dt.Columns.Add("MediumCorrect", typeof(string));
        //    dt.Columns.Add("MediumInCorrect", typeof(string));
        //    dt.Columns.Add("DifficultCorrect", typeof(string));
        //    dt.Columns.Add("DifficultInCorrect", typeof(string));
        //    dt.Columns.Add("NotAttempt", typeof(string));
        //    DataRow dr;

        //    foreach (DataRow drStudent in dtStudent.Rows)
        //    {
        //        int EC = 0, EIC = 0, MC = 0, MIC = 0, DC = 0, DIC = 0;
        //        int NotAttempt = 0;
        //        string RollNo = drStudent["Roll No"].ToString();
        //        // dtStudent.Columns.Remove("NAME");
        //        // dtStudent.Columns.Remove("Roll No");
        //        foreach (DataRow drPaper in dtPaper.Rows)
        //        {
        //            for (int i = 0; i < 183; i++)
        //            {

        //                if (drPaper["QuestionNo"].ToString() == dtStudent.Columns[i].ColumnName.ToString())//Checking the QuestionNo
        //                {
        //                    if (drPaper["Level"].ToString() == "Easy")
        //                    {
        //                        if (drPaper["CorrectAnswer"] == drStudent[dtStudent.Columns[i].ColumnName])
        //                        {
        //                            EC++;
        //                        }
        //                        else if (drStudent[dtStudent.Columns[i].ColumnName].ToString() != "")
        //                        {
        //                            EIC++;
        //                        }
        //                        else
        //                        {
        //                            NotAttempt++;
        //                        }
        //                    }
        //                    else if (drPaper["Level"].ToString() == "Medium")
        //                    {
        //                        if (drPaper["CorrectAnswer"] == drStudent[dtStudent.Columns[i].ColumnName])
        //                        {
        //                            MC++;
        //                        }
        //                        else if (drStudent[dtStudent.Columns[i].ColumnName].ToString() != "")
        //                        {
        //                            MIC++;
        //                        }
        //                        else
        //                        {
        //                            NotAttempt++;
        //                        }
        //                    }
        //                    else if (drPaper["Level"].ToString() == "Difficult")
        //                    {
        //                        if (drPaper["CorrectAnswer"] == drStudent[dtStudent.Columns[i].ColumnName])
        //                        {
        //                            DC++;
        //                        }
        //                        else if (drStudent[dtStudent.Columns[i].ColumnName].ToString() != "")
        //                        {
        //                            DIC++;
        //                        }
        //                        else
        //                        {
        //                            NotAttempt++;
        //                        }
        //                    }
        //                    break;
        //                }
        //            }

        //        }
        //        dr = dt.NewRow();
        //        dr["RollNo"] = RollNo;
        //        dr["EasyCorrect"] = EC;
        //        dr["EasyInCorrect"] = EIC;
        //        dr["MediumCorrect"] = MC;
        //        dr["MediumInCorrect"] = MIC;
        //        dr["DifficultCorrect"] = DC;
        //        dr["DifficultInCorrect"] = DIC;
        //        dr["NotAttempt"] = NotAttempt;
        //        dt.Rows.Add(dr);
        //    }

        //    #region marks Review
        //    //DataTable dtFinal = virtualTable();
        //    //int Attempt = 0, NotAttempt = 0, CorrectCount = 0, IncorrectCount = 0;
        //    //foreach (DataRow drStudent in dtStudent.Rows)
        //    //{
        //    //    foreach (DataRow drPaper in dtPaper.Rows)
        //    //    {
        //    //        for (int i = 1; i <= 180; i++)
        //    //        {
        //    //            if (drStudent[i] != string.Empty)
        //    //            {
        //    //                Attempt++;
        //    //                if (drStudent[i] == drPaper[i])
        //    //                {
        //    //                    CorrectCount++;
        //    //                }
        //    //                else
        //    //                {
        //    //                    IncorrectCount++;
        //    //                }
        //    //            }
        //    //            else
        //    //            {
        //    //                NotAttempt++;
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    #endregion

        //    return dt;
        //}
        //public DataTable MarksReview(IExcelDataReader reader1, DataTable dtPaper)
        //{
        //    IExcelDataReader reader = reader1;
        //    reader.IsFirstRowAsColumnNames = true;
        //    DataTable dtStudent = reader1.AsDataSet().Tables[0];
        //    reader.Close();
        //    //int j = 90;
        //    //for (int i = 1; i <= 90; i++)
        //    //{
        //    //    dtStudent.Columns[i + "_1"].ColumnName = Convert.ToString(j + i);   //CHange the Column Name
        //    //}

        //    DataTable dt = virtualTable();
        //    DataRow dr;
        //    foreach (DataRow drStudent in dtStudent.Rows)
        //    {
        //        int PR = 0, PW = 0, CR = 0, CW = 0, BR = 0, BW = 0;
        //        int TotalRight = 0, TotalWrong = 0, Attempt = 0, NotAttempt = 0;
        //        string RollNo = drStudent["Roll No"].ToString();
        //        // dtStudent.Columns.Remove("NAME");
        //        // dtStudent.Columns.Remove("Roll No");
        //        foreach (DataRow drPaper in dtPaper.Rows)
        //        {
        //            for (int i = 0; i < 183; i++)
        //            {
        //                if (drPaper["QuestionNo"].ToString() == dtStudent.Columns[i].ColumnName.ToString())//Checking the QuestionNo
        //                {
        //                    if (drPaper["Subject"].ToString() == "Physics")
        //                    {
        //                        if (drPaper["CorrectAnswer"] == drStudent[dtStudent.Columns[i].ColumnName])
        //                        {
        //                            PR++;
        //                        }
        //                        else if (drStudent[dtStudent.Columns[i].ColumnName].ToString() != "")
        //                        {
        //                            PW++;
        //                        }
        //                        else
        //                        {
        //                            NotAttempt++;
        //                        }
        //                    }
        //                    else if (drPaper["Subject"].ToString() == "Chemistry")
        //                    {
        //                        if (drPaper["CorrectAnswer"] == drStudent[dtStudent.Columns[i].ColumnName])
        //                        {
        //                            CR++;
        //                        }
        //                        else if (drStudent[dtStudent.Columns[i].ColumnName].ToString() != "")
        //                        {
        //                            CW++;
        //                        }
        //                        else
        //                        {
        //                            NotAttempt++;
        //                        }
        //                    }
        //                    else if (drPaper["Subject"].ToString() == "Biology")
        //                    {
        //                        if (drPaper["CorrectAnswer"] == drStudent[dtStudent.Columns[i].ColumnName])
        //                        {
        //                            BR++;
        //                        }
        //                        else if (drStudent[dtStudent.Columns[i].ColumnName].ToString() != "")
        //                        {
        //                            BW++;
        //                        }
        //                        else
        //                        {
        //                            NotAttempt++;
        //                        }
        //                    }
        //                    break;
        //                }
        //            }
        //        }
        //        dr = dt.NewRow();
        //        dr["Name"] = "Student";
        //        dr["RollNo"] = !string.IsNullOrEmpty(RollNo) ? Convert.ToInt32(RollNo) : 0;
        //        dr["Physics_Right"] = PR;
        //        dr["Physics_Wrong"] = PW;
        //        dr["Chemistry_Right"] = CR;
        //        dr["Chemistry_Wrong"] = CW;
        //        dr["Biology_Right"] = BR;
        //        dr["Biology_Wrong"] = BW;
        //        dr["Total_Right"] = PR + CR + BR;
        //        dr["Total_Wrong"] = PW + CW + BW;
        //        dr["TotalAttempt"] = PR + CR + BR + PW + CW + BW;
        //        dr["TotalMarks"] = ((PR + CR + BR) * 4) - (PW + CW + BW);
        //        dr["Percentage"] = Math.Round((Convert.ToDouble(dr["TotalMarks"]) / 720) * 100, 4);
        //        //dr["Percentage"] = Convert.ToInt32(Math.Round((Convert.ToDouble(dr["TotalMarks"]) / 720) * 100, 2));
        //        dt.Rows.Add(dr);
        //    }

        //    return dt;

        //}
        //public DataTable Topper_Averge(DataTable dt1)
        //{
        //    DataTable dtTA = virtualTable();
        //    DataTable dt = dt1.Copy();
        //    dt.Rows.RemoveAt(0);
        //    DataView view = dt.DefaultView;
        //    view.Sort = "TotalMarks DESC";
        //    dt = view.ToTable();

        //    DataRow drTopper = dtTA.NewRow();
        //    int maxTotalMarks = Convert.ToInt32(dt.AsEnumerable().Max(x => x["TotalMarks"]));
        //    var SameTopperList = dt.AsEnumerable().Where(x => Convert.ToInt32(x["TotalMarks"]) == maxTotalMarks).ToList();
        //    if (SameTopperList.Count > 0)
        //    {
        //        int maxBiologyMarks = Convert.ToInt32(SameTopperList.AsEnumerable().Max(x => x["Biology_Right"]));
        //        var BiologyMarksList = SameTopperList.Where(x => Convert.ToInt32(x["Biology_Right"]) == maxBiologyMarks).ToList();
        //        if (BiologyMarksList.Count > 1)
        //        {
        //            int maxChemistryMarks = Convert.ToInt32(BiologyMarksList.AsEnumerable().Max(x => x["Chemistry_Right"]));
        //            var ChemistryMarksList = BiologyMarksList.Where(x => Convert.ToInt32(x["Chemistry_Right"]) == maxChemistryMarks).ToList();
        //            if (ChemistryMarksList.Count > 1)
        //            {
        //                int maxPhysicsMarks = Convert.ToInt32(ChemistryMarksList.Max(x => x["Physics_Right"]));
        //                var PhysicsMarksList = ChemistryMarksList.Where(x => Convert.ToInt32(x["Physics_Right"]) == maxPhysicsMarks).ToList();
        //                if (PhysicsMarksList.Count >= 1)
        //                {
        //                    drTopper = PhysicsMarksList.FirstOrDefault();
        //                }
        //            }
        //            else
        //            {
        //                drTopper = ChemistryMarksList.FirstOrDefault();
        //            }
        //        }
        //        else
        //        {
        //            drTopper = BiologyMarksList.FirstOrDefault();
        //        }
        //    }
        //    dtTA.Rows.Add(drTopper.ItemArray);
        //    dtTA.Rows[0]["Name"] = "Topper";
        //    int TotalStudents = dt.Rows.Count;
        //    DataRow drAverage = dtTA.NewRow();
        //    drAverage["Name"] = "Average";
        //    drAverage["Physics_Right"] = Convert.ToInt32(Convert.ToInt32(dt.Compute("Sum(Physics_Right)", string.Empty)) / TotalStudents);
        //    drAverage["Physics_Wrong"] = Convert.ToInt32(Convert.ToInt32(dt.Compute("Sum(Physics_Wrong)", string.Empty)) / TotalStudents);
        //    drAverage["Chemistry_Right"] = Convert.ToInt32(Convert.ToInt32(dt.Compute("Sum(Chemistry_Right)", string.Empty)) / TotalStudents);
        //    drAverage["Chemistry_Wrong"] = Convert.ToInt32(Convert.ToInt32(dt.Compute("Sum(Chemistry_Wrong)", string.Empty)) / TotalStudents);
        //    drAverage["Biology_Right"] = Convert.ToInt32(Convert.ToInt32(dt.Compute("Sum(Biology_Right)", string.Empty)) / TotalStudents);
        //    drAverage["Biology_Wrong"] = Convert.ToInt32(Convert.ToInt32(dt.Compute("Sum(Biology_Wrong)", string.Empty)) / TotalStudents);
        //    drAverage["Total_Right"] = Convert.ToInt32(Convert.ToInt32(dt.Compute("Sum(Total_Right)", string.Empty)) / TotalStudents);
        //    drAverage["Total_Wrong"] = Convert.ToInt32(Convert.ToInt32(dt.Compute("Sum(Total_Wrong)", string.Empty)) / TotalStudents);
        //    drAverage["TotalAttempt"] = Convert.ToInt32(Convert.ToInt32(dt.Compute("Sum(TotalAttempt)", string.Empty)) / TotalStudents);
        //    drAverage["TotalMarks"] = Convert.ToInt32(Convert.ToInt32(dt.Compute("Sum(TotalMarks)", string.Empty)) / TotalStudents);
        //    drAverage["Percentage"] = Convert.ToInt32(Convert.ToInt32(dt.Compute("Sum(Percentage)", string.Empty)) / TotalStudents);
        //    dtTA.Rows.Add(drAverage);
        //    dtTA.Rows[1]["RollNo"] = 0;
        //    //string searchExpression = string.Format("RollNo = {0}", Convert.ToString(246));
        //    //dtTA.Rows.Add(dt.Select(searchExpression)[0].ItemArray);

        //    return dtTA;
        //}
        //public DataTable TopTen(DataTable dt1)
        //{
        //    //DataTable dtTA = virtualTable();
        //    DataTable dt = dt1.Copy();
        //    dt.Rows.RemoveAt(0);
        //    //DataView view = dt.DefaultView;
        //    //view.Sort = "TotalMarks DESC";
        //    //dt = view.ToTable();

        //    DataTable dt2 = dt.Clone();
        //    //get only the rows you want
        //    DataRow[] results = dt.Select("", "TotalMarks DESC");

        //    //populate new destination table
        //    for (var i = 0; i < 10; i++)
        //        dt2.ImportRow(results[i]);
        //    return dt2;
        //}
        //public DataTable virtualTable()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("Name", typeof(string));
        //    dt.Columns.Add("RollNo", typeof(int));
        //    dt.Columns.Add("Physics_Right", typeof(int));
        //    dt.Columns.Add("Physics_Wrong", typeof(int));
        //    dt.Columns.Add("Chemistry_Right", typeof(int));
        //    dt.Columns.Add("Chemistry_Wrong", typeof(int));
        //    dt.Columns.Add("Biology_Right", typeof(int));
        //    dt.Columns.Add("Biology_Wrong", typeof(int));
        //    dt.Columns.Add("Total_Right", typeof(int));
        //    dt.Columns.Add("Total_Wrong", typeof(int));
        //    dt.Columns.Add("TotalAttempt", typeof(int));
        //    dt.Columns.Add("TotalMarks", typeof(int));
        //    dt.Columns.Add("Percentage", typeof(double));
        //    return dt;
        //}
        //public DataTable AppendData(DataTable dtSource, DataTable dtDestination)
        //{
        //    foreach (DataRow dr in dtDestination.Rows)
        //    {
        //        dr["Name"] = dtSource.AsEnumerable().Select(row => row["RollNo"] = Convert.ToInt32(dr["RollNo"]));
        //        DataRow[] r1 = dtSource.Select("RollNo =" + Convert.ToString(dr["RollNo"]));
        //        dr["Name"] = r1[0]["Name"];
        //        var dValue = from row in dtSource.AsEnumerable() where row.Field<int>("Roll No") == Convert.ToInt32(dr["RollNo"]) select row.Field<string>("Name");
        //        //  dr["Name"] = dValue;

        //    }
        //    return dtDestination;
        //}
        //private static List<T> ConvertDataTable<T>(DataTable dt)
        //{
        //    List<T> data = new List<T>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        T item = GetItem<T>(row);
        //        data.Add(item);
        //    }
        //    return data;
        //}
        //private static T GetItem<T>(DataRow dr)
        //{
        //    Type temp = typeof(T);
        //    T obj = Activator.CreateInstance<T>();

        //    foreach (DataColumn column in dr.Table.Columns)
        //    {
        //        foreach (PropertyInfo pro in temp.GetProperties())
        //        {
        //            if (pro.Name == column.ColumnName)
        //                pro.SetValue(obj, dr[column.ColumnName], null);
        //            else
        //                continue;
        //        }
        //    }
        //    return obj;
        //}
        //private List<T> ConvertToList<T>(DataTable dt)
        //{
        //    var columnNames = dt.Columns.Cast<DataColumn>()
        //        .Select(c => c.ColumnName)
        //        .ToList();

        //    var properties = typeof(T).GetProperties();

        //    return dt.AsEnumerable().Select(row =>
        //    {
        //        var objT = Activator.CreateInstance<T>();

        //        foreach (var pro in properties)
        //        {
        //            if (columnNames.Contains(pro.Name))
        //                pro.SetValue(objT, row[pro.Name]);
        //        }

        //        return objT;
        //    }).ToList();

        //}
        #endregion
    }
}
