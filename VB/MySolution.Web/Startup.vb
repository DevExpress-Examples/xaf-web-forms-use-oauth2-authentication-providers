Imports Microsoft.AspNet.Identity
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.MicrosoftAccount
Imports Owin
Imports System.Configuration
Imports System.Security.Claims
Imports System.Threading.Tasks

<Assembly: OwinStartup(GetType(MySolution.Web.Startup))>

Namespace MySolution.Web
	Public Class Startup
		Private Shared microsoftClientID As String = ConfigurationManager.AppSettings("MicrosoftClientID")
		Private Shared microsoftClientSecret As String = ConfigurationManager.AppSettings("MicrosoftClientSecret")

		Public Sub Configuration(ByVal app As IAppBuilder)
			app.UseCookieAuthentication(New CookieAuthenticationOptions With {.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie})

			app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie)

			If (Not String.IsNullOrEmpty(microsoftClientID) AndAlso Not String.IsNullOrEmpty(microsoftClientSecret)) Then
				Dim microsoftAccountAuthenticationOptions As MicrosoftAccountAuthenticationOptions = New MicrosoftAccountAuthenticationOptions With {.ClientId = microsoftClientID, .ClientSecret = microsoftClientSecret, .Provider = New MicrosoftAccountAuthenticationProvider() With {.OnAuthenticated = Function(context)
					Dim email = context.User("userPrincipalName")
					If email IsNot Nothing Then
						context.Identity.AddClaim(New Claim(ClaimTypes.Email, email.ToString()))
					End If
					Return Task.FromResult(0)
				End Function}}
				app.UseMicrosoftAccountAuthentication(microsoftAccountAuthenticationOptions)
			End If
		End Sub
	End Class
End Namespace