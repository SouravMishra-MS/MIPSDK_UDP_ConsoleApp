using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIPSDK_UDP_ConsoleApp.Services
{
    internal class AuthService
    {
        private static IPublicClientApplication? app;
        internal static void InitializeMSAL()
        {
            string authority = string.Concat(
                ConfigurationManager.AppSettings.Get("Authority"),
                ConfigurationManager.AppSettings.Get("TenantGuid")
                );
            app = PublicClientApplicationBuilder.Create(ConfigurationManager.AppSettings.Get("ClientId"))
                                                .WithAuthority(authority)
                                                .WithDefaultRedirectUri()
                                                .Build();
        }

        // Sign-in user and acquire token
        internal static async Task<string> SignInUserAndGetAccessTokenUsingMSAL(string[] scopes)
        {
            AuthenticationResult authResult;
            try
            {
                var acounts = await app!.GetAccountsAsync();
                authResult = await app.AcquireTokenSilent(scopes, acounts.FirstOrDefault()).ExecuteAsync();
            }
            catch(MsalUiRequiredException ex)
            {
                authResult = await app!.AcquireTokenInteractive(scopes)
                                       .WithClaims(ex.Claims)
                                       .ExecuteAsync();
            }
            return authResult.AccessToken;
        }

        // Sign-in and return account
        internal static async Task<IAccount> SignInUserAndAccountUsingMSAL(string[] scopes)
        {
            AuthenticationResult authResult;
            try
            {
                var acounts = await app!.GetAccountsAsync();
                authResult = await app.AcquireTokenSilent(scopes, acounts.FirstOrDefault()).ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                authResult = await app!.AcquireTokenInteractive(scopes)
                                       .WithClaims(ex.Claims)
                                       .ExecuteAsync();
            }
            return authResult.Account;
        }
    }
}
