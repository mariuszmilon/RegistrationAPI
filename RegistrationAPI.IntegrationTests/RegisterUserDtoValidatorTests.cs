using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using RegistrationAPI.Entities;
using RegistrationAPI.Models.Account;
using RegistrationAPI.Models.Validators;

namespace RegistrationAPI.IntegrationTests
{
    public class RegisterUserDtoValidatorTests
    {
        private readonly ApplicationDbContext _dbContext;
        public RegisterUserDtoValidatorTests()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("TestingDb");
            _dbContext = new ApplicationDbContext(builder.Options);
            SeedUser();
        }

        [Fact]
        public void Validate_ForValiModel_ReturnSuccess()
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);
            var dto = CreateValidRegisterUserDto();

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        public void Validate_ForInvalidName_ReturnFailure(string invalidName)
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);
            var dto = CreateValidRegisterUserDto();
            dto.Name = invalidName;

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor("Name");
        }

        [Theory]
        [InlineData("")]
        public void Validate_ForInvalidSurname_ReturnFailure(string invalidSurname)
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);
            var dto = CreateValidRegisterUserDto();
            dto.Surname = invalidSurname;

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor("Surname");
        }

        [Theory]
        [InlineData("@test")]
        [InlineData("test@")]
        [InlineData("test")]
        [InlineData("test12311")]
        [InlineData("test12311.pl")]
        [InlineData("test12311.gmail.com")]
        [InlineData("")]
        [InlineData("mariusz@gmail.com")]
        public void Validate_ForInvalidEmail_ReturnFailure(string invalidEmail)
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);
            var dto = CreateValidRegisterUserDto();
            dto.Email = invalidEmail;

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor("Email");
        }

        [Theory]
        [InlineData("739974019")]
        [InlineData("739 974 019")]
        [InlineData("+48738974019")]
        [InlineData("+48 738974019")]
        [InlineData("32 6134620")]
        [InlineData("326134620")]
        [InlineData("739-974-019")]
        public void Validate_ForValidPhoneNumber_ReturnSuccess(string invalidPhoneNumber)
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);
            var dto = CreateValidRegisterUserDto();
            dto.PhoneNumber = invalidPhoneNumber;

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldNotHaveValidationErrorFor("PhoneNumber");
        }

        [Theory]
        [InlineData("123")]
        [InlineData("asdasdasdxsasd")]
        [InlineData("98765423a")]
        [InlineData("987654231987564231")]
        [InlineData("+48 987 654 321")]
        public void Validate_ForInvalidPhoneNumber_ReturnFailure(string invalidPhoneNumber)
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);
            var dto = CreateValidRegisterUserDto();
            dto.PhoneNumber = invalidPhoneNumber;

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor("PhoneNumber");
        }

        [Theory]
        [InlineData("123")]
        [InlineData("asdasdasdxsasd")]
        [InlineData("98765423a")]
        [InlineData("987654231987564231")]
        [InlineData("aas")]
        [InlineData("TestHasla123")]
        [InlineData("Test@isasd")]
        [InlineData("setasdwasdw!asd3")]
        [InlineData("setasdwa@@asd3")]
        [InlineData("setasdwasd2$*@#asd3")]
        public void Validate_ForInvalidPassword_ReturnFailure(string invalidPassword)
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);
            var dto = CreateValidRegisterUserDto();
            dto.Password = invalidPassword;

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor("Password");
        }

        [Theory]
        [InlineData("Test123!@")]
        [InlineData("Test123)")]
        [InlineData("12312312aeE1&)")]
        [InlineData("Testowe1&)")]
        [InlineData("Nananna92*")]
        public void Validate_ForValidPassword_ReturnSuccess(string validPassword)
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);
            var dto = CreateValidRegisterUserDto();
            dto.Password = validPassword;

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldNotHaveValidationErrorFor("Password");
        }

        [Fact]
        public void Validate_ForNotMatchingConfirmedPassword_ReturnFailure()
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);
            var dto = CreateValidRegisterUserDto();
            dto.ConfirmPassword = "Lala1";

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor("ConfirmPassword");
        }

        [Theory]
        [InlineData("48102129319")]
        [InlineData("66040435712")]
        [InlineData("87092954628")]
        [InlineData("93073199948")]
        [InlineData("93081396188")]
        [InlineData("65031913147")]
        public void Validate_ForValidPersonalIdNumber_ReturnSuccess(string validPersonalIdNumber)
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);
            var dto = CreateValidRegisterUserDto();
            dto.PersonalIdNumber = validPersonalIdNumber;

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldNotHaveValidationErrorFor("PersonalIdNumber");
        }

        [Theory]
        [InlineData("12365478965")]
        [InlineData("asdasd")]
        [InlineData("1231asd1233")]
        [InlineData("asd12312331")]
        [InlineData("87092952628")]
        [InlineData("15031913147")]
        public void Validate_ForInvalidPersonalIdNumber_ReturnFailure(string invalidPersonalIdNumber)
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);
            var dto = CreateValidRegisterUserDto();
            dto.PersonalIdNumber = invalidPersonalIdNumber;

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor("PersonalIdNumber");
        }

        [Theory]
        [InlineData(123.1232)]
        [InlineData(123.1123232)]
        [InlineData(112123131223.1)]
        public void Validate_ForValidPersonalIdNumber_ReturnFailure(decimal invalidAveragePowerConsumption)
        {
            // arrange
            var validator = new RegisterUserDtoValidator(_dbContext);
            var dto = CreateValidRegisterUserDto();
            dto.AveragePowerConsumption = invalidAveragePowerConsumption;

            // act
            var result = validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor("AveragePowerConsumption");
        }

        private void SeedUser()
        {
            var user = new User()
            {
                Name = "mariusz",
                Surname = "mariusz",
                Email = "mariusz@gmail.com",
                PhoneNumber = "987654321",
                DateOfBirth = new DateTime(2018, 6, 10),
                PersonalIdNumber = "84061945227",
                AveragePowerConsumption = 1231
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        private RegisterUserDto CreateValidRegisterUserDto()
        {
            var dto = new RegisterUserDto()
            {
                Name = "test",
                Surname = "test",
                Email = "test@gmail.com",
                PhoneNumber = "987654321",
                DateOfBirth = new DateTime(2011, 6, 10),
                Password = "Test123!",
                ConfirmPassword = "Test123!",
                PersonalIdNumber = "84061945227",
                AveragePowerConsumption = 1231
            };
            return dto;
        }
    }
}
