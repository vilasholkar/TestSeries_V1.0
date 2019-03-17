using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Text;
using System.Web.Http;

namespace OnlineTestApplication.CustomFilters
{
    public class CustomAuthorizationFilter : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                base.HandleUnauthorizedRequest(actionContext);
            else
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
        }
    }
}