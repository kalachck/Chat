using AspNetChat.Models.Message;
using FluentValidation;

namespace AspNetChat.Api.Validators.MessageRequestValidators
{
    public class CreateRequestValidator : AbstractValidator<CreateMessageRequestModel>
    {
        public CreateRequestValidator()
        {
            RuleFor(x => x.ChatName).NotEmpty()
                .WithMessage("Required field!");

            RuleFor(x => x.Content).NotEmpty()
                .WithMessage("Required field!");

            RuleFor(x => x.UserId).Must(x => x < 0)
                .WithMessage("Mus be more then zero!");
        }
    }
}
