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
       List<CourseViewModel> GetCourseByStream(int StreamId);
       List<BatchViewModel> GetBatchByCourse(int CourseId);
       List<SessionViewModel> GetSession();
    }
}
