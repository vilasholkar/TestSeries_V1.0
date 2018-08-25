using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineTestApplication.Controllers
{
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
        [HttpGet]
        [Route("api/GetCourseByStream", Name = "GetCourseByStream")]
        public HttpResponseMessage GetCourseByStream(int StreamId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetCourseByStream(StreamId));
        }
        [HttpGet]
        [Route("api/GetBatchByCourse", Name = "GetBatchByCourse")]
        public HttpResponseMessage GetBatchByCourse(int CourseId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetBatchByCourse(CourseId));
        }
        [HttpGet]
        [Route("api/GetSession", Name = "GetSession")]
        public HttpResponseMessage GetSession()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetSession());
        }
    }
}
