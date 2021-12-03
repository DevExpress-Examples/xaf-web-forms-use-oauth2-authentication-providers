Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports System
Imports System.Web

Namespace MySolution.Module.Web.Security
	Public Class CustomSecurityStrategyComplex
		Inherits SecurityStrategyComplex

		Protected Overrides Sub InitializeNewUserCore(ByVal objectSpace As IObjectSpace, ByVal user As Object)
			MyBase.InitializeNewUserCore(objectSpace, user)
		End Sub
		Public Sub InitializeNewUser(ByVal objectSpace As IObjectSpace, ByVal user As Object)
			InitializeNewUserCore(objectSpace, user)
		End Sub
		Public Overrides Sub Logoff()
			If HttpContext.Current.Request.Cookies(".AspNet.External") IsNot Nothing Then
				HttpContext.Current.Response.Cookies(".AspNet.External").Expires = Date.Now.AddDays(-1)
			End If
			MyBase.Logoff()
		End Sub
	End Class
End Namespace
