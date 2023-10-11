using AspNetChat.Models.Chat;
using FluentValidation;

namespace AspNetChat.Api.Validators.ChatRequestValidators
{
    public class CreateRequestValidator : AbstractValidator<CreateChatRequestModel>
    {
        public CreateRequestValidator()
        {
            RuleFor(x => x.ChatName).NotEmpty()
                .WithMessage("Required field!");

            RuleFor(x => x.CreatorId).Must(x => x > 0)
                .WithMessage("Must be more then zero!");
        }
    }
}
