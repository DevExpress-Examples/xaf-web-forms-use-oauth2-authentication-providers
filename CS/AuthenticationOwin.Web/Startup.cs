using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using System.Net.Http;
using System.Threading;
using Microsoft.Owin.Security.MicrosoftAccount;
using DevExpress.ExpressApp.Web;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(AuthenticationOwin.Web.Startup))]

namespace AuthenticationOwin.Web {
    public class Startup {
        private static string googleClientID = ConfigurationManager.AppSettings["GoogleClientID"];
        private static string googleClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"];
        private static string facebookClientID = ConfigurationManager.AppSettings["FacebookClientID"];
        private static string facebookClientSecret = ConfigurationManager.AppSettings["FacebookClientSecret"];
        private static string microsoftClientID = ConfigurationManager.AppSettings["MicrosoftClientID"];
        private static string microsoftClientSecret = ConfigurationManager.AppSettings["MicrosoftClientSecret"];

        public void Configuration(IAppBuilder app) {
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            if (!string.IsNullOrEmpty(googleClientID) && !string.IsNullOrEmpty(googleClientID)) {
                app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions() {
                    ClientId = googleClientID,
                    ClientSecret = googleClientSecret
                });
            }
            if(!string.IsNullOrEmpty(facebookClientID) && !string.IsNullOrEmpty(facebookClientSecret)) {
                FacebookAuthenticationOptions facebookAuthOptions = new FacebookAuthenticationOptions {
                    AppId = facebookClientID,
                    AppSecret = facebookClientSecret,
                    UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,name,email",
                    Scope = { "email" }
                };
                app.UseFacebookAuthentication(facebookAuthOptions);
            }
            if((!string.IsNullOrEmpty(microsoftClientID) && !string.IsNullOrEmpty(microsoftClientSecret))) {
                MicrosoftAccountAuthenticationOptions microsoftAccountAuthenticationOptions = new MicrosoftAccountAuthenticationOptions {
                    ClientId = microsoftClientID,
                    ClientSecret = microsoftClientSecret,
                    Provider = new MicrosoftAccountAuthenticationProvider() {
                        OnAuthenticated = (context) => {
                            var email = context.User["userPrincipalName"];
                            if(email != null) {
                                context.Identity.AddClaim(new Claim(ClaimTypes.Email, email.ToString()));
                            }
                            return Task.FromResult(0);
                        }
                    }
                };
                app.UseMicrosoftAccountAuthentication(microsoftAccountAuthenticationOptions);
            }
        }
    }
}
