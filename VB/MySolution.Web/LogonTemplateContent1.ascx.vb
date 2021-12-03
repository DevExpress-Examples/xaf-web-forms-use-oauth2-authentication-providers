﻿Imports DevExpress.ExpressApp.Templates
Imports DevExpress.ExpressApp.Web.Controls
Imports DevExpress.ExpressApp.Web.Templates
Imports System
Imports System.Collections.Generic

Namespace MySolution.Web
	Partial Public Class LogonTemplateContent1
		Inherits TemplateContent
		Implements IXafPopupWindowControlContainer, IHeaderImageControlContainer

		Protected Overrides Sub OnInit(ByVal e As EventArgs)
			MyBase.OnInit(e)
			LogoLink.NavigateUrl = Request.ApplicationPath
		End Sub
		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			MyBase.OnLoad(e)
		End Sub
		Public Overrides Sub SetStatus(ByVal statusMessages As ICollection(Of String))
		End Sub
		Public Overrides ReadOnly Property DefaultContainer() As IActionContainer
			Get
				Return Nothing
			End Get
		End Property
		Public Overrides ReadOnly Property ViewSiteControl() As Object
			Get
				Return _viewSiteControl
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
	End Class
End Namespace
