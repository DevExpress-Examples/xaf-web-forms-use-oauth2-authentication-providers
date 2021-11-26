Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Web
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.ClientServer
Imports MySolution.Module.Security
Imports MySolution.Module.BusinessObjects
Imports MySolution.Web.Security
Imports MySolution.Module.Web.Security

Namespace MySolution.Web
	' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Web.WebApplication
	Partial Public Class MySolutionAspNetApplication
		Inherits WebApplication

		Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
		Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
		Private module3 As MySolution.Module.MySolutionModule
		Private module4 As MySolution.Module.Web.MySolutionAspNetModule
		Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
		Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
		Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
		Private conditionalAppearanceModule As DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule
		Private validationModule As DevExpress.ExpressApp.Validation.ValidationModule
		Private validationAspNetModule As DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule

		Public Sub New()
			InitializeComponent()
			Dim authenticationMixed As New AuthenticationMixed()
			authenticationMixed.LogonParametersType = GetType(AuthenticationStandardLogonParameters)
			authenticationMixed.AuthenticationProviders.Add(GetType(CustomAuthenticationStandardProvider).Name, New CustomAuthenticationStandardProvider(GetType(ApplicationUser)))
			Dim authProvider As New OAuthProvider(GetType(ApplicationUser), securityStrategyComplex1)
			authProvider.CreateUserAutomatically = True
			authenticationMixed.AuthenticationProviders.Add(GetType(OAuthProvider).Name, authProvider)
			securityStrategyComplex1.Authentication = authenticationMixed
		End Sub
		Protected Overrides Function CreateViewUrlManager() As IViewUrlManager
			Return New ViewUrlManager()
		End Function
		Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
			args.ObjectSpaceProvider = New SecuredObjectSpaceProvider(DirectCast(Security, SecurityStrategyComplex), GetDataStoreProvider(args.ConnectionString, args.Connection), True)
			args.ObjectSpaceProviders.Add(New NonPersistentObjectSpaceProvider(TypesInfo, Nothing))
		End Sub
		Private Function GetDataStoreProvider(ByVal connectionString As String, ByVal connection As System.Data.IDbConnection) As IXpoDataStoreProvider
			Dim application As System.Web.HttpApplicationState = If(System.Web.HttpContext.Current IsNot Nothing, System.Web.HttpContext.Current.Application, Nothing)
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
		Private Sub MySolutionAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles Me.DatabaseVersionMismatch
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
					message &= vbCrLf & vbCrLf & "Inner exception: " & e.CompatibilityError.Exception.Message
				End If
				Throw New InvalidOperationException(message)
			End If
#End If
		End Sub
		Private Sub InitializeComponent()
			Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
			Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
			Me.module3 = New MySolution.Module.MySolutionModule()
			Me.module4 = New MySolution.Module.Web.MySolutionAspNetModule()
			Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
			Me.securityStrategyComplex1 = New CustomSecurityStrategyComplex()
			Me.securityStrategyComplex1.SupportNavigationPermissionsForTypes = False
			Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
			Me.conditionalAppearanceModule = New DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule()
			Me.validationModule = New DevExpress.ExpressApp.Validation.ValidationModule()
			Me.validationAspNetModule = New DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule()
			DirectCast(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' securityStrategyComplex1
			' 
			'this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
			Me.securityStrategyComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole)
			Me.securityStrategyComplex1.NewUserRoleName = "Default"
			' ApplicationUser descends from PermissionPolicyUser and supports OAuth authentication. For more information, refer to the following help topic: https://docs.devexpress.com/eXpressAppFramework/402197
			' If your application uses PermissionPolicyUser or a custom user type, set the UserType property as follows:
			Me.securityStrategyComplex1.UserType = GetType(MySolution.Module.BusinessObjects.ApplicationUser)
			' 
			' securityModule1
			' 
			Me.securityModule1.UserType = GetType(MySolution.Module.BusinessObjects.ApplicationUser)
			' 
			' authenticationStandard1
			' 
			Me.authenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
			' ApplicationUserLoginInfo is only necessary for applications that use the ApplicationUser user type.
			' Comment out the following line if using PermissionPolicyUser or a custom user type.
			Me.authenticationStandard1.UserLoginInfoType = GetType(MySolution.Module.BusinessObjects.ApplicationUserLoginInfo)
			'
			' validationModule
			'
			Me.validationModule.AllowValidationDetailsAccess = False
			' 
			' MySolutionAspNetApplication
			' 
			Me.ApplicationName = "MySolution"
			Me.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema
			Me.Modules.Add(Me.module1)
			Me.Modules.Add(Me.module2)
			Me.Modules.Add(Me.module3)
			Me.Modules.Add(Me.module4)
			Me.Modules.Add(Me.securityModule1)
			Me.Security = Me.securityStrategyComplex1
			Me.Modules.Add(Me.conditionalAppearanceModule)
			Me.Modules.Add(Me.validationModule)
			Me.Modules.Add(Me.validationAspNetModule)
			'Me.DatabaseVersionMismatch += New System.EventHandler(Of DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)(Me.AuthenticationOwinAspNetApplication_DatabaseVersionMismatch);
			DirectCast(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub
	End Class
End Namespace
