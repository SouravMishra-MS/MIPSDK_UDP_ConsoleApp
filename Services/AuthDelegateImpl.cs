using Microsoft.Identity.Client;
using Microsoft.InformationProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIPSDK_UDP_ConsoleApp.Services
{
    internal class AuthDelegateImpl: IAuthDelegate
    {
        private ApplicationInfo _appInfo;
        private IPublicClientApplication _app;

        public AuthDelegateImpl(ApplicationInfo appInfo)
        {
            _appInfo = appInfo;
            _app = PublicClientApplicationBuilder.Create(_appInfo.ApplicationId)
                                                 .Build();
        }

        public string AcquireToken(Identity identity, string authority, string resource, string claims)
        {
            string[] scopes = new string[]
            {
                resource[resource.Length - 1].Equals('/')?$"{resource}.default":$"{resource}/.default"
            };
            return AuthService.SignInUserAndGetAccessTokenUsingMSAL(scopes).Result;
        }
    }
}
