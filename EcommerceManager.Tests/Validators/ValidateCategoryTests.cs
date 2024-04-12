using EcommerceManager.DbAccess;
using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Validators;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace EcommerceManager.Tests.Validators
{
    public class ValidateCategoryTests
    {
        [Fact]
        public async Task When_name_is_null_should_throw_new_exception()
        {
            Category category = new() { Name = null };

            ValidateCategory validatorCategory = new(null);

            await validatorCategory.Invoking(validator => validator.Validate(category))
            .Should().ThrowAsync<Exception>()
            .WithMessage("The Name field must be filled to continue.");
        }

        [Fact]
        public async Task When_name_length_is_zero_should_throw_new_exception()
        {
            Category category = new() { Name = "" };

            ValidateCategory validatorCategory = new(null);

            await validatorCategory.Invoking(validator => validator.Validate(category))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The Name field must be filled to continue.");
        }

        [Fact]
        public async Task When_name_is_already_registered_into_database_should_throw_new_exception()
        {
            Category category = new() { Name = "Trousers" };
            Category category2 = new() { Name = "Trousers" };

            var dbAccessCategoryFake = Substitute.For<ICategoryDbAccess>();
            dbAccessCategoryFake.GetByName(category2.Name).Returns(Task.FromResult(category2));

            ValidateCategory validatorCategory = new(dbAccessCategoryFake);

            await validatorCategory.Invoking(validator => validator.Validate(category))
                .Should().ThrowAsync<Exception>()
                .WithMessage("This category Name " + category2.Name + " is already registered. Change the name to continue.");
        }

        [Fact]
        public async Task When_description_is_null_should_throw_new_exception()
        {
            Category category = new()
            {
                Name = "Trousers",
                Description = null
            };

            Category category2 = new()
            {
                Name = "Skirt"
            };

            var dbAccessCategoryFake = Substitute.For<ICategoryDbAccess>();
            dbAccessCategoryFake.GetByName(category2.Name).Returns(Task.FromResult(category2));   

            ValidateCategory validatorCategory = new(dbAccessCategoryFake);

            await validatorCategory.Invoking(validator => validator.Validate(category))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The Description field must be filled to continue");
        }

        [Fact]
        public async Task When_description_length_is_zero_should_throw_new_exception()
        {
            Category category = new()
            {
                Name = "Trousers",
                Description = ""
            };

            Category category2 = new()
            {
                Name = "Skirt"
            };

            var dbAccessCategoryFake = Substitute.For<ICategoryDbAccess>();
            dbAccessCategoryFake.GetByName(category2.Name).Returns(Task.FromResult(category2));

            ValidateCategory validatorCategory = new(dbAccessCategoryFake);

            await validatorCategory.Invoking(validator => validator.Validate(category))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The Description field must be filled to continue");
        }

        [Fact]
        public async Task When_description_is_already_registered_into_database_should_throw_new_exception()
        {
            Category category = new()
            {
                Name = "Trousers",
                Description = "Women Trousers"
            };

            Category category2 = new()
            {
                Name = "Skirts",
                Description = "Women Trousers"
            };

            var dbAccessCategoryFake = Substitute.For<ICategoryDbAccess>();
            dbAccessCategoryFake.GetByDescription(category2.Description).Returns(category2);

            ValidateCategory validatorCategory = new(dbAccessCategoryFake);

            await validatorCategory.Invoking(validator => validator.Validate(category))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The category Description " + category.Description + " is already registered. Change the description to continue");
        }

        [Fact]
        public async Task When_image_is_null_should_throw_new_exception()
        {
            Category category = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Image = null
            };

            Category category2 = new()
            {
                Id = 1,
                Name = "Skirt",
                Description = "Women Skirts"
            };

            var dbAccessCategoryFake = Substitute.For<ICategoryDbAccess>();
            dbAccessCategoryFake.GetById(category2.Id).Returns(Task.FromResult(category2));

            ValidateCategory validatorCategory = new(dbAccessCategoryFake);

            await validatorCategory.Invoking(validator => validator.Validate(category))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The Image field must be filled to continue");
        }

        [Fact]
        public async Task When_image_length_is_zero_should_throw_new_exception()
        {
            Category category = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Image = ""
            };

            Category category2 = new()
            {
                Id = 1,
                Name = "Skirt",
                Description = "Women Skirts"
            };

            var dbAccessCategoryFake = Substitute.For<ICategoryDbAccess>();
            dbAccessCategoryFake.GetById(category2.Id).Returns(Task.FromResult(category2));

            ValidateCategory validatorCategory = new(dbAccessCategoryFake);

            await validatorCategory.Invoking(validator => validator.Validate(category))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The Image field must be filled to continue");
        }

        [Fact]
        public async Task WhenCategoryParentDoesnotExistIntoDataBase_ShouldThrowException()
        {
            Category category = new()
            {
                Id = 1,
                Name = "Skirt",
                Description = "Women Skirts",
                Image = "Image Test",
                Parent = new() { Id = 10}
            };

            var dbAccessFake = Substitute.For<ICategoryDbAccess>();
            dbAccessFake.GetById(category.Parent.Id).ReturnsNull();

            ValidateCategory validatorCategory = new(dbAccessFake);

            await validatorCategory.Invoking(validator => validator.Validate(category))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The parent Id " + category.Parent.Id + " doesn't exist. Please verify before continue.");
        }
    }
}
