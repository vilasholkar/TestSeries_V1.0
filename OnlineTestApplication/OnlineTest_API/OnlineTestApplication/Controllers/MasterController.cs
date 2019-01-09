using BusinessAccessLayer;
using OnlineTestApplication.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineTestApplication.Controllers
{
    [CustomExceptionFilter]
    public class MasterController : ApiController
    {
        private readonly IBMaster _iBMaster;

        public MasterController(IBMaster iBMaster)
        {
            _iBMaster = iBMaster;
        }

        [HttpGet]
        [Route("api/GetStream", Name = "GetStream")]
        public HttpResponseMessage GetStream()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetStream());
        }
        [HttpPost]
        [Route("api/GetCourseByStream", Name = "GetCourseByStream")]
        public HttpResponseMessage GetCourseByStream(int[] StreamId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetCourseByStream(string.Join(",", StreamId)));
        }
        [HttpPost]
        [Route("api/GetBatchByCourse", Name = "GetBatchByCourse")]
        public HttpResponseMessage GetBatchByCourse(int[] CourseId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetBatchByCourse(string.Join(",", CourseId)));
        }
        [HttpGet]
        [Route("api/GetSession", Name = "GetSession")]
        public HttpResponseMessage GetSession()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetSession());
        }
        [HttpGet]
        [Route("api/GetMasterData", Name = "GetMasterData")]
        public HttpResponseMessage GetMasterData()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetMasterData());
        }

        [HttpPost]
        [Route("api/GetUserInfo", Name = "GetUserInfo")]
        public HttpResponseMessage GetUserInfo(string data)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetMasterData());
        }
    }
}
