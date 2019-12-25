using BusinessAccessLayer;
using OnlineTestApplication.CustomFilters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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

        #region Topic
        [HttpGet]
        [Route("api/GetTopic", Name = "GetTopic")]
        public HttpResponseMessage GetTopic()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetTopic());
        }

        [HttpGet]
        [Route("api/GetTopicByID", Name = "GetTopicByID")]
        public HttpResponseMessage GetTopicByID(int TopicID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetTopicByID(TopicID));
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
        #endregion
        #region SubTopic
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
        #endregion

        #region Slider
        [HttpGet]
        [Route("api/GetSlider", Name = "GetSlider")]
        public HttpResponseMessage GetSlider(int SliderID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetSlider(SliderID));
        }
        [HttpPost]
        [Route("api/AddUpdateSlider", Name = "AddUpdateSlider")]
        public HttpResponseMessage AddUpdateSlider(SliderViewModel objSlider)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.AddUpdateSlider(objSlider));
        }
        [HttpPost]
        [Route("api/DeleteSlider", Name = "DeleteSlider")]
        public HttpResponseMessage DeleteTestType(SliderViewModel objSlider)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.DeleteSlider(objSlider));
        }

        [HttpPost]
        [Route("api/UploadSliderImage", Name = "UploadSliderImage")]
        public HttpResponseMessage UploadSliderImage()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    string dynamicPath = string.Empty;
                    dynamicPath = "/Uploads/Slider/";
                    var filePath = HttpContext.Current.Server.MapPath("~" + dynamicPath + string.Format("File_{0}", postedFile.FileName));
                    var dbpath = dynamicPath + string.Format("File_{0}", postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    return Request.CreateResponse(HttpStatusCode.OK, dbpath);

                }
            }
            return response;
        }


        #endregion

        #region Notification
        [HttpGet]
        [Route("api/GetNotification", Name = "GetNotification")]
        public HttpResponseMessage GetNotification(int NotificationID, int ReciverID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetNotification(NotificationID, ReciverID));
        }
        [HttpPost]
        [Route("api/AddUpdateNotification", Name = "AddUpdateNotification")]
        public HttpResponseMessage AddUpdateNotification(List<NotificationViewModel> objNotification)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.AddUpdateNotification(objNotification));
        }
        #endregion

        [HttpPost]
        [Route("api/GetTopicBySubject", Name = "GetTopicBySubject")]
        public HttpResponseMessage GetTopicBySubject(int[] SubjectID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetTopicBySubject(string.Join(",", SubjectID)));
        }
        [HttpPost]
        [Route("api/GetSubTopicByTopic", Name = "GetSubTopicByTopic")]
        public HttpResponseMessage GetSubTopicByTopic(int[] TopicID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetSubTopicByTopic(string.Join(",", TopicID)));
        }

        [HttpGet]
        [Route("api/GetTopicBySubject1", Name = "GetTopicBySubject1")]
        public HttpResponseMessage GetTopicBySubject1(int SubjectID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetTopicBySubject(SubjectID.ToString()));
        }
        [HttpGet]
        [Route("api/GetSubTopicByTopic1", Name = "GetSubTopicByTopic1")]
        public HttpResponseMessage GetSubTopicByTopic1(int TopicID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBMaster.GetSubTopicByTopic(TopicID.ToString()));
        }

    }
}
