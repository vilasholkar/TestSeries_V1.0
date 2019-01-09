using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Test;

namespace BusinessAccessLayer
{
    public class BTestSeries : IBTestSeries
    {
        private readonly IDTestSeries _iDTestSeries;
        public BTestSeries(IDTestSeries iDTestSeries)
        {
            _iDTestSeries = iDTestSeries;
        }
        public Response<List<TestSeriesViewModel>> GetTestSeries()
        {
            var TestSeriesData = _iDTestSeries.GetTestSeries();
            if (TestSeriesData != null)
            {
                return new Response<List<TestSeriesViewModel>>
                {
                    IsSuccessful = true,
                    Object = TestSeriesData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<TestSeriesViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }
        public string AddUpdateTestSeries(TestSeriesViewModel objTestSeries)
        {
            return _iDTestSeries.AddUpdateTestSeries(objTestSeries);
        }
        public string DeleteTestSeries(TestSeriesViewModel objTestSeries)
        {
            return _iDTestSeries.DeleteTestSeries(objTestSeries);
        }
    }
}
