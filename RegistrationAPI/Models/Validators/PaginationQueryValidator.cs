using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using RegistrationAPI.Models.NewFolder;

namespace RegistrationAPI.Models.Validators
{
    public class PaginationQueryValidator : AbstractValidator<PaginationQuery>
    {
        private readonly string[] allowedOrderByKey = new[] { "name", "surname", "dateofbirth", "averagepowerconsumption" };
        public PaginationQueryValidator()
        {
            RuleFor(query => query.PageNumber).NotEmpty().WithMessage("Page number is required")
                .NotNull().WithMessage("Page number is required")
                .GreaterThanOrEqualTo(1).WithMessage("Page number must be greather than or equal to 1");

            RuleFor(query => query.PageSize).NotEmpty().WithMessage("Page size is required")
                .NotNull().WithMessage("Page size is required")
                .LessThanOrEqualTo(50).WithMessage("Page size must be lower than or equal to 50")
                .GreaterThanOrEqualTo(1).WithMessage("Page size must be greather than or equal to 1");

            RuleFor(query => query.OrderBy).NotEmpty().When(query => query.Descending.HasValue).WithMessage("OrderBy property is required. Allowed OrderBy phrases: Name, Surname, DateOfBirth, AveragePowerConsumption")
            .Custom((value, context) =>
            {
                if (!value.IsNullOrEmpty())
                {
                    if (!allowedOrderByKey.Contains(value.ToLower()))
                        context.AddFailure("Allowed OrderBy phrases: Name, Surname, DateOfBirth, AveragePowerConsumption");
                }
            });
        }
    }
}
