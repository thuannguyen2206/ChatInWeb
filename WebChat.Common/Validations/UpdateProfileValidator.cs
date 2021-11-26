using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Common.ViewModels.User;

namespace WebChat.Common.Validations
{
    public class UpdateProfileValidator : AbstractValidator<UserViewModel>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.FirstName).MaximumLength(200).WithMessage("Tên quá dài");

            RuleFor(x => x.LastName).MaximumLength(200).WithMessage("Họ quá dài");

            RuleFor(x => x.Phone).MaximumLength(30).WithMessage("Số điện thoại quá dài");

            RuleFor(x => x.Address).MaximumLength(200).WithMessage("Địa chỉ quá dài");
        }
    }
}
