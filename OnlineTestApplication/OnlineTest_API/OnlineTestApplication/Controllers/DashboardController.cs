using BusinessAccessLayer;
using OnlineTestApplication.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineTestApplication.Controllers
{
      [CustomExceptionFilter]
    public class DashboardController : ApiController
    {
        private readonly IBStudentDashboard _iBStudentDashboard;

        public DashboardController(IBStudentDashboard iBStudentDashboard)
        {
            _iBStudentDashboard = iBStudentDashboard;
        }

        [HttpGet]
        [Route("api/GetStudentDashboardDetail", Name = "GetStudentDashboardDetail")]
        public HttpResponseMessage GetStudentDashboardDetail(int StudentID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBStudentDashboard.GetStudentDashboardDetail(StudentID));
        }
    }
}
