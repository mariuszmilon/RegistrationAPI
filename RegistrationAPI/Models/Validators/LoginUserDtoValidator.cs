using FluentValidation;

namespace RegistrationAPI.Models.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(dto => dto.Email).NotEmpty().NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Incorrect Email");

            RuleFor(dto => dto.Password).NotEmpty().NotNull().WithMessage("Password is required");
        }
    }
}
