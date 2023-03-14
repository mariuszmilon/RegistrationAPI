using FluentValidation.TestHelper;
using RegistrationAPI.Models;
using RegistrationAPI.Models.Validators;

namespace RegistrationAPI.IntegrationTests
{
    public class LoginUserDtoValidatorTests
    {
        public static IEnumerable<object[]> GetValidData()
        {
            yield return new object[] { new LoginUserDto { Email = "mariusz@gmail.com", Password = "Pass123" } };
            yield return new object[] { new LoginUserDto { Email = "test@gmail.com", Password = "Test" } };
            yield return new object[] { new LoginUserDto { Email = "R@k", Password = "R@k" } };
        }

        public static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[] { new LoginUserDto { Email = "", Password = "Pass123" } };
            yield return new object[] { new LoginUserDto { Email = "test@gmail.com", Password = "" } };
            yield return new object[] { new LoginUserDto { Password = "R@k" } };
            yield return new object[] { new LoginUserDto { Email = "R@k" } };
        }

        [Theory]
        [MemberData(nameof(GetValidData))]
        public void Validate_ForCorrectModel_ReturnSuccess(LoginUserDto dto)
        {
            // arrange
            var validator = new LoginUserDtoValidator();

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetInvalidData))]
        public void Validate_ForIncorrectModel_ReturnFailure(LoginUserDto dto)
        {
            // arrange
            var validator = new LoginUserDtoValidator();

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldHaveAnyValidationError();
        }
    }
}
