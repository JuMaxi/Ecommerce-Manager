using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace EcommerceManager.Tests.Services
{
    public class CategoryServiceTests
    {
        private ICategoryDbAccess dbAccessFake;
        private IValidateCategory validatorFake;
        private CategoryService service;

        public CategoryServiceTests()
        {
            dbAccessFake = Substitute.For<ICategoryDbAccess>();
            validatorFake = Substitute.For<IValidateCategory>();

            service = new CategoryService(dbAccessFake, validatorFake);
        }

        [Fact]
        public async Task WhenInsertingNewCategory_ShouldSaveOnDatabase()
        {
            Category toInclude = new Category()
            {
                Name = "Test",
                Description = "Test Description",
                Parent = null
            };

            await service.InsertNewCategory(toInclude);

            // Assert
            await dbAccessFake.Received(1).AddNewCategory(toInclude);
        }

        [Fact]
        public async Task WhenInsertingNewCatetoryWithParent_ShouldGetParentFromDataBase()
        {
            Category toInclude = new Category()
            {
                Name = "Test",
                Description = "Test Description",
                Parent = new() { Id = 1 }
            };

            Category parent = new Category();

            dbAccessFake.GetCategoryFromDbById(toInclude.Parent.Id).Returns(parent);

            await service.InsertNewCategory(toInclude);

            await dbAccessFake.Received(1).AddNewCategory(toInclude);

            Assert.Equal(toInclude.Parent, parent);
        }

        [Fact]
        public async Task WhenUpdatingCategory_ShouldBeEqualCategoryReceived()
        {
            // This is the one we receive, we pass in the method's parameter
            Category toUpdate = new Category()
            {
                Id = 1,
                Name = "Test",
                Description = "Test Description",
                Image = "Image Test",
                Parent = null
            };

            // This is the one we get from the database, whose values will be updated with the above's values
            Category updated = new Category()
            {
                Id = 1,
                Name = "Trousers",
                Description = "Green Trousers S",
                Image = "NewImage Green Trousers",
                Parent = null
            };

            dbAccessFake.GetCategoryFromDbById(toUpdate.Id).Returns(updated);

            await service.UpdateCategory(toUpdate);

            await dbAccessFake.Received(1).UpdateCategory(updated);

            Assert.Equal(toUpdate.Name, updated.Name);
            Assert.Equal(toUpdate.Description, updated.Description);
            Assert.Equal(toUpdate.Image, updated.Image);
        }

        [Fact]
        public async Task WhenUpdatingCategoryParent_ShouldReturnCategoryParentFromDataBase()
        {
            // This is the one we receive, we pass in the method's parameter
            Category updated = new Category()
            {
                Id = 1,
                Parent = new() { Id = 10 }
            };


            // This is the one we get from the database, whose values will be updated with the above's values
            Category toUpdate = new Category()
            {
                Id = 1,
                Parent = new() { Id = 25 }
            };

            // This is just the parent, unchanged
            Category parent = new Category()
            {
                Id = 10,
            };

            dbAccessFake.GetCategoryFromDbById(updated.Id).Returns(toUpdate);
            dbAccessFake.GetCategoryFromDbById(updated.Parent.Id).Returns(parent);

            await service.UpdateCategory(updated);

            await dbAccessFake.Received(1).UpdateCategory(toUpdate);

            Assert.Equal(toUpdate.Parent, parent);
        }

        [Fact]
        public async Task WhenUpdatingCategoryIfParentIsNull_ShouldUpdateParentToNull()
        {
            Category updated = new Category()
            {
                Id = 1,
                Parent = null
            };

            Category toUpdate = new Category()
            {
                Id = 1,
                Parent = new() { Id = 3 }
            };

            dbAccessFake.GetCategoryFromDbById(updated.Id).Returns(toUpdate);

            await service.UpdateCategory(updated);

            Assert.Null(toUpdate.Parent);
        }

        [Fact]
        public async Task WhenDeletingCategoryIfThereAreNoChildrenCategories_ShouldNotThrowException()
        {
            Category delete = new Category()
            {
                Id = 1,
                Parent = null
            };

            dbAccessFake.GetCategoryFromDbByParentId(delete.Id).ReturnsNull();

            await service.DeleteCategory(delete.Id);

            await dbAccessFake.Received(1).DeleteCategory(delete.Id);
        }

        [Fact]
        public async Task WhenDeletingCategoryIfThereAreChildrenCategories_ShouldThrowException()
        {
            Category delete = new Category()
            {
                Id = 1,
                Parent = null
            };

            Category childrenCategory = new Category()
            {
                Id = 10,
                Parent = new() { Id = 1 }
            };

            dbAccessFake.GetCategoryFromDbByParentId(delete.Id).Returns(childrenCategory);

            await service.Invoking(svc => svc.DeleteCategory(delete.Id))
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage("There are children categories for Id " + delete.Id + ". Please, verify the children categories before delete.");
        }
    }
}
