﻿using EcommerceManager.Interfaces;
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
        public async Task When_Insert_Category_Should_Save_On_Data_Base()
        {
            Category toInclude = new Category()
            {
                Name = "Test",
                Description = "Test Description",
                Parent = null
            };

            await service.Insert(toInclude);

            // Assert
            await dbAccessFake.Received(1).Insert(toInclude);
        }

        [Fact]
        public async Task When_Insert_Catetory_With_Parent_Should_Get_Parent_From_Data_Base()
        {
            Category toInclude = new Category()
            {
                Name = "Test",
                Description = "Test Description",
                Parent = new() { Id = 1 }
            };

            Category parent = new Category();

            dbAccessFake.GetById(toInclude.Parent.Id).Returns(parent);

            await service.Insert(toInclude);

            await dbAccessFake.Received(1).Insert(toInclude);

            Assert.Equal(toInclude.Parent, parent);
        }

        [Fact]
        public async Task When_Update_Category_Should_Be_Equal_Category_Received()
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

            dbAccessFake.GetById(toUpdate.Id).Returns(updated);

            await service.Update(toUpdate);

            await dbAccessFake.Received(1).Update(updated);

            Assert.Equal(toUpdate.Name, updated.Name);
            Assert.Equal(toUpdate.Description, updated.Description);
            Assert.Equal(toUpdate.Image, updated.Image);
        }

        [Fact]
        public async Task When_Update_Category_Parent_Should_Return_Category_Parent_From_Data_Base()
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

            dbAccessFake.GetById(updated.Id).Returns(toUpdate);
            dbAccessFake.GetById(updated.Parent.Id).Returns(parent);

            await service.Update(updated);

            await dbAccessFake.Received(1).Update(toUpdate);

            Assert.Equal(toUpdate.Parent, parent);
        }

        [Fact]
        public async Task When_Update_Category_If_Parent_Is_Null_Should_Update_Parent_To_Null()
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

            dbAccessFake.GetById(updated.Id).Returns(toUpdate);

            await service.Update(updated);

            Assert.Null(toUpdate.Parent);
        }

        [Fact]
        public async Task When_Delete_Category_If_There_Are_No_Children_Categories_Should_Not_Throw_Exception()
        {
            Category delete = new Category()
            {
                Id = 1,
                Parent = null
            };

            dbAccessFake.GetByParentId(delete.Id).ReturnsNull();

            await service.Delete(delete.Id);

            await dbAccessFake.Received(1).Delete(delete.Id);
        }

        [Fact]
        public async Task When_Delete_Category_If_There_Are_Children_Categories_Should_Throw_Exception()
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

            dbAccessFake.GetByParentId(delete.Id).Returns(childrenCategory);

            await service.Invoking(svc => svc.Delete(delete.Id))
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage("There are children categories for Id " + delete.Id + ". Please, verify the children categories before delete.");
        }

        [Fact]
        public async Task When_Delete_Category_Should_Category_Db_Access_Receive_One_Call()
        {
            Category category = new Category() { Id = 1, };

            await service.Delete(category.Id);

            await dbAccessFake.Received(1).Delete(category.Id);
        }

        [Fact]
        public async Task When_GetCount_Category_Should_Category_Db_Access_Receive_One_Call()
        {
            await service.GetCount();

            await dbAccessFake.Received(1).GetCount();
        }
    }
}
