using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using AuthenticationOwin.Module.Web.Security;
using AuthenticationOwin.Module;
using System.Security.Claims;
using System.Linq;
using AuthenticationOwin.Module.BusinessObjects;
using System;
using DevExpress.Persistent.Base.Security;
using DevExpress.ExpressApp.Web;

namespace AuthenticationOwin.Web.Security {
    public class AuthenticationStandartWithOAuth : AuthenticationStandard {
        public bool CreateUserAutomatically { get; set; }
        private async Task<AuthenticateResult> Authenticate() {
            return await HttpContext.Current.GetOwinContext().Authentication.AuthenticateAsync("External");
        }
        public override void Logoff() {
            if(HttpContext.Current.Request.Cookies[".AspNet.External"] != null) {
                HttpContext.Current.Response.Cookies[".AspNet.External"].Expires = DateTime.Now.AddDays(-1);
            }
            base.Logoff();
        }
        public override object Authenticate(IObjectSpace objectSpace) {
            IAuthenticationOAuthUser user = null;
            if(AuthenticationOwin.Module.Web.Controllers.LogonAuthController.IsOAuthRequest) {
                AuthenticateResult authenticateResult = Authenticate().Result;
                if(authenticateResult != null) {
                    Claim emailClaim = authenticateResult.Identity.FindFirst(ClaimTypes.Email);
                    if(emailClaim != null) {
                        user = (IAuthenticationOAuthUser)objectSpace.FindObject(UserType, CriteriaOperator.Parse(string.Format("OAuthAuthenticationEmails[Email = '{0}']", emailClaim.Value)));
                        if(user == null && CreateUserAutomatically) {
                            user = (IAuthenticationOAuthUser)objectSpace.CreateObject(UserType);
                            user.UserName = emailClaim.Value;
                            EmailEntity email = objectSpace.CreateObject<EmailEntity>();
                            email.Email = emailClaim.Value;
                            user.OAuthAuthenticationEmails.Add(email);
                            ((CustomSecurityStrategyComplex)Security).InitializeNewUser(objectSpace, user);
                            objectSpace.CommitChanges();
                        }
                    }
                }
                else {
                    //TODO
                    WebApplication.Redirect(WebApplication.LogonPage);
                }
                if(user == null) {
                    throw new Exception("Login failed");
                }
                return user;
            }
            else {
                user = base.Authenticate(objectSpace) as IAuthenticationOAuthUser;
                if(user != null && !user.EnableStandardAuthentication) {
                    throw new InvalidOperationException("Password authentication is not allowed for this user.");
                }
                return user;
            }
        }
    }

}