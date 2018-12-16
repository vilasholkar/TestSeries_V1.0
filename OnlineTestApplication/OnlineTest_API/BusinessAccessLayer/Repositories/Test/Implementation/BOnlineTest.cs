using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Test;

namespace BusinessAccessLayer
{
    public class BOnlineTest : IBOnlineTest
    {
        private readonly IDOnlineTest _iDOnlineTest;

        private readonly IDMaster _iDMaster;
        public BOnlineTest(IDOnlineTest iDOnlineTest, IDMaster iDMaster)
        {
            _iDOnlineTest = iDOnlineTest;
            _iDMaster = iDMaster;
        }
        public Response<List<OnlineTestViewModel>> GetOnlineTest()
        {
            try
            {
                var onlineTestData = _iDOnlineTest.GetOnlineTest();
                if (onlineTestData != null)
                {
                    return new Response<List<OnlineTestViewModel>>
                    {
                        IsSuccessful = true,
                        Object = onlineTestData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<List<OnlineTestViewModel>>
                    {
                        IsSuccessful = false,
                        Message = "error",
                        Object = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<OnlineTestViewModel>>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Object = null
                };
            }
        }
        public string AddUpdateOnlineTest(OnlineTestViewModel objOnlineTest)
        {
            try
            {
                var onlineTestData = _iDOnlineTest.AddUpdateOnlineTest(objOnlineTest);
                if (!string.IsNullOrEmpty(onlineTestData))
                {
                    return onlineTestData;
                }
                else
                {
                    return onlineTestData;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeleteOnlineTest(int OnlineTestId)
        {
            return _iDOnlineTest.DeleteOnlineTest(OnlineTestId);
        }
        public Response<OnlineTestViewModel> GetOnlineTestById(int OnlineTestId)
        {
            try
            {
                var onlineTestData = _iDOnlineTest.GetOnlineTestById(OnlineTestId);
                if (onlineTestData != null)
                {
                    return new Response<OnlineTestViewModel>
                    {
                        IsSuccessful = true,
                        Object = onlineTestData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<OnlineTestViewModel>
                    {
                        IsSuccessful = false,
                        Message = "error",
                        Object = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<OnlineTestViewModel>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Object = null
                };
            }
        }
        public Response<QuizViewModel> GetQuestionsByTestId(int OnlineTestID)
        {
            try
            {
                var questionData = _iDOnlineTest.GetQuestionsByTestId(OnlineTestID);
                if (questionData != null)
                {
                    return new Response<QuizViewModel>
                    {
                        IsSuccessful = true,
                        Object = questionData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<QuizViewModel>
                    {
                        IsSuccessful = false,
                        Message = "error",
                        Object = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<QuizViewModel>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Object = null
                };
            }
        }
        public Response<List<StudentOnlineTestViewModel>> GetOnlineTestByStudentID(int StudentID)
        {
            try
            {
                var onlineTestData = _iDOnlineTest.GetOnlineTestByStudentID(StudentID);
                if (onlineTestData != null)
                {
                    return new Response<List<StudentOnlineTestViewModel>>
                    {
                        IsSuccessful = true,
                        Object = onlineTestData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<List<StudentOnlineTestViewModel>>
                    {
                        IsSuccessful = false,
                        Message = "error",
                        Object = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<StudentOnlineTestViewModel>>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Object = null
                };
            }
        }
        public Response<OnlineTestMasterViewModel> GetOnlineTestMasterDataByTestID(int OnlineTestID)
        {
            try
            {
                var onlineTestMasterData = new OnlineTestMasterViewModel();
                onlineTestMasterData.OnlineTestData = _iDOnlineTest.GetOnlineTestById(OnlineTestID);
                onlineTestMasterData.MasterData = _iDMaster.GetMasterData();
                if (onlineTestMasterData != null)
                {
                    return new Response<OnlineTestMasterViewModel>
                    {
                        IsSuccessful = true,
                        Object = onlineTestMasterData,
                        Message = CommonEnum.Status.Success.ToString()
                    };
                }
                else
                {
                    return new Response<OnlineTestMasterViewModel>
                    {
                        IsSuccessful = false,
                        Message = CommonEnum.Status.Failed.ToString(),
                        Object = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<OnlineTestMasterViewModel>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Object = null
                };
            }
        }
    }
}
