Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security

Namespace AuthenticationOwin.Module.Web.Security
    Public Class CustomSecurityStrategyComplex
        Inherits SecurityStrategyComplex

        Protected Overrides Sub InitializeNewUserCore(ByVal objectSpace As IObjectSpace, ByVal user As Object)
            MyBase.InitializeNewUserCore(objectSpace, user)
        End Sub
        Public Sub InitializeNewUser(ByVal objectSpace As IObjectSpace, ByVal user As Object)
            InitializeNewUserCore(objectSpace, user)
        End Sub
    End Class
End Namespace
