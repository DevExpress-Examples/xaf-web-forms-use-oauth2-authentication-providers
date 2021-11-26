Imports System
Imports System.Web
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Web
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports MySolution.Module.BusinessObjects
Imports MySolution.Module.Web.Security

Namespace MySolution.Web.Security
	Public Class OAuthProvider
		Implements IAuthenticationProvider

		Private ReadOnly userType As Type
		Private ReadOnly security As SecurityStrategyComplex
		Public Property CreateUserAutomatically() As Boolean
		Public Sub New(ByVal userType As Type, ByVal security As SecurityStrategyComplex)
			Guard.ArgumentNotNull(userType, "userType")
			Me.userType = userType
			Me.security = security
		End Sub
		Public Function Authenticate(ByVal objectSpace As IObjectSpace) As Object Implements DevExpress.ExpressApp.Security.IAuthenticationProviderV2.Authenticate
			Dim user As ApplicationUser = Nothing
			Dim externalLoginInfo As ExternalLoginInfo = Authenticate()
			If externalLoginInfo IsNot Nothing Then
				Dim userEmail As String = externalLoginInfo.Email
				If userEmail IsNot Nothing Then
					user = DirectCast(objectSpace.FindObject(userType, CriteriaOperator.Parse("OAuthAuthenticationEmails[Email = ?]", userEmail)), ApplicationUser)
					If user Is Nothing AndAlso CreateUserAutomatically Then
						user = DirectCast(objectSpace.CreateObject(userType), ApplicationUser)
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
		Private Function Authenticate() As ExternalLoginInfo
			Return HttpContext.Current.GetOwinContext().Authentication.GetExternalLoginInfo()
		End Function
		Public Sub Setup(ParamArray ByVal args() As Object) Implements IAuthenticationProvider.Setup
		End Sub
	End Class
End Namespace