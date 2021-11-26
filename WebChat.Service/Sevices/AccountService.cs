using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Common.Utilities.Enums;
using WebChat.Common.ViewModels.Account;
using WebChat.DataAccess.EF;
using WebChat.Entities.Model;
using WebChat.Service.IServices;

namespace WebChat.Service.Sevices
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly WebChatDbContext _context;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, WebChatDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }   

        /// <summary>
        /// Đăng nhập vào website
        /// Nếu tài khoản bị khóa (isDelete == true) thì return LockedOut
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SignInResult> Login(LoginViewModel model)
        {
            var checkUser = _context.Users.Where(x => x.UserName == model.Username && x.IsLocked == true).FirstOrDefault();
            if (checkUser != null)
            {
                return SignInResult.LockedOut;
            }
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, true);
            return result;
        }

        /// <summary>
        /// Đăng kí tài khoản
        /// Chỉ đăng kí thành công nếu username và email đăng kí chưa có trong database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<RegisterAccountResult> Register(RegisterViewModel model)
        {
            if (await _userManager.FindByNameAsync(model.Username) != null)
                return RegisterAccountResult.ExistUsername;

            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return RegisterAccountResult.ExistEmail;

            var user = new User()
            {
                UserName = model.Username,
                Email = model.Email,
                AvatarLink = model.AvatarLink,
                Id = Guid.NewGuid()
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                return RegisterAccountResult.Successed;

            return RegisterAccountResult.Failed;
        }

    }
}
