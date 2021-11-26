using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebChat.Common.Utilities.Enums;
using WebChat.Common.ViewModels.Account;

namespace WebChat.Service.IServices
{
    public interface IAccountService
    {
        /// <summary>
        /// User login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<SignInResult> Login(LoginViewModel model);

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<RegisterAccountResult> Register(RegisterViewModel model);

    }
}
