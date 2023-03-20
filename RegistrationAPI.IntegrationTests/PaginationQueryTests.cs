using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using RegistrationAPI.Models.NewFolder;
using RegistrationAPI.Models.Validators;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RegistrationAPI.IntegrationTests
{
    public class PaginationQueryTests
    {
        public static IEnumerable<object[]> GetValidQuery()
        {
            yield return new object[] { new PaginationQuery { SearchString = "", PageSize = 30, PageNumber = 1, OrderBy = "Name" } };
            yield return new object[] { new PaginationQuery { SearchString = "mariusz", PageSize = 10, PageNumber = 2, OrderBy = "Surname", Descending = true } };
            yield return new object[] { new PaginationQuery { SearchString = "test", PageSize = 30, PageNumber = 1, OrderBy = "DateOfBirth", Descending = false} };
            yield return new object[] { new PaginationQuery { SearchString = "aaaaa", PageSize = 30, PageNumber = 1, OrderBy = "AveragePowerConsumption" } };
        }

        [Theory]
        [MemberData(nameof(GetValidQuery))]
        public void Validate_ForValiModel_ReturnSuccess(PaginationQuery query)
        {
            // arrange
            var validator = new PaginationQueryValidator();

            // act
            var result = validator.TestValidate(query);

            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(51)]
        public void Validate_ForInvalidPageSize_ReturnFailure(int pageSize)
        {
            // arrange
            var validator = new PaginationQueryValidator();
            var query = CreateValidPaginationQuery();
            query.PageSize = pageSize;

            // act
            var result = validator.TestValidate(query);

            // assert
            result.ShouldHaveValidationErrorFor("PageSize");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validate_ForInvalidPageNumer_ReturnFailure(int pageNumber)
        {
            // arrange
            var validator = new PaginationQueryValidator();
            var query = CreateValidPaginationQuery();
            query.PageNumber = pageNumber;

            // act
            var result = validator.TestValidate(query);

            // assert
            result.ShouldHaveValidationErrorFor("PageNumber");
        }

        [Theory]
        [InlineData("test")]
        [InlineData("PageNumber")]
        public void Validate_ForInvalidOrderBy_ReturnFailure(string orderBy)
        {
            // arrange
            var validator = new PaginationQueryValidator();
            var query = CreateValidPaginationQuery();
            query.OrderBy = orderBy;

            // act
            var result = validator.TestValidate(query);

            // assert
            result.ShouldHaveValidationErrorFor("OrderBy");
        }


        private PaginationQuery CreateValidPaginationQuery()
        {
            var query = new PaginationQuery()
            {
                SearchString = "mariusz",
                PageSize = 10,
                PageNumber = 2,
                OrderBy = "Surname",
                Descending = true
            };
            return query;
        }   
    }
}
