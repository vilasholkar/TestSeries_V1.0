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

        Response<List<CourseViewModel>> GetCourseByStream(int StreamId);

        Response<List<BatchViewModel>> GetBatchByCourse(int CourseId);

        Response<List<SessionViewModel>> GetSession();
    }
}
