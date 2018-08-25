using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Test;

namespace DataAccessLayer
{
   public interface IDTestSeries
    {
       List<TestSeriesViewModel> GetTestSeries();
       string AddUpdateTestSeries(TestSeriesViewModel objTestSeries);
       string DeleteTestSeries(TestSeriesViewModel objTestSeries);

    }
}
