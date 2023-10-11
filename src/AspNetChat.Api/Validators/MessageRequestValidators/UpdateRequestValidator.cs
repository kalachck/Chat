using AspNetChat.Models.Message;
using FluentValidation;

namespace AspNetChat.Api.Validators.MessageRequestValidators
{
    public class UpdateRequestValidator : AbstractValidator<UpdateMessageRequestModel>
    {
        public UpdateRequestValidator()
        {
            RuleFor(x => x.Content).NotEmpty()
                .WithMessage("Required field");
        }
    }
}
