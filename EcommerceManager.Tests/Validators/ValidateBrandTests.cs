using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Validators;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace EcommerceManager.Tests.Validators
{
    public class ValidateBrandTests
    {
        [Fact]
        public async Task When_Brand_Name_Is_Null_Should_Throw_Exception()
        {
            Brand brand = new() { Name = null };

            ValidateBrand validator = new(null);

            await validator.Invoking(validator => validator.Validate(brand))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The Name brand can't be null. Please, fill the name to continue.");
        }

        [Fact]
        public async Task When_Brand_Name_Is_Empty_Should_Throw_Exception()
        {
            Brand brand = new() { Name = "" };

            ValidateBrand validator = new(null);

            await validator.Invoking(validator => validator.Validate(brand))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The name brand can't be empty. Please, fill the name to continue.");
        }

        [Fact]
        public async Task When_Brand_Name_Already_Exist_Into_DataBase_Should_Throw_Exception()
        {
            Brand brandNew = new() { Id = 10, Name = "Guess" };
            Brand brandDB = new() { Id = 11, Name = "Guess" };

            var dbAccessBrand = Substitute.For<IBrandDbAccess>();
            dbAccessBrand.GetByName(brandNew.Name).Returns(brandDB);

            ValidateBrand validator = new(dbAccessBrand);

            await validator.Invoking(validator => validator.Validate(brandNew))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The name " + brandNew.Name + " already exist into database, please verify to continue.");
        }

        [Fact]
        public async Task When_Brand_FoundationYear_is_Zero_Should_Throw_Exception()
        {
            Brand brand = new() { Name = "Guess", FoundationYear = 0 };

            var dbAccessBrand = Substitute.For<IBrandDbAccess>();
            dbAccessBrand.GetByName(brand.Name).ReturnsNull();

            ValidateBrand validator = new(dbAccessBrand);

            await validator.Invoking(validator => validator.Validate(brand))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The Foundation Year can't be zero. Please fill the year to continue.");
        }

        [Fact]
        public async Task When_Brand_FoundationYear_is_Less_Than_Zero_Should_Throw_Exception()
        {
            Brand brand = new() { Name = "Guess", FoundationYear = -5 };

            var dbAccessBrand = Substitute.For<IBrandDbAccess>();
            dbAccessBrand.GetByName(brand.Name).ReturnsNull();

            ValidateBrand validator = new(dbAccessBrand);

            await validator.Invoking(validator => validator.Validate(brand))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The Foundation Year can't be less than zero. Please fill the year to continue");
        }

        [Fact]
        public async Task When_Brand_FoundationYear_is_Greater_Than_Actual_Year_Should_Throw_Exception()
        {
            Brand brand = new() { Name = "Guess", FoundationYear = 2025 };

            var dbAccessBrand = Substitute.For<IBrandDbAccess>();
            dbAccessBrand.GetByName(brand.Name).ReturnsNull();

            ValidateBrand validator = new(dbAccessBrand);

            int actualYear = DateTime.Now.Year;

            await validator.Invoking(validator => validator.Validate(brand))
                .Should().ThrowAsync<Exception>()
                .WithMessage("This year " + brand.FoundationYear + " is greater than the actual year " + actualYear +
                ". Please fill the correct year to continue");
        }
    }
}
