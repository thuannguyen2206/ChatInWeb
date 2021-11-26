using FluentValidation;
using WebChat.Common.ViewModels.Discussion;

namespace WebChat.Common.Validations
{
    public class NewChatValidator : AbstractValidator<NewChatViewModel>
    {
        public NewChatValidator()
        {
            //RuleFor(x => x.ListMembers).NotEmpty().WithMessage("Hãy chọn thành viên cho nhóm");

            RuleFor(x => x.NameGroup).MaximumLength(100).WithMessage("Tên nhóm quá dài");

        }
    }
}
