using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using WebChat.Common.Utilities.Constants;

namespace WebChat.Common.Utilities.TokenProvider
{
    public class EmailConfirmationTokenProvider<User> : DataProtectorTokenProvider<User> where User : class
    {
        public EmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
        IOptions<EmailConfirmationTokenProviderOptions> options,
        ILogger<DataProtectorTokenProvider<User>> logger) : base(dataProtectionProvider, options, logger)
        {

        }

        public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
        {
            public EmailConfirmationTokenProviderOptions()
            {
                Name = "EmailDataProtectorTokenProvider";
                TokenLifespan = TimeSpan.FromHours(SystemConstant.LifeTimeConfirmEmail);
            }
        }
    }
}
