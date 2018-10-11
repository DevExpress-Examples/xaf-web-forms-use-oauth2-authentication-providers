using AuthenticationOwin.Module;
using AuthenticationOwin.Module.BusinessObjects;
using AuthenticationOwin.Module.Web.Security;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AuthenticationOwin.Web.Security {
    public class OAuthProvider : IAuthenticationProvider {
        private readonly Type userType;
        private readonly SecurityStrategyComplex security;
        public bool CreateUserAutomatically { get; set; }
        public OAuthProvider(Type userType, SecurityStrategyComplex security) {
            Guard.ArgumentNotNull(userType, "userType");
            this.userType = userType;
            this.security = security;
        }
   
        public object Authenticate(IObjectSpace objectSpace) {
            IAuthenticationOAuthUser user = null;
            AuthenticateResult authenticateResult = Authenticate().Result;
            if(authenticateResult != null) {
                Claim emailClaim = authenticateResult.Identity.FindFirst(ClaimTypes.Email);
                if(emailClaim != null) {
                    user = (IAuthenticationOAuthUser)objectSpace.FindObject(userType, CriteriaOperator.Parse(string.Format("OAuthAuthenticationEmails[Email = '{0}']", emailClaim.Value)));
                    if(user == null && CreateUserAutomatically) {
                        user = (IAuthenticationOAuthUser)objectSpace.CreateObject(userType);
                        user.UserName = emailClaim.Value;
                        EmailEntity email = objectSpace.CreateObject<EmailEntity>();
                        email.Email = emailClaim.Value;
                        user.OAuthAuthenticationEmails.Add(email);
                        ((CustomSecurityStrategyComplex)security).InitializeNewUser(objectSpace, user);
                        objectSpace.CommitChanges();
                    }
                }
            }
            else {
                WebApplication.Redirect(WebApplication.LogonPage);
            }
            if(user == null) {
                throw new Exception("Login failed");
            }
            return user;
        }

        public void Setup(params object[] args) {            
        }
        private async Task<AuthenticateResult> Authenticate() {
            return await HttpContext.Current.GetOwinContext().Authentication.AuthenticateAsync("External");
        }
    }
}