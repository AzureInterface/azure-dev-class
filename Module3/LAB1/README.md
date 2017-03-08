# Lab 1: Implement WS-federation with AAD and an ASP.NET MVC App

* Add a test user account to your Azure AD tenant
* Download the project: https://github.com/Azure-Samples/active-directory-dotnet-webapp-wsfederation
* Note the HTTP/Non-SSL url for the application (e.g. http://localhost:4279/)
* Add a new app registration in Azure AD. Set the sign-on URL to the SignIn controller in your application (e.g. http://localhost:4279/Account/SignIn/)
* Update the Wtrealm and Tenant keys in your web.config using the correct values from the Azure AD portal
* Run the application locally and sign in using the credentials for your Azure AD test user
