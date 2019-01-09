using BusinessAccessLayer;
using Newtonsoft.Json;
using OnlineTestApplication.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModels.TestType;

namespace OnlineTestApplication.Controllers
{
    [CustomExceptionFilter]
    public class TestTypeController : ApiController
    {
        private readonly IBTestType _iBTestType;

        public TestTypeController(IBTestType iBTestType)
        {
            _iBTestType = iBTestType;
        }

        [HttpGet]
        [Route("api/GetTestType", Name = "GetTestType")]
        public HttpResponseMessage GetTestType()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBTestType.GetTestType());            
        }
        [HttpPost]
        [Route("api/AddUpdateTestType", Name = "AddUpdateTestType")]
        public HttpResponseMessage AddUpdateTestType(TestTypeViewModel objTestType)
        {            
            return Request.CreateResponse(HttpStatusCode.OK, _iBTestType.AddUpdateTestType(objTestType));            
        }
        [HttpPost]
        [Route("api/DeleteTestType", Name = "DeleteTestType")]
        public HttpResponseMessage DeleteTestType(TestTypeViewModel objTestType)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBTestType.DeleteTestType(objTestType));
        }
    }
}
