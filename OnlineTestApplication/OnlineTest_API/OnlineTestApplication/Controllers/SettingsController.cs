using BusinessAccessLayer;
using OnlineTestApplication.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels.Settings;

namespace OnlineTestApplication.Controllers
{
    [CustomExceptionFilter]
    public class SettingsController : ApiController
    {
         private readonly IBGeneralSettings _iBGeneralSettings;

         public SettingsController(IBGeneralSettings iBGeneralSettings)
        {
            _iBGeneralSettings = iBGeneralSettings;
        }

        [HttpGet]
        [Route("api/GetGeneralSettings", Name = "GetGeneralSettings")]
        public HttpResponseMessage GetGeneralSettings()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBGeneralSettings.GetGeneralSettings());
        }

        [HttpPost]
        [Route("api/ChangePassword", Name = "ChangePassword")]
        public HttpResponseMessage ChangePassword(ChangePasswordViewModel changepassworddata)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBGeneralSettings.ChangePassword(changepassworddata));
        }
    }
}
