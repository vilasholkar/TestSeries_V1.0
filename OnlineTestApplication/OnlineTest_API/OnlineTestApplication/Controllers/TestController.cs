using BusinessAccessLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private readonly IBOnlineTest _iBOnlineTest;
        private readonly IBEligibleStudent _iBEligibleStudent;

        public TestController(IBTestSeries iBTestSeries, IBOnlineTest iBOnlineTest, IBEligibleStudent iBEligibleStudent)
        {
            _iBTestSeries = iBTestSeries;
            _iBOnlineTest = iBOnlineTest;
            _iBEligibleStudent = iBEligibleStudent;
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

        #region OnlineTest

        [HttpGet]
        [Route("api/GetOnlineTest", Name = "GetOnlineTest")]
        public HttpResponseMessage GetOnlineTest()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBOnlineTest.GetOnlineTest());
        }

        [HttpPost]
        [Route("api/AddUpdateOnlineTest", Name = "AddUpdateOnlineTest")]
        public HttpResponseMessage AddUpdateOnlineTest(OnlineTestViewModel objTestSeries)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBOnlineTest.AddUpdateOnlineTest(objTestSeries));
        }

        [HttpPost]
        [Route("api/DeleteOnlineTest", Name = "DeleteOnlineTest")]
        public HttpResponseMessage DeleteOnlineTest(object OnlineTestId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBOnlineTest.DeleteOnlineTest(Convert.ToInt32(OnlineTestId)));
        }

        [HttpGet]
        [Route("api/GetOnlineTestById", Name = "GetOnlineTestById")]
        public HttpResponseMessage GetOnlineTestById(int OnlineTestId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBOnlineTest.GetOnlineTestById(OnlineTestId));
        }

        [HttpGet]
        [Route("api/GetQuestionsByTestId", Name = "GetQuestionsByTestId")]
        public HttpResponseMessage GetQuestionsByTestId(int OnlineTestId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBOnlineTest.GetQuestionsByTestId(OnlineTestId));
        }

        [HttpGet]
        [Route("api/GetOnlineTestByStudentID", Name = "GetOnlineTestByStudentID")]
        public HttpResponseMessage GetOnlineTestByStudentID(int StudentID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBOnlineTest.GetOnlineTestByStudentID(StudentID));
        }
        [HttpGet]
        [Route("api/GetOnlineTestMasterDataByTestID", Name = "GetOnlineTestMasterDataByTestID")]
        public HttpResponseMessage GetOnlineTestMasterDataByTestID(int OnlineTestID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBOnlineTest.GetOnlineTestMasterDataByTestID(OnlineTestID));
        }
       
        [HttpGet]
        [Route("api/GetOnlineTest_ForGenerateResult", Name = "GetOnlineTest_ForGenerateResult")]
        public HttpResponseMessage GetOnlineTest_ForGenerateResult()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBOnlineTest.GetOnlineTest_ForGenerateResult());
        }
        #endregion

        #region EligibleStudent
        [HttpGet]
        [Route("api/GetEligibleStudent", Name = "GetEligibleStudent")]
        public HttpResponseMessage GetEligibleStudent(int OnlineTestID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBEligibleStudent.GetEligibleStudent(OnlineTestID));
        }
        [HttpPost]
        [Route("api/AddEligibleStudent", Name = "AddEligibleStudent")]
        public HttpResponseMessage AddEligibleStudent(List<EligibleStudentViewModel> EligibleStudentData)
        {
            return Request.CreateResponse(HttpStatusCode.OK,_iBEligibleStudent.AddEligibleStudent(EligibleStudentData));
        }
        #endregion
    }
}