using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Question;
using ViewModels.Test;

namespace DataAccessLayer
{
    public class DQuiz : IDQuiz
    {
        public QuizViewModel GetQuiz(int OnlineTestID)
        {
            try
            {
                //List<string> optionList = new List<string>();
                Dictionary<int, string> optionList = new Dictionary<int, string>();
                //optionList.Add("A");
                //optionList.Add("B");
                //optionList.Add("C");
                //optionList.Add("D");
                optionList.Add(1, "A");
                optionList.Add(2, "B");
                optionList.Add(3, "C");
                optionList.Add(4, "D");
                List<SqlParameter> paramerterList = new List<SqlParameter>();
                paramerterList.Add(new SqlParameter("@OnlineTestID", OnlineTestID));
                DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetQuiz", paramerterList, null).Tables[0];
                QuizViewModel quizViewModel = new QuizViewModel();
                quizViewModel.Question = new List<QuestionViewModel>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr["OnlineTestID"]) != quizViewModel.OnlineTestID)
                        {
                            quizViewModel.OnlineTestID = Convert.ToInt32(dr["OnlineTestID"]);
                            quizViewModel.TestName = dr["TestName"].ToString();
                            quizViewModel.TestDuration = dr["TestDuration"].ToString();
                            quizViewModel.Instructions = dr["Instructions"].ToString();
                            QuestionViewModel questionViewModel = new QuestionViewModel();
                            questionViewModel.QuestionId = Convert.ToInt32(dr["QuestionId"]);
                            questionViewModel.Image_English = ConfigurationManager.AppSettings["BaseURL"].ToString() + "/" + dr["Image_English"];
                            questionViewModel.Image_Hindi = ConfigurationManager.AppSettings["BaseURL"].ToString() + "/" + dr["Image_Hindi"];
                            questionViewModel.QuestionTypeId = Convert.ToInt32(dr["QuestionTypeId"]);
                            questionViewModel.Options = new List<OptionViewModel>();
                            foreach (var optionItem in optionList)
                            {
                                questionViewModel.Options.Add(new OptionViewModel()
                                {
                                    QuestionId = Convert.ToInt32(dr["QuestionId"]),
                                    OptionId = optionItem.Key,
                                    Option = optionItem.Value
                                });
                            }
                            questionViewModel.QuestionType = new QuestionTypeViewModel()
                            {
                                QuestionTypeID = Convert.ToInt32(dr["QuestionTypeId"]),
                                QuestionType = dr["QuestionType"].ToString()
                            };
                            quizViewModel.Question.Add(questionViewModel);
                        }
                        else
                        {
                            QuestionViewModel questionViewModel = new QuestionViewModel();
                            questionViewModel.QuestionId = Convert.ToInt32(dr["QuestionId"]);
                            questionViewModel.Image_English = ConfigurationManager.AppSettings["BaseURL"].ToString() + "/" + dr["Image_English"];
                            questionViewModel.Image_Hindi = ConfigurationManager.AppSettings["BaseURL"].ToString() + "/" + dr["Image_Hindi"];
                            questionViewModel.QuestionTypeId = Convert.ToInt32(dr["QuestionTypeId"]);
                            questionViewModel.Options = new List<OptionViewModel>();
                            foreach (var optionItem in optionList)
                            {
                                questionViewModel.Options.Add(new OptionViewModel()
                                {
                                    QuestionId = Convert.ToInt32(dr["QuestionId"]),
                                    OptionId = optionItem.Key,
                                    Option = optionItem.Value
                                });
                            }
                            questionViewModel.QuestionType = new QuestionTypeViewModel()
                            {
                                QuestionTypeID = Convert.ToInt32(dr["QuestionTypeId"]),
                                QuestionType = dr["QuestionType"].ToString()
                            };
                            quizViewModel.Question.Add(questionViewModel);
                        }
                    }
                }
                return quizViewModel;
            }
            catch (Exception ex)
            {
                dynamic obj = new QuizViewModel();
                return obj.ErrorMessage = ex.Message;
                //return new QuizViewModel() { ErrorMessage = ex.Message };
            }
        }
    }
}
