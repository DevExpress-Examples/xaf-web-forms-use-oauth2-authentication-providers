using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.MicrosoftAccount;
using Owin;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(MySolution.Web.Startup))]

namespace MySolution.Web {
    public class Startup {
        private static string microsoftClientID = ConfigurationManager.AppSettings["MicrosoftClientID"];
        private static string microsoftClientSecret = ConfigurationManager.AppSettings["MicrosoftClientSecret"];

        public void Configuration(IAppBuilder app) {
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            if ((!string.IsNullOrEmpty(microsoftClientID) && !string.IsNullOrEmpty(microsoftClientSecret))) {
                MicrosoftAccountAuthenticationOptions microsoftAccountAuthenticationOptions = new MicrosoftAccountAuthenticationOptions {
                    ClientId = microsoftClientID,
                    ClientSecret = microsoftClientSecret,
                    Provider = new MicrosoftAccountAuthenticationProvider() {
                        OnAuthenticated = (context) => {
                            var email = context.User["userPrincipalName"];
                            if (email != null) {
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