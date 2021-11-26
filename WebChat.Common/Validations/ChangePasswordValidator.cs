using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Common.ViewModels.User;

namespace WebChat.Common.Validations
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.PresentPassword).NotEmpty().WithMessage("Bạn chưa nhập mật khẩu.")
                .MinimumLength(6).WithMessage("Mật khẩu có ít nhất 6 ký tự.")
                .MaximumLength(50).WithMessage("Mật khẩu không quá 50 kí tự.");

            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Chưa nhập mật khẩu mới.")
                .MinimumLength(6).WithMessage("Mật khẩu có ít nhất 6 ký tự.")
                .MaximumLength(50).WithMessage("Mật khẩu không quá 50 kí tự.");

            RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessage("Xác nhận mật khẩu mới không khớp.");
        }
     
    }
}
