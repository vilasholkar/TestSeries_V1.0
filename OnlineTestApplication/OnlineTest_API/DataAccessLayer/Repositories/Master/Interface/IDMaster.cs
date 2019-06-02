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
       string AddUpdateTopic(TopicViewModel objTopic);
       string DeleteTopic(TopicViewModel objTopic);
       #endregion

       #region SubTopic
       List<SubTopicViewModel> GetSubTopic();
       string AddUpdateSubTopic(SubTopicViewModel objSubTopic);
       string DeleteSubTopic(SubTopicViewModel objSubTopic);
       #endregion
    }
}
