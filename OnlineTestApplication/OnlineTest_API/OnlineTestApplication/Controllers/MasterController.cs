using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineTestApplication.Controllers
{
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
    }
}
