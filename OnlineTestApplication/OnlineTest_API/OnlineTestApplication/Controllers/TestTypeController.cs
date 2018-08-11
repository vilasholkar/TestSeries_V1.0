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
    public class TestTypeController : ApiController
    {
        private readonly IBTestType _iBTestType;

        public TestTypeController(IBTestType iBTestType)
        {
            _iBTestType = iBTestType;
        }

        [HttpGet]
        [Route("api/GetTestType", Name = "GetTestType")]
        public async Task<HttpResponseMessage> GetTestType()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_iBTestType.GetTestType()));
            return response;
        }
    }
}
