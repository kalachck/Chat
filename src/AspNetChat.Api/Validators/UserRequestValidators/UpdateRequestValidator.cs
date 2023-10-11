using AspNetChat.Models.User;
using FluentValidation;

namespace AspNetChat.Api.Validators.UserRequestValidators
{
    public class UpdateRequestValidator : AbstractValidator<UpdateUserRequestModel>
    {
        public UpdateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Required field");
        }
    }
}
