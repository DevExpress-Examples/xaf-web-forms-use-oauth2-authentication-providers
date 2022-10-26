<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128594415/21.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T535280)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# XAF ASP.NET Web Forms - Use OAuth2 Authentication Providers

This example demonstrates how to extend your ASP.NET Web Forms application with OAuth authentication providers. Users can sign in to the application with their [Microsoft](https://learn.microsoft.com/en-us/azure/active-directory/develop/tutorial-v2-asp-webapp) accounts. Refer to the following article to learn how to add more authentication providers: [OWIN OAuth 2.0 Authorization Server](https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/owin-oauth-20-authorization-server).

See the following help topic for more information: [OAuth2 Authentication Providers in ASP.NET Web Forms Applications](https://docs.devexpress.com/eXpressAppFramework/403582/data-security-and-safety/security-system/authentication/oauth2-authentication-providers-in-web-forms-applications).

![The extended login form](./media/LogonPageWithOAuthProviders.png)

<!-- default file list -->
## Files to Look at

* [LogonAuthController.cs](./CS/MySolution.Module.Web/Controllers/LogonAuthController.cs)
* [CustomSecurityStrategyComplex.cs](./CS/MySolution.Module.Web/Security/CustomSecurityStrategyComplex.cs)
* [ApplicationUser.cs](./CS/MySolution.Module/BusinessObjects/ApplicationUser.cs)
* [CustomAuthenticationStandardProvider.cs](./CS/MySolution.Module/Security/CustomAuthenticationStandardProvider.cs)
* [OAuthProvider.cs](./CS/MySolution.Web/Security/OAuthProvider.cs)
* [Startup.cs](./CS/MySolution.Web/Startup.cs)
* [WebApplication.cs](./CS/MySolution.Web/WebApplication.cs)

<!-- default file list end -->
## Documentation

* [Security (Access Control & Authentication)](https://docs.devexpress.com/eXpressAppFramework/113366/data-security-and-safety/security-system)
* [Authentication](https://docs.devexpress.com/eXpressAppFramework/119064/data-security-and-safety/security-system/authentication)

## More GitHub Examples

* [Use Active Directory and OAuth2 Authentication Providers in Blazor Applications](https://docs.devexpress.com/eXpressAppFramework/402197/task-based-help/security/how-to-use-active-directory-and-oauth2-authentication-providers-in-blazor-applications)
* [OAuth2 Authentication for WinForms Applications](https://www.devexpress.com/Support/Center/p/T567978)
