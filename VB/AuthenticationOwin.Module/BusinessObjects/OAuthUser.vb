Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

Namespace AuthenticationOwin.Module.BusinessObjects
    Public Class OAuthUser
        Inherits PermissionPolicyUser
        Implements IAuthenticationOAuthUser

        Public Property EnableStandardAuthentication() As Boolean
            Get
                Return GetPropertyValue(Of Boolean)("EnableStandardAuthentication")
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue("EnableStandardAuthentication", value)
            End Set
        End Property
        ReadOnly Property I_EnableStandardAuthentication() As Boolean Implements IAuthenticationOAuthUser.EnableStandardAuthentication
            Get
                Return EnableStandardAuthentication
            End Get
        End Property
        <Association, Aggregated>
        Public ReadOnly Property OAuthAuthenticationEmails() As XPCollection(Of EmailEntity) Implements IAuthenticationOAuthUser.OAuthAuthenticationEmails
            Get
                Return GetCollection(Of EmailEntity)("OAuthAuthenticationEmails")
            End Get
        End Property
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        'VB kludge
        Property I_UserName As String Implements IAuthenticationOAuthUser.UserName
            Get
                Return UserName
            End Get
            Set(value As String)
                UserName = value
            End Set
        End Property
    End Class
    Public Class EmailEntity
        Inherits BaseObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        <RuleUniqueValue("Unique_Email", DefaultContexts.Save, CriteriaEvaluationBehavior:=CriteriaEvaluationBehavior.BeforeTransaction)> _
        Public Property Email() As String
            Get
                Return GetPropertyValue(Of String)("Email")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Email", value)
            End Set
        End Property
        <Association> _
        Public Property OAuthUser() As OAuthUser
            Get
                Return GetPropertyValue(Of OAuthUser)("OAuthUser")
            End Get
            Set(ByVal value As OAuthUser)
                SetPropertyValue("OAuthUser", value)
            End Set
        End Property
    End Class
End Namespace
