Imports Microsoft.VisualBasic
Imports AuthenticationOwin.Module
Imports AuthenticationOwin.Module.BusinessObjects
Imports AuthenticationOwin.Module.Web.Security
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Web
Imports Microsoft.Owin.Security
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports System.Web
Imports Microsoft.AspNet.Identity.Owin

Namespace AuthenticationOwin.Web.Security
    Public Class OAuthProvider
        Implements IAuthenticationProvider
        Private ReadOnly userType As Type
        Private ReadOnly security As SecurityStrategyComplex
        Private privateCreateUserAutomatically As Boolean
        Public Property CreateUserAutomatically() As Boolean
            Get
                Return privateCreateUserAutomatically
            End Get
            Set(ByVal value As Boolean)
                privateCreateUserAutomatically = value
            End Set
        End Property
        Public Sub New(ByVal userType As Type, ByVal security As SecurityStrategyComplex)
            Guard.ArgumentNotNull(userType, "userType")
            Me.userType = userType
            Me.security = security
        End Sub

        Public Function Authenticate(ByVal objectSpace As IObjectSpace) As Object Implements IAuthenticationProvider.Authenticate
            Dim user As IAuthenticationOAuthUser = Nothing
            Dim externalLoginInfo = Authenticate()
            If externalLoginInfo IsNot Nothing Then
                Dim userEmail = externalLoginInfo.Email
                If userEmail IsNot Nothing Then
                    user = CType(objectSpace.FindObject(userType, CriteriaOperator.Parse("OAuthAuthenticationEmails[Email = ?]", userEmail)), IAuthenticationOAuthUser)
                    If user Is Nothing AndAlso CreateUserAutomatically Then
                        user = CType(objectSpace.CreateObject(userType), IAuthenticationOAuthUser)
                        user.UserName = userEmail
                        Dim email As EmailEntity = objectSpace.CreateObject(Of EmailEntity)()
                        email.Email = userEmail
                        user.OAuthAuthenticationEmails.Add(email)
                        CType(security, CustomSecurityStrategyComplex).InitializeNewUser(objectSpace, user)
                        objectSpace.CommitChanges()
                    End If
                End If
            Else
                WebApplication.Redirect(WebApplication.LogonPage)
            End If
            If user Is Nothing Then
                Throw New Exception("Login failed")
            End If
            Return user
        End Function

        Public Sub Setup(ParamArray ByVal args() As Object) Implements IAuthenticationProvider.Setup
        End Sub
        Private Function Authenticate() As ExternalLoginInfo
            Return HttpContext.Current.GetOwinContext().Authentication.GetExternalLoginInfo()
        End Function
    End Class
End Namespace