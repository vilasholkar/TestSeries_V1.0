using BusinessAccessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModels.Account;

namespace OnlineTestApplication.Controllers
{
    public class StudentController : ApiController
    {
        private readonly IBStudent _iBStudent;
        private readonly IBAccount _iBAccount;

        public StudentController(IBStudent iBStudent,IBAccount iBAccount)
        {
            _iBStudent = iBStudent;
            _iBAccount = iBAccount;
        }
        [HttpGet]
        [Route("api/GetLoginInfo", Name = "GetLoginInfo")]
        public HttpResponseMessage GetLoginInfo(string UserName, string Password, int UserTypeID)
        {
            Login loginsetdetails = new Login();
            loginsetdetails.UserName = UserName;
            loginsetdetails.UserPassword = Password;
            loginsetdetails.UserTypeID = UserTypeID;
            //Login logingetdetails = DataAccessLayer.DAccount.GetUserDetails1(loginsetdetails);
            return Request.CreateResponse(HttpStatusCode.OK, _iBAccount.GetUserDetails(loginsetdetails));

            //if (logingetdetails != null)
            //{
            //    var currentUserRole = logingetdetails.UserType;
            //   // return logingetdetails;
            //    //return Request.CreateResponse(HttpStatusCode.OK, logingetdetails);
            //}
            //else
            //{
            //    return Request.CreateResponse(HttpStatusCode.NoContent, logingetdetails);
            //}
        }
        [HttpGet]
        [Route("api/GetStudentDetails", Name = "GetStudentDetails")]
        public HttpResponseMessage GetStudentDetails()
        {
            //var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_iBStudent.GetStudent()));
            //return response;
            return Request.CreateResponse(HttpStatusCode.OK, _iBStudent.GetStudentDetails());
        }
    }
}
