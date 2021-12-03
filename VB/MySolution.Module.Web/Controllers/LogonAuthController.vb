Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Web.Utils
Imports Microsoft.Owin.Security

Namespace MySolution.Module.Web.Controllers
	Public Class LogonAuthController
		Inherits ViewController(Of DetailView)

		Public Const OAuthParameter As String = "oauth"
		Private microsoftAction As SimpleAction

		Public Sub New()
			TargetObjectType = GetType(AuthenticationStandardLogonParameters)
			microsoftAction = New SimpleAction(Me, "LoginWithMicrosoft", "OAuthActions")
			AddHandler microsoftAction.Execute, AddressOf microsoftAccountAction_Execute
			microsoftAction.Caption = "Microsoft"
		End Sub
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			Dim logonController As LogonController = Frame.GetController(Of LogonController)()
			If logonController IsNot Nothing Then
				AddHandler logonController.AcceptAction.Changed, AddressOf AcceptAction_Changed
			End If
		End Sub
		Private Sub Challenge(ByVal provider As String)
			Dim redirectUrl As String = WebApplication.LogonPage & "?oauth=true"
			Dim properties As New AuthenticationProperties()
			properties.RedirectUri = redirectUrl
			properties.Dictionary("Provider") = provider
			HttpContext.Current.GetOwinContext().Authentication.Challenge(properties, provider)
		End Sub
		Private Sub microsoftAccountAction_Execute(ByVal sender As Object, ByVal e As SimpleActionExecuteEventArgs)
			Challenge("Microsoft")
		End Sub
		Private Function GetProviderNames() As IList(Of String)
			Dim descriptions As IList(Of AuthenticationDescription) = TryCast(HttpContext.Current.GetOwinContext().Authentication.GetAuthenticationTypes(Function(d As AuthenticationDescription) d.Properties IsNot Nothing AndAlso d.Properties.ContainsKey("Caption")), IList(Of AuthenticationDescription))
			Dim providersNames As New List(Of String)()
			For Each description As AuthenticationDescription In descriptions
				providersNames.Add(description.AuthenticationType)
			Next description
			Return providersNames
		End Function
		Private Sub CurrentRequestPage_Load(ByVal sender As Object, ByVal e As System.EventArgs)
			RemoveHandler DirectCast(sender, Page).Load, AddressOf CurrentRequestPage_Load
			Dim logonController As LogonController = Frame.GetController(Of LogonController)()
			If logonController IsNot Nothing AndAlso logonController.AcceptAction.Active = True Then
				DirectCast(Application.Security, ISupportMixedAuthentication).AuthenticationMixed.SetupAuthenticationProvider("OAuthProvider")
				logonController.AcceptAction.DoExecute()
			End If
		End Sub
		Private Sub AcceptAction_Changed(ByVal sender As Object, ByVal e As ActionChangedEventArgs)
			If e.ChangedPropertyType = ActionChangedType.Active Then
				SetActionsActive(DirectCast(sender, ActionBase).Active)
			End If
		End Sub
		Private Sub SetActionsActive(ByVal logonActionActive As Boolean)
			For Each action As ActionBase In Actions
				action.Active("LogonActionActive") = logonActionActive
			Next action
			RegisterVisibleUserExistingTextScript(logonActionActive)
		End Sub
		Private Sub RegisterVisibleUserExistingTextScript(ByVal visible As Boolean)
			CType(Frame, WebWindow).RegisterClientScript("LogonActionActive", String.Format("SetVisibleUserExistingText({0});", ClientSideEventsHelper.ToJSBoolean(visible)), True)
		End Sub
		Protected Overrides Sub OnViewControlsCreated()
			Dim logonController As LogonController = Frame.GetController(Of LogonController)()
			If logonController IsNot Nothing Then
				SetActionsActive(logonController.AcceptAction.Active)
			End If

			Dim providersName As IList(Of String) = TryCast(GetProviderNames(), IList(Of String))
			If providersName.Count = 0 Then
				RegisterVisibleUserExistingTextScript(False)
			End If
			microsoftAction.Active("ProviderIsSet") = providersName.Contains("Microsoft")

			If IsOAuthRequest AndAlso WebWindow.CurrentRequestPage IsNot Nothing Then
				AddHandler WebWindow.CurrentRequestPage.Load, AddressOf CurrentRequestPage_Load
			End If
			MyBase.OnViewControlsCreated()
		End Sub
		Public Shared ReadOnly Property IsOAuthRequest() As Boolean
			Get
				Return HttpContext.Current.Request.Params(OAuthParameter) IsNot Nothing
			End Get
		End Property
		Protected Overrides Sub OnDeactivated()
			Dim logonController As LogonController = Frame.GetController(Of LogonController)()
			If logonController IsNot Nothing Then
				RemoveHandler logonController.AcceptAction.Changed, AddressOf AcceptAction_Changed
			End If
			MyBase.OnDeactivated()
		End Sub
	End Class
End Namespace
