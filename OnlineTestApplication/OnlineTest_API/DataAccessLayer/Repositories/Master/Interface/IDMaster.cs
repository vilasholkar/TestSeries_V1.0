using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Master;
namespace DataAccessLayer
{
   public interface IDMaster
    {
       List<StreamViewModel> GetStream();
       List<CourseViewModel> GetCourseByStream(string StreamId);
       List<BatchViewModel> GetBatchByCourse(string CourseId);
       List<SessionViewModel> GetSession();
       MasterViewModel GetMasterData();
       List<SubjectViewModel> GetSubject();

       #region Topic
       List<TopicViewModel> GetTopic();
       TopicViewModel GetTopicByID(int TopicID);
       string AddUpdateTopic(TopicViewModel objTopic);
       string DeleteTopic(TopicViewModel objTopic);
       #endregion

       #region SubTopic
       List<SubTopicViewModel> GetSubTopic();
       string AddUpdateSubTopic(SubTopicViewModel objSubTopic);
       string DeleteSubTopic(SubTopicViewModel objSubTopic);
       #endregion

       #region Slider
       List<SliderViewModel> GetSlider(int SliderID);
       string AddUpdateSlider(SliderViewModel objSlider);
       string DeleteSlider(SliderViewModel objSlider);
        #endregion

        #region Notification
        List<NotificationViewModel> GetNotification(int NotificationID,int ReciverID);
        string AddUpdateNotification(List<NotificationViewModel> objNotification);
        #endregion

        List<TopicViewModel> GetTopicBySubject(string SubjectID);
       List<SubTopicViewModel> GetSubTopicByTopic(string TopicID);
    }
}
