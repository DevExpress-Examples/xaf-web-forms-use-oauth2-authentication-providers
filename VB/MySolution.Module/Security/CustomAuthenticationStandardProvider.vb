Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports MySolution.Module.BusinessObjects
Imports System

Namespace MySolution.Module.Security

	' ...
	Public Class CustomAuthenticationStandardProvider
		Inherits AuthenticationStandardProvider

		Public Sub New(ByVal userType As Type)
			MyBase.New(userType)
		End Sub
		Public Overrides Function Authenticate(ByVal objectSpace As IObjectSpace) As Object
			Dim user As ApplicationUser = TryCast(MyBase.Authenticate(objectSpace), ApplicationUser)
			If user IsNot Nothing AndAlso Not user.EnableStandardAuthentication Then
				Throw New InvalidOperationException("Password authentication is not allowed for this user.")
			End If
			Return user
		End Function
	End Class
End Namespace
