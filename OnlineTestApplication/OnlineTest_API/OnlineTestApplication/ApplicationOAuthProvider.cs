using BusinessAccessLayer;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using ViewModels.Account;

namespace OnlineTestApplication
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IBAccount _iBAccount;
        public ApplicationOAuthProvider(){}
        public ApplicationOAuthProvider(IBAccount iBAccount)
        {
            _iBAccount = iBAccount;
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            string UserTypeID = context.Parameters.Where(f => f.Key == "UserTypeID").Select(f => f.Value).SingleOrDefault()[0];
            context.OwinContext.Set<string>("UserTypeID", UserTypeID);

        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            string UserTypeID = context.OwinContext.Get<string>("UserTypeID");
            
            Login loginsetdetails = new Login();
            loginsetdetails.UserName = context.UserName;
            loginsetdetails.UserPassword = context.Password;
            loginsetdetails.UserTypeID =Convert.ToInt32(context.OwinContext.Get<string>("UserTypeID"));
            //Login logingetdetails = _iBAccount.GetUserDetails(loginsetdetails);
            Login logingetdetails = DataAccessLayer.DAccount.GetUserDetails1(loginsetdetails);
            // var currentUserRole = "Admin";
            if (logingetdetails != null)
            {
                var currentUserRole = logingetdetails.UserType;
                identity.AddClaim(new Claim("Role", currentUserRole));
                var props = new AuthenticationProperties(new Dictionary<string, string>
                        {
                            {
                             "DisplayName",context.UserName
                            },
                            {
                             "Role",currentUserRole
                            },
                            {
                             "UserID",logingetdetails.UserID.ToString()
                            },
                            {
                             "FirstName",logingetdetails.FirstName
                            },
                            {
                             "LastName",logingetdetails.LastName
                            },
                            {
                             "MobileNo",logingetdetails.MobileNo
                            }
	                    });
                var token = new AuthenticationTicket(identity, props);
                context.Validated(token);

            }
            else if (context.UserName == "student")
            {
                if (context.Password == "student")
                {
                    var currentUserRole = "Student";
                    identity.AddClaim(new Claim("Role", currentUserRole));
                    var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {
                         "DisplayName",context.UserName
                        },
                        {
                         "Role",currentUserRole         
                        }
	                });
                    var token = new AuthenticationTicket(identity, props);
                    context.Validated(token);
                }
            }
            else if (context.UserName == "superadmin")
            {
                if (context.Password == "superadmin")
                {
                    var currentUserRole = "SuperAdmin";
                    identity.AddClaim(new Claim("Role", currentUserRole));
                    var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {
                         "DisplayName",context.UserName
                        },
                        {
                         "Role",currentUserRole         
                        }
	                });
                    var token = new AuthenticationTicket(identity, props);
                    context.Validated(token);
                }
            }
            else
            context.SetError("invalid_grant", "The username or password is incorrect.");
           // context.Response.Headers.Add("AuthorizationResponse",new[]{"Failed"});
            return; 
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        internal static AuthenticationProperties CreateProperties(string p)
        {
            throw new NotImplementedException();
        }
    }
}