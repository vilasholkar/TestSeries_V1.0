using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Test;

namespace BusinessAccessLayer
{
    public interface IBTestSeries
    {
       Response<List<TestSeriesViewModel>> GetTestSeries();
       string AddUpdateTestSeries(TestSeriesViewModel objTestSeries);
       string DeleteTestSeries(TestSeriesViewModel objTestSeries);
    }
}
