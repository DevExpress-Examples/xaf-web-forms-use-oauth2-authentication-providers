using System;
using System.Web;
using AuthenticationOwin.Module;
using AuthenticationOwin.Module.BusinessObjects;
using AuthenticationOwin.Module.Web.Security;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

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
            ExternalLoginInfo externalLoginInfo = Authenticate();
            if(externalLoginInfo != null) {
                string userEmail = externalLoginInfo.Email;
                if(userEmail != null) {
                    user = (IAuthenticationOAuthUser)objectSpace.FindObject(userType, CriteriaOperator.Parse("OAuthAuthenticationEmails[Email = ?]", userEmail));
                    if(user == null && CreateUserAutomatically) {
                        user = (IAuthenticationOAuthUser)objectSpace.CreateObject(userType);
                        user.UserName = userEmail;
                        EmailEntity email = objectSpace.CreateObject<EmailEntity>();
                        email.Email = userEmail;
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
        private ExternalLoginInfo Authenticate() {
            return HttpContext.Current.GetOwinContext().Authentication.GetExternalLoginInfo();
        }
        public void Setup(params object[] args) {
        }
    }
}
