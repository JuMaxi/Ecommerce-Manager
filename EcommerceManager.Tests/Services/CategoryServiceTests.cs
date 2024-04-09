using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Services;
using NSubstitute;

namespace EcommerceManager.Tests.Services
{
    public class CategoryServiceTests
    {
        [Fact]
        public async Task WhenInsertingNewCategory_ShouldSaveOnDatabase()
        {
            ICategoryDbAccess dbAccessFake = Substitute.For<ICategoryDbAccess>();
            IValidateCategory validatorFake = Substitute.For<IValidateCategory>();

            CategoryService service = new CategoryService(dbAccessFake, validatorFake);

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
            ICategoryDbAccess dbAccessFake = Substitute.For<ICategoryDbAccess>();
            IValidateCategory validatorFake = Substitute.For<IValidateCategory>();

            CategoryService service = new CategoryService(dbAccessFake, validatorFake);


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
            ICategoryDbAccess dbAccessFake = Substitute.For<ICategoryDbAccess>();
            IValidateCategory validatorFake = Substitute.For<IValidateCategory>();

            CategoryService service = new CategoryService(dbAccessFake, validatorFake);

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
            ICategoryDbAccess dbAccessFake = Substitute.For<ICategoryDbAccess>();
            IValidateCategory validateCategory = Substitute.For<IValidateCategory>();

            CategoryService service = new CategoryService(dbAccessFake, validateCategory);

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

    }
}
