using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Master;

namespace BusinessAccessLayer
{
    public interface IBMaster
    {
        Response<List<StreamViewModel>> GetStream();

        Response<List<CourseViewModel>> GetCourseByStream(string StreamId);

        Response<List<BatchViewModel>> GetBatchByCourse(string CourseId);

        Response<List<SessionViewModel>> GetSession();

        Response<MasterViewModel> GetMasterData();

        Response<List<SubjectViewModel>> GetSubject();

        #region Topic
        Response<List<TopicViewModel>> GetTopic();
        Response<TopicMasterViewModel> GetTopicByID(int TopicID);
        string AddUpdateTopic(TopicViewModel objTopic);
        string DeleteTopic(TopicViewModel objTopic);
        #endregion

        #region SubTopic
        Response<List<SubTopicViewModel>> GetSubTopic();
        string AddUpdateSubTopic(SubTopicViewModel objSubTopic);
        string DeleteSubTopic(SubTopicViewModel objSubTopic);
        #endregion

        #region Slider
        Response<List<SliderViewModel>> GetSlider(int SliderID);
        string AddUpdateSlider(SliderViewModel objSlider);
        string DeleteSlider(SliderViewModel objSlider);
        #endregion

        #region Notification
        Response<List<NotificationViewModel>> GetNotification(int NotificationID, int ReciverID);
        string AddUpdateNotification(List<NotificationViewModel> objNotification);
        #endregion

        Response<List<TopicViewModel>> GetTopicBySubject(string SubjectID);

        Response<List<SubTopicViewModel>> GetSubTopicByTopic(string TopicID);
    }
}
