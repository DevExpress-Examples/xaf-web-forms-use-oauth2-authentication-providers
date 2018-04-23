Imports AuthenticationOwin.Module.BusinessObjects
Imports DevExpress.Xpo
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace AuthenticationOwin.Module
    Public Interface IAuthenticationOAuthUser
        Property UserName() As String
        ReadOnly Property OAuthAuthenticationEmails() As XPCollection(Of EmailEntity)
        ReadOnly Property EnableStandardAuthentication() As Boolean
    End Interface
End Namespace
