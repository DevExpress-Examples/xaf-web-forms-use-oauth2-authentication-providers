Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports Microsoft.Owin.Security
Imports System.Threading.Tasks
Imports System.Web
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports AuthenticationOwin.Module.Web.Security
Imports AuthenticationOwin.Module
Imports System.Security.Claims
Imports System.Linq
Imports AuthenticationOwin.Module.BusinessObjects
Imports System
Imports DevExpress.Persistent.Base.Security
Imports DevExpress.ExpressApp.Web

Namespace AuthenticationOwin.Web.Security
    Public Class AuthenticationStandartWithOAuth
        Inherits AuthenticationStandard

        Public Property CreateUserAutomatically() As Boolean
        Private Overloads Async Function Authenticate() As Task(Of AuthenticateResult)
            Return Await HttpContext.Current.GetOwinContext().Authentication.AuthenticateAsync("External")
        End Function
        Public Overrides Sub Logoff()
            If HttpContext.Current.Request.Cookies(".AspNet.External") IsNot Nothing Then
                HttpContext.Current.Response.Cookies(".AspNet.External").Expires = Date.Now.AddDays(-1)
            End If
            MyBase.Logoff()
        End Sub
        Public Overrides Function Authenticate(ByVal objectSpace As IObjectSpace) As Object
            Dim user As IAuthenticationOAuthUser = Nothing
            If AuthenticationOwin.Module.Web.Controllers.LogonAuthController.IsOAuthRequest Then
                Dim authenticateResult As AuthenticateResult = Authenticate().Result
                If authenticateResult IsNot Nothing Then
                    Dim emailClaim As Claim = authenticateResult.Identity.FindFirst(ClaimTypes.Email)
                    If emailClaim IsNot Nothing Then
                        user = DirectCast(objectSpace.FindObject(UserType, CriteriaOperator.Parse(String.Format("OAuthAuthenticationEmails[Email = '{0}']", emailClaim.Value))), IAuthenticationOAuthUser)
                        If user Is Nothing AndAlso CreateUserAutomatically Then
                            user = DirectCast(objectSpace.CreateObject(UserType), IAuthenticationOAuthUser)
                            user.UserName = emailClaim.Value
                            Dim email As EmailEntity = objectSpace.CreateObject(Of EmailEntity)()
                            email.Email = emailClaim.Value
                            user.OAuthAuthenticationEmails.Add(email)
                            CType(Security, CustomSecurityStrategyComplex).InitializeNewUser(objectSpace, user)
                            objectSpace.CommitChanges()
                        End If
                    End If
                Else
                    'TODO
                    WebApplication.Redirect(WebApplication.LogonPage)
                End If
                If user Is Nothing Then
                    Throw New Exception("Login failed")
                End If
                Return user
            Else
                user = TryCast(MyBase.Authenticate(objectSpace), IAuthenticationOAuthUser)
                If user IsNot Nothing AndAlso Not user.EnableStandardAuthentication Then
                    Throw New InvalidOperationException("Password authentication is not allowed for this user.")
                End If
                Return user
            End If
        End Function
    End Class

End Namespace