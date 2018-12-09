using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;

namespace OnlineTestApplication.Controllers
{
    public class ResultController : ApiController
    {
        private readonly IBResult _iBResult;

        public ResultController(IBResult iBResult)
        {
            _iBResult = iBResult;
        }

        [HttpGet]
        [Route("api/ResultAnalysis", Name = "ResultAnalysis")]
        public HttpResponseMessage ResultAnalysis(int TestID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBResult.ResultAnalysis(TestID));
        }

        //[HttpGet]
        //[Route("api/GetOnlineTestResultByTestID", Name = "GetOnlineTestResultByTestID")]
        //public HttpResponseMessage GetOnlineTestResultByTestID(int TestID)
        //{
        //    DataTable dt =_iBResult.GetOnlineTestResultByTestID(TestID);
        //    if(dt.Rows.Count>0)
        //    return Request.CreateResponse(HttpStatusCode.OK, dt);
        //    else
        //    return Request.CreateResponse(HttpStatusCode.OK, "No Records.");
        //}

        //[HttpGet]
        //[Route("api/GetOnlineTestResultByStudentID", Name = "GetOnlineTestResultByStudentID")]
        //public HttpResponseMessage GetOnlineTestResultByStudentID(int StudentID)
        //{
        //   // return Request.CreateResponse(HttpStatusCode.OK, _iBResult.GetOnlineTestResultByStudentID(StudentID));
        //    DataTable dt = _iBResult.GetOnlineTestResultByStudentID(StudentID);
        //    if (dt.Rows.Count > 0)
        //        return Request.CreateResponse(HttpStatusCode.OK, dt);
        //    else
        //        return Request.CreateResponse(HttpStatusCode.OK, "No Records.");
        //}
        
        [HttpGet]
        [Route("api/GetResultAnalysis", Name = "GetResultAnalysis")]
        public HttpResponseMessage GetResultAnalysis(int StudentID,int TestID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBResult.GetResultAnalysis(StudentID,TestID));
        }
        [HttpGet]
        [Route("api/GetPaperAnalysis", Name = "GetPaperAnalysis")]
        public HttpResponseMessage GetPaperAnalysis(int TestID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBResult.GetPaperAnalysis(TestID));
        }

        [HttpGet]
        [Route("api/GetStudentAttempt", Name = "GetStudentAttempt")]
        public HttpResponseMessage GetStudentAttempt(int StudentID, int TestID)
        {
             return Request.CreateResponse(HttpStatusCode.OK, _iBResult.GetStudentAttempt(StudentID,TestID));
        }
       
        [HttpGet]
        [Route("api/GetOnlineTestResultByID", Name = "GetOnlineTestResultByID")]
        public HttpResponseMessage GetOnlineTestResultByID(int StudentID, int TestID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBResult.GetOnlineTestResultByID(StudentID, TestID));

            
        }
        //[HttpGet]
        //[Route("api/GetStudentMarksReview", Name = "GetStudentMarksReview")]
        //public HttpResponseMessage GetStudentMarksReview(int StudentID, int TestID)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, _iBResult.GetStudentMarksReview(StudentID, TestID));
        //}
        
        [HttpGet]
        [Route("api/GetTopper_Average", Name = "GetTopper_Average")]
        public HttpResponseMessage GetTopper_Average(int TestID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBResult.GetTopper_Average(TestID));
        }

    }
}