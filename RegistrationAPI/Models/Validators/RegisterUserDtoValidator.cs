using FluentValidation;
using RegistrationAPI.Entities;
using System.Runtime.InteropServices;
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
                        context.AddFailure("That email is already taken");
                    }
                });

            RuleFor(dto => dto.PersonalIdNumber).NotEmpty().WithMessage("Personal id number is required")
                .NotNull().WithMessage("Personal id number is required")
                .Length(11).WithMessage("Incorrect Personal Id Number")
                .Custom((value, context) =>
                {
                    if (value.Length == 11 && value != null && value.All(char.IsDigit))
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
                            context.AddFailure("Incorrect Personal Id Number");
                    }
                    else
                        context.AddFailure("Incorrect Personal Id Number");
                });

            RuleFor(dto => dto.Password).NotEmpty().WithMessage("Password is required")
                .NotNull().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("The password is too short")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Your password must contain a special character");

            RuleFor(dto => dto.ConfirmPassword).NotEmpty().NotNull().WithMessage("Confirmed password is required")
                .Equal(cp => cp.Password).WithMessage("Passwords do not match");

            RuleFor(dto => dto.PhoneNumber).NotEmpty().WithMessage("Phone number is required")
                .NotNull().WithMessage("Phone number is required")
                .Matches(new Regex("^\\+?[1-9][0-9\\s.-]{7,11}$")).WithMessage("Invalid phone number")
                .MaximumLength(16).WithMessage("Phone number is too long")
                .MinimumLength(7).WithMessage("Phone number is too short");

            RuleFor(dto => dto.AveragePowerConsumption)
                .ScalePrecision(3, 8, false).WithMessage("Incorrect average power consumption");

            RuleFor(dto => dto.DateOfBirth).LessThan(DateTime.Now).WithMessage("Incorrect date of birth");
        }
    }
}
