Imports Microsoft.VisualBasic
Imports System
Imports System.Configuration
Imports System.Threading.Tasks
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.Facebook
Imports Microsoft.Owin.Security.Google
Imports Owin
Imports System.Net.Http
Imports System.Threading
Imports Microsoft.Owin.Security.MicrosoftAccount
Imports DevExpress.ExpressApp.Web
Imports System.Security.Claims
Imports Microsoft.AspNet.Identity

<Assembly: OwinStartup(GetType(AuthenticationOwin.Web.Startup))>

Namespace AuthenticationOwin.Web
    Public Class Startup
        Private Shared googleClientID As String = ConfigurationManager.AppSettings("GoogleClientID")
        Private Shared googleClientSecret As String = ConfigurationManager.AppSettings("GoogleClientSecret")
        Private Shared facebookClientID As String = ConfigurationManager.AppSettings("FacebookClientID")
        Private Shared facebookClientSecret As String = ConfigurationManager.AppSettings("FacebookClientSecret")
        Private Shared microsoftClientID As String = ConfigurationManager.AppSettings("MicrosoftClientID")
        Private Shared microsoftClientSecret As String = ConfigurationManager.AppSettings("MicrosoftClientSecret")

        Public Sub Configuration(ByVal app As IAppBuilder)
            app.UseCookieAuthentication(New CookieAuthenticationOptions With {
                                        .AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie})

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie)

            If (Not String.IsNullOrEmpty(googleClientID)) AndAlso (Not String.IsNullOrEmpty(googleClientID)) Then
                app.UseGoogleAuthentication(New GoogleOAuth2AuthenticationOptions() With {.ClientId = googleClientID, .ClientSecret = googleClientSecret})
            End If
            If (Not String.IsNullOrEmpty(facebookClientID)) AndAlso (Not String.IsNullOrEmpty(facebookClientSecret)) Then
                Dim facebookAuthOptions As FacebookAuthenticationOptions = New FacebookAuthenticationOptions With {.AppId = facebookClientID, .AppSecret = facebookClientSecret, .UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,name,email"}
                app.UseFacebookAuthentication(facebookAuthOptions)
            End If
            If ((Not String.IsNullOrEmpty(microsoftClientID)) AndAlso (Not String.IsNullOrEmpty(microsoftClientSecret))) Then
                Dim microsoftAccountAuthenticationOptions As MicrosoftAccountAuthenticationOptions = New MicrosoftAccountAuthenticationOptions With {
                    .ClientId = microsoftClientID,
                    .ClientSecret = microsoftClientSecret,
                    .Provider = New MicrosoftAccountAuthenticationProvider() With {.OnAuthenticated = Function(context)
                                                                                                          Dim email = context.User("userPrincipalName")
                                                                                                          If (email IsNot Nothing) Then
                                                                                                              context.Identity.AddClaim(New Claim(ClaimTypes.Email, email.ToString()))
                                                                                                          End If
                                                                                                          Return Task.FromResult(0)
                                                                                                      End Function}}
                app.UseMicrosoftAccountAuthentication(microsoftAccountAuthenticationOptions)
            End If
        End Sub
    End Class
End Namespace
