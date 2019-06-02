using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Master;

namespace BusinessAccessLayer
{
    public class BMaster : IBMaster
    {
        private readonly IDMaster _iDMaster;
        public BMaster(IDMaster iDMaster)
        {
            _iDMaster = iDMaster;
        }
        public Response<List<StreamViewModel>> GetStream()
        {
            var masterData = _iDMaster.GetStream();
            if (masterData != null)
            {
                return new Response<List<StreamViewModel>>
                {
                    IsSuccessful = true,
                    Object = masterData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<StreamViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }


        public Response<List<CourseViewModel>> GetCourseByStream(string StreamId)
        {
            var courseData = _iDMaster.GetCourseByStream(StreamId);
            if (courseData != null)
            {
                return new Response<List<CourseViewModel>>
                {
                    IsSuccessful = true,
                    Object = courseData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<CourseViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }


        public Response<List<BatchViewModel>> GetBatchByCourse(string CourseId)
        {
            var batchData = _iDMaster.GetBatchByCourse(CourseId);
            if (batchData != null)
            {
                return new Response<List<BatchViewModel>>
                {
                    IsSuccessful = true,
                    Object = batchData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<BatchViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }


        public Response<List<SessionViewModel>> GetSession()
        {
            var sessionData = _iDMaster.GetSession();
            if (sessionData != null)
            {
                return new Response<List<SessionViewModel>>
                {
                    IsSuccessful = true,
                    Object = sessionData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<SessionViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }


        public Response<MasterViewModel> GetMasterData()
        {
            var masterData = _iDMaster.GetMasterData();
            if (masterData != null)
            {
                return new Response<MasterViewModel>
                {
                    IsSuccessful = true,
                    Object = masterData,
                    Message = CommonEnum.Status.Success.ToString()
                };
            }
            else
            {
                return new Response<MasterViewModel>
                {
                    IsSuccessful = false,
                    Message = CommonEnum.Status.Failed.ToString(),
                    Object = null
                };
            }
        }

        public Response<List<SubjectViewModel>> GetSubject()
        {
            var topicData = _iDMaster.GetSubject();
            if (topicData != null)
            {
                return new Response<List<SubjectViewModel>>
                {
                    IsSuccessful = true,
                    Object = topicData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<SubjectViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }

        #region Topic
        public Response<List<TopicViewModel>> GetTopic()
        {
            var topicData = _iDMaster.GetTopic();
            if (topicData != null)
            {
                return new Response<List<TopicViewModel>>
                {
                    IsSuccessful = true,
                    Object = topicData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<TopicViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }
        public string AddUpdateTopic(TopicViewModel objTopic)
        {
            var topicData = _iDMaster.AddUpdateTopic(objTopic);
            if (!string.IsNullOrEmpty(topicData))
            {
                return topicData;
            }
            else
            {
                return topicData;
            }
        }

        public string DeleteTopic(TopicViewModel objTopic)
        {
            return _iDMaster.DeleteTopic(objTopic);
        }
        #endregion

        #region SubTopic
        public Response<List<SubTopicViewModel>> GetSubTopic()
        {
            var subTopicData = _iDMaster.GetSubTopic();
            if (subTopicData != null)
            {
                return new Response<List<SubTopicViewModel>>
                {
                    IsSuccessful = true,
                    Object = subTopicData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<SubTopicViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }
       public string AddUpdateSubTopic(SubTopicViewModel objSubTopic)
        {
            var subTopicData = _iDMaster.AddUpdateSubTopic(objSubTopic);
            if (!string.IsNullOrEmpty(subTopicData))
            {
                return subTopicData;
            }
            else
            {
                return subTopicData;
            }
        }

       public string DeleteSubTopic(SubTopicViewModel objSubTopic)
       {
           return _iDMaster.DeleteSubTopic(objSubTopic);
       }
        #endregion
    }
}
