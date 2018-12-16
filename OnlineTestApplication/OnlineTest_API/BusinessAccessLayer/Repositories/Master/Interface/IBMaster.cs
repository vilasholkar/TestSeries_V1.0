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
    }
}
