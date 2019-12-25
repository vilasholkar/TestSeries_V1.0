using BusinessAccessLayer;
using DataAccessLayer;
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
            //string token = "fVfadVvz12c:APA91bHuWO-3dl8duadxHCdyFyd_nX00L9gJV_MOOcwsWVSf3NiDf7U9uiCXE2xbsqX_v0dYNrc6DNehYzouI4E-9seKj5eSwVZL3ieTpZ-H9QBAIxKxO-4rJUOnY2eEu4JS19v3QNsH";
            //return Request.CreateResponse(HttpStatusCode.OK, DSMSGeneric.SendPushNotification(token,"));
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
