using BusinessAccessLayer;
using OnlineTestApplication.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels.Master;

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

        [HttpGet]
        [Route("api/GetSubject", Name = "GetSubject")]
        public HttpResponseMessage GetSubject()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetSubject());
        }

        [HttpGet]
        [Route("api/GetTopic", Name = "GetTopic")]
        public HttpResponseMessage GetTopic()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetTopic());
        }

        [HttpPost]
        [Route("api/AddUpdateTopic", Name = "AddUpdateTopic")]
        public HttpResponseMessage AddUpdateTopic(TopicViewModel objTopic)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.AddUpdateTopic(objTopic));
        }

        [HttpPost]
        [Route("api/DeleteTopic", Name = "DeleteTopic")]
        public HttpResponseMessage DeleteTopic(TopicViewModel objTopic)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.DeleteTopic(objTopic));
        }
        [HttpGet]
        [Route("api/GetSubTopic", Name = "GetSubTopic")]
        public HttpResponseMessage GetSubTopic()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetSubTopic());
        }

        [HttpPost]
        [Route("api/AddUpdateSubTopic", Name = "AddUpdateSubTopic")]
        public HttpResponseMessage GetSubTopic(SubTopicViewModel objSubTopic)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.AddUpdateSubTopic(objSubTopic));
        }
        [HttpPost]
        [Route("api/DeleteSubTopic", Name = "DeleteSubTopic")]
        public HttpResponseMessage DeleteTestType(SubTopicViewModel objSubTopic)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.DeleteSubTopic(objSubTopic));
        }
    }
}
