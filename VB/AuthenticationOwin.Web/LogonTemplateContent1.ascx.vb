Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Templates
Imports DevExpress.ExpressApp.Web.Controls
Imports DevExpress.ExpressApp.Web.Templates
Namespace AuthenticationOwin.Web
	Partial Public Class LogonTemplateContent1
		Inherits TemplateContent
		Implements IXafPopupWindowControlContainer, IHeaderImageControlContainer
		Public Overrides Sub SetStatus(ByVal statusMessages As ICollection(Of String))
		End Sub
		Public Overrides ReadOnly Property DefaultContainer() As IActionContainer
			Get
				Return Nothing
			End Get
		End Property
		Public Overrides ReadOnly Property ViewSiteControl() As Object
			Get
				Return viewSiteControl_Renamed
			End Get
		End Property
		Public ReadOnly Property XafPopupWindowControl() As XafPopupWindowControl Implements IXafPopupWindowControlContainer.XafPopupWindowControl
			Get
				Return PopupWindowControl
			End Get
		End Property
		Public Overrides Sub BeginUpdate()
			MyBase.BeginUpdate()
			Me.PopupActions.BeginUpdate()
		End Sub
		Public Overrides Sub EndUpdate()
			Me.PopupActions.EndUpdate()
			MyBase.EndUpdate()
		End Sub
		Public ReadOnly Property HeaderImageControl() As ThemedImageControl Implements IHeaderImageControlContainer.HeaderImageControl
			Get
				Return TIC
			End Get
		End Property
		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			MyBase.OnLoad(e)
			OAuthActions.Menu.ItemWrap = True
		End Sub
	End Class
End Namespace
