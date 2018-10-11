Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Web
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security

Namespace AuthenticationOwin.Module.Web.Security
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
				HttpContext.Current.Response.Cookies(".AspNet.External").Expires = DateTime.Now.AddDays(-1)
			End If
			MyBase.Logoff()
		End Sub
	End Class
End Namespace
