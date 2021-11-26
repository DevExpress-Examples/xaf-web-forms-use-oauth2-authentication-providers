Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo

Namespace MySolution.Module.BusinessObjects
	<DeferredDeletion(False), Persistent("PermissionPolicyUserLoginInfo")>
	Public Class ApplicationUserLoginInfo
		Inherits BaseObject
		Implements ISecurityUserLoginInfo

		Private _loginProviderName As String
		Private _user As ApplicationUser
		Private _providerUserKey As String
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub

		<Indexed("ProviderUserKey", Unique:=True), Appearance("PasswordProvider", Enabled:=False, Criteria:="!(IsNewObject(this)) and LoginProviderName == '" & SecurityDefaults.PasswordAuthentication & "'", Context:="DetailView")>
		Public Property LoginProviderName() As String Implements ISecurityUserLoginInfo.LoginProviderName
			Get
				Return _loginProviderName
			End Get
			Set(ByVal value As String)
				SetPropertyValue(NameOf(LoginProviderName), _loginProviderName, value)
			End Set
		End Property

		<Appearance("PasswordProviderUserKey", Enabled:=False, Criteria:="!(IsNewObject(this)) and LoginProviderName == '" & SecurityDefaults.PasswordAuthentication & "'", Context:="DetailView")>
		Public Property ProviderUserKey() As String Implements ISecurityUserLoginInfo.ProviderUserKey
			Get
				Return _providerUserKey
			End Get
			Set(ByVal value As String)
				SetPropertyValue(NameOf(ProviderUserKey), _providerUserKey, value)
			End Set
		End Property

		<Association("User-LoginInfo")>
		Public Property User() As ApplicationUser
			Get
				Return _user
			End Get
			Set(ByVal value As ApplicationUser)
				SetPropertyValue(NameOf(User), _user, value)
			End Set
		End Property

		Private ReadOnly Property ISecurityUserLoginInfo_User() As Object Implements ISecurityUserLoginInfo.User
			Get
				Return User
			End Get
		End Property
	End Class
End Namespace
