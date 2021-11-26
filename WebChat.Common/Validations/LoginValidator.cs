using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Common.ViewModels.Account;

namespace WebChat.Common.Validations
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Chưa nhập tên tài khoản");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Chưa nhập mật khẩu")
                .MinimumLength(6).WithMessage("Mật khẩu có ít nhất 6 ký tự");
        }
    }
}
