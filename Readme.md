<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128594415/20.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T535280)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

**ASP.NET WebForms**
* [LogonAuthController.cs](./CS/AuthenticationOwin.Module.Web/Controllers/LogonAuthController.cs) (VB: [LogonAuthController.vb](./VB/AuthenticationOwin.Module.Web/Controllers/LogonAuthController.vb))
* [CustomSecurityStrategyComplex.cs](./CS/AuthenticationOwin.Module.Web/Security/CustomSecurityStrategyComplex.cs) (VB: [CustomSecurityStrategyComplex.vb](./VB/AuthenticationOwin.Module.Web/Security/CustomSecurityStrategyComplex.vb))
* [OAuthUser.cs](./CS/AuthenticationOwin.Module/BusinessObjects/OAuthUser.cs) (VB: [OAuthUser.vb](./VB/AuthenticationOwin.Module/BusinessObjects/OAuthUser.vb))
* [IAuthenticationOAuthUser.cs](./CS/AuthenticationOwin.Module/IAuthenticationOAuthUser.cs) (VB: [IAuthenticationOAuthUser.vb](./VB/AuthenticationOwin.Module/IAuthenticationOAuthUser.vb))
* [CustomAuthenticationStandardProvider.cs](./CS/AuthenticationOwin.Module/Security/CustomAuthenticationStandardProvider.cs) (VB: [CustomAuthenticationStandardProvider.vb](./VB/AuthenticationOwin.Module/Security/CustomAuthenticationStandardProvider.vb))
* [OAuthProvider.cs](./CS/AuthenticationOwin.Web/Security/OAuthProvider.cs) (VB: [OAuthProvider.vb](./VB/AuthenticationOwin.Web/Security/OAuthProvider.vb))
* [Startup.cs](./CS/AuthenticationOwin.Web/Startup.cs) (VB: [Startup.vb](./VB/AuthenticationOwin.Web/Startup.vb))
* [WebApplication.cs](./CS/AuthenticationOwin.Web/WebApplication.cs) (VB: [WebApplication.vb](./VB/AuthenticationOwin.Web/WebApplication.vb))

**Blazor Server**

[Use Active Directory and OAuth2 Authentication Providers in Blazor Applications](https://docs.devexpress.com/eXpressAppFramework/402197/task-based-help/security/how-to-use-active-directory-and-oauth2-authentication-providers-in-blazor-applications)

**WinForms**

[OAuth2 Authentication for WinForms Applications](https://www.devexpress.com/Support/Center/p/T567978)

<!-- default file list end -->
# How to: Use Google, Facebook and Microsoft accounts in ASP.NET Web Forms XAF applications (OAuth2 demo)


This example demonstrates the use of OAuth2 authentication in a web application. Users can sign in to the application via Google, Facebook or  Microsoft authentication providers.<br><br><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-use-google-facebook-and-microsoft-accounts-in-aspnet-xaf-applications-oauth2-demo-t535280/17.1.3+/media/f34385f9-6797-11e7-80c0-00155d624807.png"><br>You can try this demo "as is" to overview its capabilities, and then try the demonstrated functionality in your own XAF applications according to the instructions below.<br><br><br><strong>How to Run this Demo</strong><br><br>Before running this demo, register developer accounts at the services you are going to use

* <a href="https://console.developers.google.com/">https://console.developers.google.com/</a> (Make sure that 'Google+ API' is enabled. Read more here: <a href="https://docs.microsoft.com/en-us/aspnet/mvc/overview/security/create-an-aspnet-mvc-5-app-with-facebook-and-google-oauth2-and-openid-sign-on">Creating a Google app for OAuth 2 and connecting the app to the project</a>)
* <a href="https://developers.facebook.com/">https://developers.facebook.com/</a>
* <a href="https://docs.microsoft.com/en-us/azure/active-directory/fundamentals/auth-oauth2">OAuth 2.0 authentication with Azure Active Directory</a> <br><br>Open the <em>Web.config</em> file and specify your own client IDs and client secrets for each provider.<br><br>


```xml
<appSettings>
    <add key="GoogleClientID" value="YourGoogleClientID" />
    <add key="GoogleClientSecret" value="YourGoogleClientSecret" />
    <add key="FacebookClientID" value="YourFacebookClientID" />
    <add key="FacebookClientSecret" value="YourFacebookClientSecret" />
    <add key="MicrosoftClientID" value="YourMicrosoftClientID" />
    <add key="MicrosoftClientSecret" value="YourMicrosoftClientSecret" />
```


<br>You can remove keys corresponding to providers that you do not want to use. <br>
> Note that you may need to update nuget packages to work correctly.

<br>Now you can run the application.<br>

<br><strong><br>Overview of this Demo Capabilities</strong><br><br>In the logon window, there are buttons for each provider specified in <em>Web.config:<br><br><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-use-google-facebook-and-microsoft-accounts-in-aspnet-xaf-applications-oauth2-demo-t535280/17.1.3+/media/64415faf-679a-11e7-80c0-00155d624807.png"><br></em>Standard XAF authentication with built-in username/password is also supported. When you log in via OAuth authentication, the email is used as a user name. By default, a user object is autocreated for each logon. You can disable autocreation, or specify the auto-assigned role for new users in the <strong>InitializeComponent</strong> method (see [AuthenticationOwin.Web/WebApplication.cs](./CS/AuthenticationOwin.Web/WebApplication.cs)([vb](./VB/AuthenticationOwin.Web/WebApplication.vb))):<br>

C#
```cs
OAuthProvider authProvider = new OAuthProvider(typeof(OAuthUser), securityStrategyComplex1);
authProvider.CreateUserAutomatically = true;
```

VB.NET
```vb
Dim authProvider As New OAuthProvider(GetType(OAuthUser), securityStrategyComplex1)
authProvider.CreateUserAutomatically = True
```


When <strong>CreateUserAutomatically</strong> is false, the logon is allowed if a user with the email returned by the external service exists in the application database. To grant access to a user with a specific e-mail, use the built-in Admin account, create a user object and set the <strong>UserName</strong> to this e-mail.<br><br><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-use-google-facebook-and-microsoft-accounts-in-aspnet-xaf-applications-oauth2-demo-t535280/17.1.3+/media/6f2e4798-679f-11e7-80c0-00155d624807.png"><br><br>If you set the <strong>EnableStandardAuthentication</strong> property to true for an auto-created user, this user will be able to login directly, with a user name and password. Note that the password is empty by default, so do not forget to specify it when enabling standard authentication.<br><br><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-use-google-facebook-and-microsoft-accounts-in-aspnet-xaf-applications-oauth2-demo-t535280/17.1.3+/media/f2aeacb6-679e-11e7-80c0-00155d624807.png"><br><br>Each user can have several associated email addresses. To add or remove email addresses, use the  <strong>OAuth Authorization Emails</strong> list in the user's Detail View.<br><br><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-use-google-facebook-and-microsoft-accounts-in-aspnet-xaf-applications-oauth2-demo-t535280/17.1.3+/media/ec102541-679f-11e7-80c0-00155d624807.png"><br><strong><br>How to Implement the Demonstrated Functionality in your XAF Application</strong> <br><br>1. In your solution, open <a href="https://docs.microsoft.com/en-us/nuget/tools/package-manager-console">Package Manager Console</a>.<br> 
1.1. Choose the *YourSolutionName.Web* project in the **Default project** combo box, and execute the following commands to add Owin packages:  
*Install-Package Microsoft.Owin -Version 4.1.0*   
*Install-Package Microsoft.Owin.Cors -Version 4.1.0*  
*Install-Package Microsoft.Owin.Security -Version 4.1.0*  
*Install-Package Microsoft.Owin.Security.Cookies -Version 4.1.0*  
*Install-Package Microsoft.Owin.Host.SystemWeb -Version 4.1.0*  
*Install-Package Microsoft.Owin.Security.Google -Version 4.1.0*  
*Install-Package Microsoft.Owin.Security.Facebook -Version 4.1.0*  
*Install-Package Microsoft.Owin.Security.MicrosoftAccount -Version 4.1.0*  

1.2. Switch to the *YourSolutionName.Module.Web* project and install these packages:  
*Install-Package Microsoft.AspNet.Cors -Version 5.2.7*  
*Install-Package Microsoft.Owin -Version 4.1.0*  
*Install-Package Microsoft.Owin.Host.SystemWeb -Version 4.1.0*  
*Install-Package Microsoft.Owin.Security -Version 4.1.0*  



2\. Open the *YourSolutionName.Module.Web/Web.config* file and specify your own client IDs and client secrets for each provider you are going to use. Refer to the [*AuthenticationOwin.Web\Web.config*](./CS/AuthenticationOwin.Web/Web.config) file in the demo solution to see the example. Then, set the authentication mode to "None" and comment or remove settings related to the default XAF authentication:<br>
  


```xml
<authentication mode="None" /> 
  <!--<forms name="Login" loginUrl="Login.aspx" path="/" timeout="10" />--> 
</authentication> 
    <!--<authorization> 
      <deny users="?" /> 
      <allow users="*" /> 
    </authorization>-->
```


<br>3. Copy the following files from the demo solution to the corresponding locations within your solution:  
*AuthenticationOwin.Module\IAuthenticationOAuthUser.cs(vb)*  
*AuthenticationOwin.Module\BusinessObjects\OAuthUser.cs(vb)*  
*AuthenticationOwin.Module.Web\Controllers\LogonAuthController.cs(vb)*  
*AuthenticationOwin.Module.Web\Security\CustomSecurityStrategyComplex.cs(vb)*  
*AuthenticationOwin.Module.Web\Images\Facebook.svg*  
*AuthenticationOwin.Module.Web\Images\Google.svg*  
*AuthenticationOwin.Module.Web\Images\Microsoft.png*  
*AuthenticationOwin.Web\Startup.cs(vb)*  
*AuthenticationOwin.Web\LogonTemplateContent1.ascx*  
*AuthenticationOwin.Web\LogonTemplateContent1.ascx.cs(vb)*  
*AuthenticationOwin.Web\LogonTemplateContent1.ascx.designer.cs(vb)*  
*AuthenticationOwin.Web\Login.aspx*  
*AuthenticationOwin.Web\Login.aspx.designer.cs*  
*AuthenticationOwin.Module/Security/CustomAuthenticationStandardProvider.cs(vb)*  
*AuthenticationOwin.Web\Security\OAuthProvider.cs(vb)*  


Include the copied files to your solution (**Add|Existing Item...**). Update the namespace names in the copied code files to match namespaces you use in your solution. For image files, set the <a href="https://msdn.microsoft.com/en-us/library/0c6xyb66(v=vs.100).aspx#Anchor_1">Build Action</a> property to <strong>Embedded Resource</strong>.<br><em><br></em>  
4. Edit the <em>YourSolutionName.Module.Web\WebModule.cs(vb)</em> file. In the overridden <strong>Setup</strong> method, handle the<a href="https://documentation.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.XafApplication.CreateCustomLogonWindowControllers.event"> XafApplication.CreateCustomLogonWindowControllers</a> event and add the <strong>LogonAuthController</strong> to the <a href="https://documentation.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CreateCustomLogonWindowControllersEventArgs.Controllers.property">e.Controllers</a> collection passed to this event. Refer to the <em>AuthenticationOwin.Module.Web\Module.cs(vb)</em> file to see an example.  

5\. Edit the *YourSolutionName.Web\WebApplication.cs(vb)* code:  
  
Register CustomSecurityStrategyComplex:  

C#
```cs
this.securityStrategyComplex1 = new AuthenticationOwin.Module.Web.Security.CustomSecurityStrategyComplex();
```
VB.NET
```vb
Me.securityStrategyComplex1 = New AuthenticationOwin.Module.Web.Security.CustomSecurityStrategyComplex()
```
Use AuthenticationMixed instead of your authentication:  

C#
```cs
public YourApplicationNameAspNetApplication() {
  InitializeComponent();
  //...
  AuthenticationMixed authenticationMixed = new AuthenticationMixed();
  authenticationMixed.LogonParametersType = typeof(AuthenticationStandardLogonParameters);
  authenticationMixed.AuthenticationProviders.Add(typeof(CustomAuthenticationStandardProvider).Name, new CustomAuthenticationStandardProvider(typeof(OAuthUser)));
  OAuthProvider authProvider = new OAuthProvider(typeof(OAuthUser), securityStrategyComplex1);
  authProvider.CreateUserAutomatically = true;
  authenticationMixed.AuthenticationProviders.Add(typeof(OAuthProvider).Name, authProvider);
  securityStrategyComplex1.Authentication = authenticationMixed;

```
VB.NET
```vb
Public Sub New()
  InitializeComponent()
  '...
  Dim authenticationMixed As New AuthenticationMixed()
  authenticationMixed.LogonParametersType = GetType(AuthenticationStandardLogonParameters)
  authenticationMixed.AuthenticationProviders.Add(GetType(CustomAuthenticationStandardProvider).Name, New CustomAuthenticationStandardProvider(GetType(OAuthUser)))
  Dim authProvider As New OAuthProvider(GetType(OAuthUser), securityStrategyComplex1)
  authProvider.CreateUserAutomatically = True
  authenticationMixed.AuthenticationProviders.Add(GetType(OAuthProvider).Name, authProvider)
  securityStrategyComplex1.Authentication = authenticationMixed
```


<br>6. Implement the <strong>IAuthenticationOAuthUser </strong>interface in your custom user class. You can see an example in the <em>AuthenticationOwin.Module\BusinessObjects\OAuthUser.cs </em>file. If you use the built-in user, you can use the <strong>OAuthUser </strong>class and set the <a href="https://documentation.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Security.SecurityStrategy.UserType.property">SecurityStrategy.UserType</a> property to <strong>OAuthUser</strong> in the <a href="https://documentation.devexpress.com/eXpressAppFramework/112827/Design-Time-Features/Application-Designer">Application Designer</a>.<br><br>7. Change the code that creates your predefined users in <em>YourSolutionName.Module\DatabaseUpdate\Updater.cs</em>. Set <strong>EnableStandardAuthentication</strong> to <strong>true</strong> for users who can login with standard authentication (username and password). See the example in the <em>AuthenticationOwin.Module\DatabaseUpdate\Updater.cs</em> file.<strong><br><br></strong>8. Register the <em>LogonTemplateContent1.ascx</em> template in the *Session_Start* method in the <em>YourSolutionName.Web\Global.asax.cs(vb)</em> file:<br>

C#
```cs
WebApplication.Instance.Settings.LogonTemplateContentPath = "LogonTemplateContent1.ascx"; 
```

VB.NET
```vb
WebApplication.Instance.Settings.LogonTemplateContentPath = "LogonTemplateContent1.ascx"
```


<br>9. Copy the<strong> LoginWith*</strong> actions customizations and the **AuthenticationStandardLogonParameters_DetailView** layout settings from the *[AuthenticationOwin.Web\Model.xafml](./CS/AuthenticationOwin.Web/Model.xafml)* file to the same file in the <em>YourSolutionName.Web</em> project. If you have no model customizations in<em> Model.xafml</em>, you can just overwrite it with the file from demo. Ensure that the <a href="https://documentation.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Web.SystemModule.IModelActionWeb.IsPostBackRequired.property">IsPostBackRequired</a> property of each <strong>LoginWith*</strong> action is set to true.<br>
<br>10. Configure OAuth2 provider services according to their documentation.
<br>This example shows how XAF can get a user's email from OAuth2 services and create (or authenticate) a user based on this data ([the OAuthProvider.Authenticate method](./CS/AuthenticationOwin.Web/Security/OAuthProvider.cs)). 
<br>Note that a third-party API and settings of OAuth2 services (Google, Facebook, and Microsoft) that we use in this example often change and we cannot control this at the level of our components. While we try to keep this example up-to-date with these changes, it is always better to refer to the official OAuth2 provider documentation. Please leave comments or create merge requests to this example if you find any inconsistencies. Refer to the <a href="https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/owin-oauth-20-authorization-server">OWIN OAuth 2.0 Authorization Server</a> documentation to learn how to add more authentication providers.
<br>Known OAuth2 services specificities:
- Microsoft [requires](https://www.devexpress.com/Support/Center/Question/Details/T686058/oauth2-example-with-microsoftaccountauthenticationoptions-not-working) the '/signin-microsoft' string to the Redirect URI (validated on March 13th 2020);
![chrome_2020-03-13_11-58-18w](https://user-images.githubusercontent.com/14300209/76611233-3aa34880-652b-11ea-958b-14bdb83ff071.png)
- "The Microsoft.Owin.Security.MicrosoftAccount assembly supports authenticating to both: Microsoft user accounts and Azure AD (School/Orgnizational) user accounts. To successfully authenticate an Azure AD user account in this demo project, ensure that you configure the Azure AD registered application as 'multi-tenanted = yes'. (The manifest entry: "availableToOtherTenants": true)" - added by [nrpieper](https://github.com/nrpieper):
- Google requires to enable the Google+ API.

**Disclaimer**

This example is based on the Microsoft Owin API and targets advanced developers who are experts in the Microsfot OWIN. It was developed for Microsoft.Owin v4.0.1 and was not tested in more recent versions. To support recent versions of Microsoft.Owin, it may be required to contact the Microsoft team or find possible solutions on public threads such as StackOverflow and etc. Researching such an API to update the example goes beyond the scope of DevExpress support services.
