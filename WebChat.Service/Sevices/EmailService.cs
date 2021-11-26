using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WebChat.Entities.Model;
using WebChat.Service.IServices;

namespace WebChat.Service.Sevices
{
    public class EmailService : IEmailService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public EmailService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> EmailConfirmed(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            // Giải mã token
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var emailStatus = await _userManager.ConfirmEmailAsync(user, token);
            if (emailStatus.Succeeded)
                return true;
            return false;
        }

        public async Task<string> EmailConfirmationToken(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public void SendEmail(string userEmail, string content)
        {
            string myEmail = _configuration.GetValue<string>("FromMyEmail");
            string myPasswordEmail = _configuration.GetValue<string>("MyPasswordEmail");
            string displayMyEmail = "WebChat";

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(myEmail, displayMyEmail, Encoding.UTF8);
            mailMessage.To.Add(new MailAddress(userEmail));
            mailMessage.Subject = "Xác nhận email của bạn";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = content;

            SmtpClient client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod=SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(myEmail, myPasswordEmail)
            };

            client.Send(mailMessage);
        }

    }
}
