using FluentValidation;
using RegistrationAPI.Entities;
using RegistrationAPI.Models.Account;
using System.Text.RegularExpressions;

namespace RegistrationAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        private readonly int[] weights = new int[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
        public RegisterUserDtoValidator(ApplicationDbContext dbContext)
        {
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("Name is required")
                .NotNull().WithMessage("Name is required");

            RuleFor(dto => dto.Surname).NotEmpty().WithMessage("Surname is required")
                .NotNull().WithMessage("Surname is required");

            RuleFor(dto => dto.Email).NotEmpty().WithMessage("Email is required")
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Incorrect Email")
                .Custom((value, context) =>
                {
                    var emailInDb = dbContext.Users.Any(u => u.Email == value);
                    if(emailInDb)
                    {
                        context.AddFailure("This email has already been taken");
                    }
                });

            RuleFor(dto => dto.PersonalIdNumber).NotEmpty().WithMessage("Personal identification number is required")
                .NotNull().WithMessage("Personal identification number is required")
                .Length(11).WithMessage("Personal identification number should contain 11 digits")
                .Custom((value, context) =>
                {
                    if (value != null && value.Length == 11 &&  value.All(char.IsDigit))
                    {
                        int sum = 0;
                        int temp = 0;
                        for (int i = 0; i < weights.Length; i++)
                        {
                            temp = weights[i] * int.Parse(value[i].ToString());
                            if (temp >= 10)
                                temp = temp % 10;
                            sum += temp;
                        }
                        if (sum >= 10)
                            sum = sum % 10;
                        var controlSum = 10 - sum;
                        if (value.Last().ToString() != controlSum.ToString())
                            context.AddFailure("Incorrect personal identification number");
                    }
                    else
                        context.AddFailure("Incorrect personal identification number");
                });

            RuleFor(dto => dto.Password).NotEmpty().WithMessage("Password is required")
                .NotNull().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must contain at least 8 characters")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Your password must contain a special character");

            RuleFor(dto => dto.ConfirmPassword).NotEmpty().WithMessage("Confirmed password is required")
                .NotNull().WithMessage("Confirmed password is required")
                .Equal(cp => cp.Password).WithMessage("Passwords do not match");

            RuleFor(dto => dto.PhoneNumber).NotEmpty().WithMessage("Phone number is required")
                .NotNull().WithMessage("Phone number is required")
                .Matches(new Regex("^\\+?[1-9][0-9\\s.-]{7,11}$")).WithMessage("Invalid phone number")
                .MaximumLength(16).WithMessage("Invalid phone number")
                .MinimumLength(7).WithMessage("Invalid phone number");

            RuleFor(dto => dto.AveragePowerConsumption)
                .ScalePrecision(3, 8, false).WithMessage("‘AveragePowerConsumption’ must not be more than 8 digits in total, with allowance for 3 decimals");

            RuleFor(dto => dto.DateOfBirth).LessThan(DateTime.Now).WithMessage("Incorrect date of birth");
        }
    }
}
