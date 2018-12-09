using BusinessAccessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnlineTestApplication.Controllers
{
    public class StudentController : ApiController
    {
        private readonly IBStudent _iBStudent;

        public StudentController(IBStudent iBStudent)
        {
            _iBStudent = iBStudent;
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
