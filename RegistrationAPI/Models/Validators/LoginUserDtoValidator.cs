using FluentValidation;
using RegistrationAPI.Models.Account;

namespace RegistrationAPI.Models.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(dto => dto.Email).NotEmpty().WithMessage("Email is required")
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Incorrect Email");

            RuleFor(dto => dto.Password).NotEmpty().WithMessage("Password is required")
                .NotNull().WithMessage("Password is required");
        }
    }
}
