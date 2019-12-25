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
        public Response<TopicMasterViewModel> GetTopicByID(int TopicID)
        {
            var topicMasterData = new TopicMasterViewModel();
            topicMasterData.TopicData = _iDMaster.GetTopicByID(TopicID);
            topicMasterData.MasterData = _iDMaster.GetMasterData();
            if (topicMasterData != null)
            {
                return new Response<TopicMasterViewModel>
                {
                    IsSuccessful = true,
                    Object = topicMasterData,
                    Message = CommonEnum.Status.Success.ToString()
                };
            }
            else
            {
                return new Response<TopicMasterViewModel>
                {
                    IsSuccessful = false,
                    Message = CommonEnum.Status.Failed.ToString(),
                    Object = null
                };
            }
            //var topicData = _iDMaster.GetTopicByID(TopicID);
            //if (topicData != null)
            //{
            //    return new Response<TopicViewModel>
            //    {
            //        IsSuccessful = true,
            //        Object = topicData,
            //        Message = "Success"
            //    };
            //}
            //else
            //{
            //    return new Response<TopicViewModel>
            //    {
            //        IsSuccessful = false,
            //        Message = "error",
            //        Object = null
            //    };
            //}
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

       #region Slider
       public Response<List<SliderViewModel>> GetSlider(int SliderID)
       {
           var SliderData = _iDMaster.GetSlider(SliderID);
           if (SliderData != null)
           {
               return new Response<List<SliderViewModel>>
               {
                   IsSuccessful = true,
                   Object = SliderData,
                   Message = "Success"
               };
           }
           else
           {
               return new Response<List<SliderViewModel>>
               {
                   IsSuccessful = false,
                   Message = "error",
                   Object = null
               };
           }
       }
       public string AddUpdateSlider(SliderViewModel objSlider)
       {
           var SliderData = _iDMaster.AddUpdateSlider(objSlider);
           if (!string.IsNullOrEmpty(SliderData))
           {
               return SliderData;
           }
           else
           {
               return SliderData;
           }
       }

       public string DeleteSlider(SliderViewModel objSlider)
       {
           return _iDMaster.DeleteSlider(objSlider);
       }
        #endregion

        #region Notification
        public Response<List<NotificationViewModel>> GetNotification(int NotificationID,int ReciverID)
        {
            var NotificationData = _iDMaster.GetNotification(NotificationID, ReciverID);
            if (NotificationData != null)
            {
                return new Response<List<NotificationViewModel>>
                {
                    IsSuccessful = true,
                    Object = NotificationData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<NotificationViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }
        public string AddUpdateNotification(List<NotificationViewModel> objNotification)
        {
            var NotificationData = _iDMaster.AddUpdateNotification(objNotification);
            if (!string.IsNullOrEmpty(NotificationData))
            {
                return NotificationData;
            }
            else
            {
                return NotificationData;
            }
        }
        #endregion

        public Response<List<TopicViewModel>> GetTopicBySubject(string SubjectID)
       {
           var courseData = _iDMaster.GetTopicBySubject(SubjectID);
           if (courseData != null)
           {
               return new Response<List<TopicViewModel>>
               {
                   IsSuccessful = true,
                   Object = courseData,
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

       public Response<List<SubTopicViewModel>> GetSubTopicByTopic(string TopicID)
       {
           var batchData = _iDMaster.GetSubTopicByTopic(TopicID);
           if (batchData != null)
           {
               return new Response<List<SubTopicViewModel>>
               {
                   IsSuccessful = true,
                   Object = batchData,
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

    }
}
