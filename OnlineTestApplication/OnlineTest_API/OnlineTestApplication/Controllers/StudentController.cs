using BusinessAccessLayer;
using Newtonsoft.Json;
using OnlineTestApplication.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModels.Account;
using ViewModels.Student;

namespace OnlineTestApplication.Controllers
{
    [CustomExceptionFilter]
    public class StudentController : ApiController
    {
        private readonly IBStudent _iBStudent;
        private readonly IBAccount _iBAccount;

        public StudentController(IBStudent iBStudent, IBAccount iBAccount)
        {
            _iBStudent = iBStudent;
            _iBAccount = iBAccount;
        }

        #region Student
        //[HttpGet]
        //[Route("api/GetLoginInfo", Name = "GetLoginInfo")]
        //public HttpResponseMessage GetLoginInfo(string UserName, string Password, int UserTypeID)
        //{
        //    Login loginsetdetails = new Login();
        //    loginsetdetails.UserName = UserName;
        //    loginsetdetails.UserPassword = Password;
        //    loginsetdetails.UserTypeID = UserTypeID;
        ////    //Login logingetdetails = DataAccessLayer.DAccount.GetUserDetails1(loginsetdetails);
        //    return Request.CreateResponse(HttpStatusCode.OK, _iBAccount.GetUserDetails(loginsetdetails));
        //}
        [HttpGet]
        [Route("api/GetStudentDetails", Name = "GetStudentDetails")]
        public HttpResponseMessage GetStudentDetails()
        {
            //var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_iBStudent.GetStudent()));
            //return response;
            return Request.CreateResponse(HttpStatusCode.OK, _iBStudent.GetStudentDetails());
        }

        [HttpPost]
        [Route("api/GetFilteredStudent", Name = "GetFilteredStudent")]
        public HttpResponseMessage GetFilteredStudent(StudentReport objSR)
        {
            
            return Request.CreateResponse(HttpStatusCode.OK, _iBStudent.GetFilteredStudent(objSR));
        }
        #endregion

        #region Attendance
        [HttpGet]
        [Route("api/GetAttendance", Name = "GetAttendance")]
        public HttpResponseMessage GetAttendance(string Date, string EnrollmentNo = null)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBStudent.GetAttendance(Date, EnrollmentNo));
        }
        #endregion
    }
}
