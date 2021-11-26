using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.Service.IServices
{
    public interface IEmailService
    {
        /// <summary>
        /// Confirmed user email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> EmailConfirmed(string email, string token);

        /// <summary>
        /// Get token to confirm email
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<string> EmailConfirmationToken(string username);

        /// <summary>
        /// Sent email to user with content body
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="content"></param>
        void SendEmail(string userEmail, string content);

    }
}
