Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Xpo
Imports AuthenticationOwin.Web.Security
Imports AuthenticationOwin.Module.Web.Controllers
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports AuthenticationOwin.Module.BusinessObjects
Imports AuthenticationOwin.Module.Security

Namespace AuthenticationOwin.Web
	' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWebWebApplicationMembersTopicAll.aspx
	Partial Public Class AuthenticationOwinAspNetApplication
		Inherits WebApplication

		Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
		Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
		Private module3 As AuthenticationOwin.Module.AuthenticationOwinModule
		Private module4 As AuthenticationOwin.Module.Web.AuthenticationOwinAspNetModule
		Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
		Private securityStrategyComplex1 As AuthenticationOwin.Module.Web.Security.CustomSecurityStrategyComplex
		Private validationModule As DevExpress.ExpressApp.Validation.ValidationModule
		Private validationAspNetModule As DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule

		#Region "Default XAF configuration options (https:" 'www.devexpress.com/kb=T501418)
		Shared Sub New()
			EnableMultipleBrowserTabsSupport = True
			DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.AllowFilterControlHierarchy = True
			DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.MaxFilterControlHierarchyDepth = 3
			DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.AllowFilterControlHierarchyDefault = True
			DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.MaxHierarchyDepthDefault = 3
			DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = True
			DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = False
		End Sub
		Private Sub InitializeDefaults()
			LinkNewObjectToParentImmediately = False
			OptimizedControllersCreation = True
		End Sub
		#End Region
		Public Sub New()
			InitializeComponent()
			InitializeDefaults()
			Dim authenticationMixed As New AuthenticationMixed()
			authenticationMixed.LogonParametersType = GetType(AuthenticationStandardLogonParameters)
			authenticationMixed.AuthenticationProviders.Add(GetType(CustomAuthenticationStandardProvider).Name, New CustomAuthenticationStandardProvider(GetType(OAuthUser)))
			Dim authProvider As New OAuthProvider(GetType(OAuthUser), securityStrategyComplex1)
			authProvider.CreateUserAutomatically = True
			authenticationMixed.AuthenticationProviders.Add(GetType(OAuthProvider).Name, authProvider)
			securityStrategyComplex1.Authentication = authenticationMixed
		End Sub
		Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
			args.ObjectSpaceProvider = New XPObjectSpaceProvider(GetDataStoreProvider(args.ConnectionString, args.Connection), True)
			args.ObjectSpaceProviders.Add(New NonPersistentObjectSpaceProvider(TypesInfo, Nothing))
		End Sub
		Private Function GetDataStoreProvider(ByVal connectionString As String, ByVal connection As System.Data.IDbConnection) As IXpoDataStoreProvider
			Dim application As System.Web.HttpApplicationState = If((System.Web.HttpContext.Current IsNot Nothing), System.Web.HttpContext.Current.Application, Nothing)
			Dim dataStoreProvider As IXpoDataStoreProvider = Nothing
			If application IsNot Nothing AndAlso application("DataStoreProvider") IsNot Nothing Then
				dataStoreProvider = TryCast(application("DataStoreProvider"), IXpoDataStoreProvider)
			Else
				dataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, True)
				If application IsNot Nothing Then
					application("DataStoreProvider") = dataStoreProvider
				End If
			End If
			Return dataStoreProvider
		End Function
		Private Sub AuthenticationOwinAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
#If EASYTEST Then
			e.Updater.Update()
			e.Handled = True
#Else
			If System.Diagnostics.Debugger.IsAttached Then
				e.Updater.Update()
				e.Handled = True
			Else
				Dim message As String = "The application cannot connect to the specified database, " & "because the database doesn't exist, its version is older " & "than that of the application or its schema does not match " & "the ORM data model structure. To avoid this error, use one " & "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article."

				If e.CompatibilityError IsNot Nothing AndAlso e.CompatibilityError.Exception IsNot Nothing Then
					message &= Constants.vbCrLf & Constants.vbCrLf & "Inner exception: " & e.CompatibilityError.Exception.Message
				End If
				Throw New InvalidOperationException(message)
			End If
#End If
		End Sub
		Private Sub InitializeComponent()
			Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
			Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
			Me.module3 = New AuthenticationOwin.Module.AuthenticationOwinModule()
			Me.module4 = New AuthenticationOwin.Module.Web.AuthenticationOwinAspNetModule()
			Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
            Me.securityStrategyComplex1 = New AuthenticationOwin.Module.Web.Security.CustomSecurityStrategyComplex()
            Me.securityStrategyComplex1.SupportNavigationPermissionsForTypes = False
			Me.securityStrategyComplex1.NewUserRoleName = "Default"

			Me.validationModule = New DevExpress.ExpressApp.Validation.ValidationModule()
			Me.validationAspNetModule = New DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule()
			CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' securityStrategyComplex1
			' 
			Me.securityStrategyComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole)
            Me.securityStrategyComplex1.UserType = GetType(AuthenticationOwin.Module.BusinessObjects.OAuthUser)
            ' 
            ' securityModule1
            ' 
            Me.securityModule1.UserType = GetType(AuthenticationOwin.Module.BusinessObjects.OAuthUser)

            ' 
            ' AuthenticationOwinAspNetApplication
            ' 
            Me.ApplicationName = "AuthenticationOwin"
			Me.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema
			Me.Modules.Add(Me.module1)
			Me.Modules.Add(Me.module2)
			Me.Modules.Add(Me.module3)
			Me.Modules.Add(Me.module4)
			Me.Modules.Add(Me.securityModule1)
			Me.Security = Me.securityStrategyComplex1
			Me.Modules.Add(Me.validationModule)
			Me.Modules.Add(Me.validationAspNetModule)
'			Me.DatabaseVersionMismatch += New System.EventHandler(Of DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)(Me.AuthenticationOwinAspNetApplication_DatabaseVersionMismatch);
			CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub
	End Class
End Namespace
