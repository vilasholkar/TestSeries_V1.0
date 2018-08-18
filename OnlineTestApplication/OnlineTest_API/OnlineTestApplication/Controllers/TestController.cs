using BusinessAccessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ViewModels.Test;

namespace OnlineTestApplication.Controllers
{
    public class TestController : ApiController
    {
        private readonly IBTestSeries _iBTestSeries;

        public TestController(IBTestSeries iBTestSeries)
        {
            _iBTestSeries = iBTestSeries;
        }

        #region Test Series
        [HttpGet]
        [Route("api/GetTestSeries", Name = "GetTestSeries")]
        public HttpResponseMessage GetTestSeries()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBTestSeries.GetTestSeries());
        }
        [HttpPost]
        [Route("api/AddUpdateTestSeries", Name = "AddUpdateTestSeries")]
        public HttpResponseMessage AddUpdateTestSeries(TestSeriesViewModel objTestSeries)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBTestSeries.AddUpdateTestSeries(objTestSeries));
        }
        [HttpPost]
        [Route("api/DeleteTestSeries", Name = "DeleteTestSeries")]
        public HttpResponseMessage DeleteTestSeries(TestSeriesViewModel objTestSeries)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBTestSeries.DeleteTestSeries(objTestSeries));
        }
        #endregion
    }
}