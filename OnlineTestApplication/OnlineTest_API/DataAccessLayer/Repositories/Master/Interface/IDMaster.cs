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
    }
}
