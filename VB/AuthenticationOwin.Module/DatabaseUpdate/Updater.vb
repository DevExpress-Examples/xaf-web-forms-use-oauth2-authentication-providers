Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Security.Strategy
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports AuthenticationOwin.Module.BusinessObjects

Namespace AuthenticationOwin.Module.DatabaseUpdate
	' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
	Public Class Updater
		Inherits ModuleUpdater
		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
			Dim sampleUser As OAuthUser = ObjectSpace.FindObject(Of OAuthUser)(New BinaryOperator("UserName", "User"))
			If sampleUser Is Nothing Then
				sampleUser = ObjectSpace.CreateObject(Of OAuthUser)()
				sampleUser.UserName = "User"
				sampleUser.EnableStandardAuthentication = True
				sampleUser.SetPassword("")
			End If
			Dim defaultRole As PermissionPolicyRole = CreateDefaultRole()
			sampleUser.Roles.Add(defaultRole)

			Dim userAdmin As OAuthUser = ObjectSpace.FindObject(Of OAuthUser)(New BinaryOperator("UserName", "Admin"))
			If userAdmin Is Nothing Then
				userAdmin = ObjectSpace.CreateObject(Of OAuthUser)()
				userAdmin.UserName = "Admin"
				userAdmin.EnableStandardAuthentication = True
				' Set a password if the standard Authentication type is used
				userAdmin.SetPassword("")
			End If
			' If a role with the Administrators name doesn't exist in the database, create this role
			Dim adminRole As PermissionPolicyRole = ObjectSpace.FindObject(Of PermissionPolicyRole)(New BinaryOperator("Name", "Administrators"))
			If adminRole Is Nothing Then
				adminRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
				adminRole.Name = "Administrators"
			End If
			adminRole.IsAdministrative = True
			userAdmin.Roles.Add(adminRole)
			ObjectSpace.CommitChanges() 'This line persists created object(s).
		End Sub
		Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
			MyBase.UpdateDatabaseBeforeUpdateSchema()
			'if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
			'    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
			'}
		End Sub
		Private Function CreateDefaultRole() As PermissionPolicyRole
			Dim defaultRole As PermissionPolicyRole = ObjectSpace.FindObject(Of PermissionPolicyRole)(New BinaryOperator("Name", "Default"))
			If defaultRole Is Nothing Then
				defaultRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
				defaultRole.Name = "Default"

				defaultRole.AddObjectPermission(Of PermissionPolicyUser)(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow)
				defaultRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow)
				defaultRole.AddMemberPermission(Of PermissionPolicyUser)(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow)
				defaultRole.AddMemberPermission(Of PermissionPolicyUser)(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow)
				defaultRole.AddTypePermissionsRecursively(Of PermissionPolicyRole)(SecurityOperations.Read, SecurityPermissionState.Deny)
				defaultRole.AddTypePermissionsRecursively(Of ModelDifference)(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow)
				defaultRole.AddTypePermissionsRecursively(Of ModelDifferenceAspect)(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow)
			End If
			Return defaultRole
		End Function
	End Class
End Namespace
