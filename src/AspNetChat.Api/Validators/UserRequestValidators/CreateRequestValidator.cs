using AspNetChat.Models.User;
using FluentValidation;

namespace AspNetChat.Api.Validators.UserRequestValidators
{
    public class CreateRequestValidator : AbstractValidator<CreateUserRequestModel>
    {
        public CreateRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty()
                .WithMessage("Required field");

            RuleFor(x => x.UserName).NotEqual(x => x.Name)
                .WithMessage("Username must be unique");

            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Required field");
        }
    }
}
