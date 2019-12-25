using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ViewModels;
using ViewModels.Question;
using ViewModels.Test;

namespace DataAccessLayer
{
    public class DQuiz : IDQuiz
    {
        public QuizViewModel GetQuiz(int OnlineTestID, int StudentID)
        {
            string strQuery = string.Format("select TestStatusID from EligibleStudent where OnlineTestID={0} and StudentID={1}", OnlineTestID, StudentID);
            int TestStatusID = Convert.ToInt32(DGeneric.GetValue(strQuery));
            if (TestStatusID == 1)
            {
                string strQry = string.Format("update EligibleStudent set TestStatusID=2 where OnlineTestID={0} and StudentID={1};", OnlineTestID, StudentID);
                DGeneric.ExecQuery(strQry);

                Dictionary<int, string> optionList = new Dictionary<int, string>();
                optionList.Add(1, "A");
                optionList.Add(2, "B");
                optionList.Add(3, "C");
                optionList.Add(4, "D");
                List<SqlParameter> paramerterList = new List<SqlParameter>();
                paramerterList.Add(new SqlParameter("@OnlineTestID", OnlineTestID));
                DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetQuiz", paramerterList, null).Tables[0];
                QuizViewModel quizViewModel = new QuizViewModel();
                quizViewModel.Questions = new List<QuestionViewModel>();
                List<string> answerList = new List<string>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr["OnlineTestID"]) != quizViewModel.OnlineTestID)
                        {
                            quizViewModel.OnlineTestID = Convert.ToInt32(dr["OnlineTestID"]);
                            quizViewModel.TestName = dr["TestName"].ToString();
                            quizViewModel.TestSeriesID = Convert.ToInt32(dr["TestSeriesID"]);
                            quizViewModel.TestSeries = dr["TestSeries"].ToString();
                            quizViewModel.TestTypeID = Convert.ToInt32(dr["TestTypeID"]);
                            quizViewModel.TestType = dr["TestType"].ToString();
                            quizViewModel.TestDuration = dr["TestDuration"].ToString();
                            quizViewModel.Instructions = dr["Instructions"].ToString();
                            quizViewModel.TotalMarks = dr["TotalMarks"].ToString();
                            quizViewModel.PassingPercentage = dr["PassingPercentage"].ToString();
                            QuestionViewModel questionViewModel = new QuestionViewModel();
                            questionViewModel.QuestionID = Convert.ToInt32(dr["QuestionId"]);
                            questionViewModel.TestQuestionNo = dr["TestQuestionNo"].ToString();
                            switch (dr["Subject"].ToString())
                            {
                                case "Physics":
                                    quizViewModel.PhysicsQuestionCount += 1;
                                    questionViewModel.IsDefaultQuestion = true;
                                    break;
                                case "Chemistry":
                                    quizViewModel.ChemistryQuestionCount += 1;
                                    break;
                                case "Biology":
                                    quizViewModel.BiologyQuestionCount += 1;
                                    break;
                                case "Aptitude":
                                    quizViewModel.AptitudeQuestionCount += 1;
                                    break;
                                default:
                                    break;
                            }
                            questionViewModel.Subject = dr["Subject"].ToString();
                            questionViewModel.SubjectID = Convert.ToInt32(dr["SubjectID"]);
                            questionViewModel.Image_English = ConfigurationManager.AppSettings["BaseURL"].ToString() + "/" + dr["Image_English"];
                            questionViewModel.Image_Hindi = ConfigurationManager.AppSettings["BaseURL"].ToString() + "/" + dr["Image_Hindi"];
                            questionViewModel.QuestionTypeID = Convert.ToInt32(dr["QuestionTypeId"]);
                            answerList = dr["Answer"].ToString().Split(',').Select(s => s.ToString()).ToList();
                            questionViewModel.Options = new List<OptionViewModel>();
                            foreach (var optionItem in optionList)
                            {
                                OptionViewModel optionViewModel = new OptionViewModel();
                                optionViewModel.QuestionID = Convert.ToInt32(dr["QuestionId"]);
                                optionViewModel.OptionID = optionItem.Key;
                                optionViewModel.Option = optionItem.Value;
                                if (answerList.Any(a => a == optionItem.Value))
                                    optionViewModel.IsAnswer = true;
                                else
                                    optionViewModel.IsAnswer = false;
                                questionViewModel.Options.Add(optionViewModel);
                            }
                            questionViewModel.QuestionType = new QuestionTypeViewModel()
                            {
                                QuestionTypeID = Convert.ToInt32(dr["QuestionTypeId"]),
                                QuestionType = dr["QuestionType"].ToString()
                            };
                            quizViewModel.Questions.Add(questionViewModel);
                        }
                        else
                        {
                            QuestionViewModel questionViewModel = new QuestionViewModel();
                            questionViewModel.QuestionID = Convert.ToInt32(dr["QuestionId"]);
                            switch (dr["Subject"].ToString())
                            {
                                case "Physics":
                                    quizViewModel.PhysicsQuestionCount += 1;
                                    break;
                                case "Chemistry":
                                    quizViewModel.ChemistryQuestionCount += 1;
                                    break;
                                case "Biology":
                                    quizViewModel.BiologyQuestionCount += 1;
                                    break;
                                case "Aptitude":
                                    quizViewModel.AptitudeQuestionCount += 1;
                                    break;
                                default:
                                    break;
                            }
                            questionViewModel.SubjectID = Convert.ToInt32(dr["SubjectID"]);
                            questionViewModel.IsDefaultQuestion = false;
                            questionViewModel.TestQuestionNo = dr["TestQuestionNo"].ToString();
                            questionViewModel.Subject = dr["Subject"].ToString();
                            questionViewModel.Image_English = ConfigurationManager.AppSettings["BaseURL"].ToString() + "/" + dr["Image_English"];
                            questionViewModel.Image_Hindi = ConfigurationManager.AppSettings["BaseURL"].ToString() + "/" + dr["Image_Hindi"];
                            questionViewModel.QuestionTypeID = Convert.ToInt32(dr["QuestionTypeId"]);
                            answerList = dr["Answer"].ToString().Split(',').Select(s => s.ToString()).ToList();
                            questionViewModel.Options = new List<OptionViewModel>();
                            foreach (var optionItem in optionList)
                            {
                                OptionViewModel optionViewModel = new OptionViewModel();
                                optionViewModel.QuestionID = Convert.ToInt32(dr["QuestionId"]);
                                optionViewModel.OptionID = optionItem.Key;
                                optionViewModel.Option = optionItem.Value;
                                if (answerList.Any(a => a == optionItem.Value))
                                    optionViewModel.IsAnswer = true;
                                else
                                    optionViewModel.IsAnswer = false;
                                questionViewModel.Options.Add(optionViewModel);
                            }
                            questionViewModel.QuestionType = new QuestionTypeViewModel()
                            {
                                QuestionTypeID = Convert.ToInt32(dr["QuestionTypeId"]),
                                QuestionType = dr["QuestionType"].ToString()
                            };
                            quizViewModel.Questions.Add(questionViewModel);
                        }
                    }
                }
                return quizViewModel;
            }
            else
            {
                return null;
            }
        }

        public string SubmitQuiz(QuizViewModel QuizViewModel)
        {
            // int StudentID = 5123;
            // Convert.ToInt32(HttpContext.Current.Session["StudentID"]);
            int OptionID = 0;
            int AnswerID = 0;
            bool IsCorrect = false;
            DataTable dtStudentResponse = new DataTable();
            dtStudentResponse.Columns.Add("StudentID", typeof(int));
            dtStudentResponse.Columns.Add("TestID", typeof(int));
            dtStudentResponse.Columns.Add("QuestionID", typeof(int));
            dtStudentResponse.Columns.Add("SubjectID", typeof(int));
            dtStudentResponse.Columns.Add("OptionID", typeof(int));
            dtStudentResponse.Columns.Add("AnswerID", typeof(int));
            dtStudentResponse.Columns.Add("IsCorrect", typeof(bool));

            foreach (var Questions in QuizViewModel.Questions)
            {
                //What will happen when 2 answers are correct
                // Questions.SubjectID = 1;//For Testing Only
                if (Questions.SubjectID == 1)//Physics
                {
                    foreach (var Options in Questions.Options)
                    {
                        if (Options.IsAnswer && Options.Selected)   //Correct Answer
                        {
                            OptionID = Options.OptionID;
                            AnswerID = Options.OptionID;
                            IsCorrect = true;
                        }
                        else if (Options.IsAnswer && !Options.Selected) //Correct Not Attempt
                        {
                            OptionID = Options.OptionID;
                            IsCorrect = false;
                        }
                        else if (!Options.IsAnswer && Options.Selected) //Incorrect Answer
                        {
                            AnswerID = Options.OptionID;
                            IsCorrect = false;
                        }
                    }
                }
                if (Questions.SubjectID == 2)//Chemistry
                {
                    foreach (var Options in Questions.Options)
                    {
                        if (Options.IsAnswer && Options.Selected)
                        {
                            OptionID = Options.OptionID;
                            AnswerID = Options.OptionID;
                            IsCorrect = true;
                        }
                        else if (Options.IsAnswer && !Options.Selected)
                        {
                            OptionID = Options.OptionID;
                            IsCorrect = false;
                        }
                        else if (!Options.IsAnswer && Options.Selected)
                        {
                            AnswerID = Options.OptionID;
                            IsCorrect = false;
                        }
                    }
                }
                if (Questions.SubjectID == 3)//Biology
                {
                    foreach (var Options in Questions.Options)
                    {
                        if (Options.IsAnswer && Options.Selected)
                        {
                            OptionID = Options.OptionID;
                            AnswerID = Options.OptionID;
                            IsCorrect = true;
                        }
                        else if (Options.IsAnswer && !Options.Selected)
                        {
                            OptionID = Options.OptionID;
                            IsCorrect = false;
                        }
                        else if (!Options.IsAnswer && Options.Selected)
                        {
                            AnswerID = Options.OptionID;
                            IsCorrect = false;
                        }
                    }
                }
                if (Questions.SubjectID == 4)//Aptitude
                {
                    foreach (var Options in Questions.Options)
                    {
                        if (Options.IsAnswer && Options.Selected)
                        {
                            OptionID = Options.OptionID;
                            AnswerID = Options.OptionID;
                            IsCorrect = true;
                        }
                        else if (Options.IsAnswer && !Options.Selected)
                        {
                            OptionID = Options.OptionID;
                            IsCorrect = false;
                        }
                        else if (!Options.IsAnswer && Options.Selected)
                        {
                            AnswerID = Options.OptionID;
                            IsCorrect = false;
                        }
                    }
                }
                dtStudentResponse.Rows.Add(QuizViewModel.StudentID, QuizViewModel.OnlineTestID, Questions.QuestionID, Questions.SubjectID, OptionID, AnswerID, IsCorrect);
                OptionID = 0; AnswerID = 0; IsCorrect = false;
            }

            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@StudentResponseDetails", dtStudentResponse));
           string response = DGeneric.RunSP_ExecuteNonQuery("sp_AddStudentResponse", sqlParameterList);

            string strquery = string.Format(@"UPDATE EligibleStudent SET TestStatusID = {2} WHERE OnlineTestID={0} and StudentID in ({1});", QuizViewModel.OnlineTestID, QuizViewModel.StudentID, '3');
            DGeneric.ExecQuery(strquery);
            return response;
        }
    }
}
