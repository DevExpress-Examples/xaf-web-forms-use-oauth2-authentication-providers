using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(MySolution.Web.Startup))]

namespace MySolution.Web {
    public class Startup {
        private static string clientId = ConfigurationManager.AppSettings["ClientId"];
        private static string tenantId = ConfigurationManager.AppSettings["Tenant"];
        private static string authority = string.Format(EnsureTrailingSlash(ConfigurationManager.AppSettings["Authority"]), tenantId);
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["redirectUri"];

        public void Configuration(IAppBuilder app) {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                 new OpenIdConnectAuthenticationOptions {
                     ClientId = clientId,
                     Authority = authority,
                     PostLogoutRedirectUri = postLogoutRedirectUri,
                     Notifications = new OpenIdConnectAuthenticationNotifications() {
                         AuthenticationFailed = (context) => {
                             return Task.FromResult(0);
                         },
                         SecurityTokenValidated = (context) => {
                             string name = context.AuthenticationTicket.Identity.FindFirst("preferred_username").Value;
                             context.AuthenticationTicket.Identity.AddClaim(new Claim(ClaimTypes.Name, name, string.Empty));
                             return Task.FromResult(0);
                         }
                     }
                 });
            app.UseStageMarker(PipelineStage.Authenticate);
        }
        private static string EnsureTrailingSlash(string value) {
            if(value == null) {
                value = string.Empty;
            }

            if(!value.EndsWith("/", StringComparison.Ordinal)) {
                return value + "/";
            }

            return value;
        }
    }
}