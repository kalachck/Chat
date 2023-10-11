using AspNetChat.Models.Chat;
using FluentValidation;

namespace AspNetChat.Api.Validators.ChatRequestValidators
{
    public class UpdateRequestValidator : AbstractValidator<UpdateChatRequestModel>
    {
        public UpdateRequestValidator()
        {
            RuleFor(x => x.ChatName).NotEmpty()
                .WithMessage("Required field!");
        }
    }
}
