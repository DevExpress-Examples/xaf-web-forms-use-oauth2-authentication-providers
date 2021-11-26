Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo

Namespace MySolution.Module.BusinessObjects
	<MapInheritance(MapInheritanceType.ParentTable), DefaultProperty(NameOf(ApplicationUser.UserName))>
	Public Class ApplicationUser
		Inherits PermissionPolicyUser
		Implements IObjectSpaceLink, ISecurityUserWithLoginInfo

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub

		<Browsable(False), Aggregated, Association("User-LoginInfo")>
		Public ReadOnly Property LoginInfo() As XPCollection(Of ApplicationUserLoginInfo)
			Get
				Return GetCollection(Of ApplicationUserLoginInfo)(NameOf(LoginInfo))
			End Get
		End Property

		Private ReadOnly Property IOAuthSecurityUser_UserLogins() As IEnumerable(Of ISecurityUserLoginInfo) Implements IOAuthSecurityUser.UserLogins
			Get
				Return LoginInfo.OfType(Of ISecurityUserLoginInfo)()
			End Get
		End Property

		Private Property IObjectSpaceLink_ObjectSpace() As IObjectSpace Implements IObjectSpaceLink.ObjectSpace

		Private Function ISecurityUserWithLoginInfo_CreateUserLoginInfo(ByVal loginProviderName As String, ByVal providerUserKey As String) As ISecurityUserLoginInfo Implements ISecurityUserWithLoginInfo.CreateUserLoginInfo
			Dim result As ApplicationUserLoginInfo = DirectCast(Me, IObjectSpaceLink).ObjectSpace.CreateObject(Of ApplicationUserLoginInfo)()
			result.LoginProviderName = loginProviderName
			result.ProviderUserKey = providerUserKey
			result.User = Me
			Return result
		End Function
		Public Property EnableStandardAuthentication() As Boolean
			Get
				Return GetPropertyValue(Of Boolean)(NameOf(EnableStandardAuthentication))
			End Get
			Set(ByVal value As Boolean)
				SetPropertyValue(NameOf(EnableStandardAuthentication), value)
			End Set
		End Property
		<Association, Aggregated>
		Public ReadOnly Property OAuthAuthenticationEmails() As XPCollection(Of EmailEntity)
			Get
				Return GetCollection(Of EmailEntity)(NameOf(OAuthAuthenticationEmails))
			End Get
		End Property
	End Class
	Public Class EmailEntity
		Inherits BaseObject

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		<RuleUniqueValue("Unique_Email", DefaultContexts.Save, CriteriaEvaluationBehavior := CriteriaEvaluationBehavior.BeforeTransaction)>
		Public Property Email() As String
			Get
				Return GetPropertyValue(Of String)(NameOf(Email))
			End Get
			Set(ByVal value As String)
				SetPropertyValue(NameOf(Email), value)
			End Set
		End Property
		<Association>
		Public Property ApplicationUser() As ApplicationUser
			Get
				Return GetPropertyValue(Of ApplicationUser)(NameOf(ApplicationUser))
			End Get
			Set(ByVal value As ApplicationUser)
				SetPropertyValue(NameOf(ApplicationUser), value)
			End Set
		End Property
	End Class
End Namespace
