using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Text;

namespace OnlineTestApplication.CustomFilters
{
    public class CustomAuthorizationFilter : AuthorizationFilterAttribute
    {
        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //    base.OnAuthorization(actionContext);

        //    string status = HttpContext.Current.User.Identity.AuthenticationType;

        //    //HttpCookie authCookie = actionContext.RequestContext.Principal.Identity.IsAuthenticated;

        //    //if (actionContext.Request.Headers.GetValues("authenticationToken") != null)
        //    //{
        //    //    // get value from header
        //    //    string authenticationToken = Convert.ToString(
        //    //      actionContext.Request.Headers.GetValues("authenticationToken").FirstOrDefault());
        //    //    //authenticationTokenPersistant
        //    //    // it is saved in some data store
        //    //    // i will compare the authenticationToken sent by client with
        //    //    // authenticationToken persist in database against specific user, and act accordingly
        //    //    if (authenticationTokenPersistant != authenticationToken)
        //    //    {
        //    //        HttpContext.Current.Response.AddHeader("authenticationToken", authenticationToken);
        //    //        HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
        //    //        actionContext.Response = actionContext.Request.;
        //    //        return;
        //    //    }

        //    //    HttpContext.Current.Response.AddHeader("authenticationToken", authenticationToken);
        //    //    HttpContext.Current.Response.AddHeader("AuthenticationStatus", "Authorized");
        //    //    return;
        //    //}
        //    //actionContext.Response =
        //    //  actionContext.Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        //    //actionContext.Response.ReasonPhrase = "Please provide valid inputs";
        //}

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //if (Active)
            //{
            //    var identity = ParseAuthorizationHeader(actionContext);
            //    if (identity == null)
            //    {
            //        Challenge(actionContext);
            //        return;
            //    }


            //    if (!OnAuthorizeUser(identity.Name, identity.Password, actionContext))
            //    {
            //        Challenge(actionContext);
            //        return;
            //    }

            //    var principal = new GenericPrincipal(identity, null);

            //    Thread.CurrentPrincipal = principal;

            //    // inside of ASP.NET this is required
            //    //if (HttpContext.Current != null)
            //    //    HttpContext.Current.User = principal;

            //    base.OnAuthorization(actionContext);
            //}
        }

        protected virtual bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            return true;
        }

        protected virtual BasicAuthenticationIdentity ParseAuthorizationHeader(HttpActionContext actionContext)
        {
            //string authHeader = null;
            //var auth = actionContext.Request.Headers.Authorization;
            //if (auth != null && auth.Scheme == "Basic")
            //    authHeader = auth.Parameter;

            //if (string.IsNullOrEmpty(authHeader))
            //    return null;

            //authHeader = Encoding.Default.GetString(Convert.FromBase64String(authHeader));

            //var tokens = authHeader.Split(':', 2);
            //if (tokens.Length < 2)
            //    return null;

            return new BasicAuthenticationIdentity("","");
        }

        void Challenge(HttpActionContext actionContext)
        {
            //var host = actionContext.Request.RequestUri.DnsSafeHost;
            //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            //actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", host));
        }
    }

    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public string Password { get; set; }
        public BasicAuthenticationIdentity(string name, string password)
            : base(name, "Basic")
        {
            this.Password = password;
        } /// 

        /// Basic Auth Password for custom authentication ///  public string Password { get; set; }
    }
}