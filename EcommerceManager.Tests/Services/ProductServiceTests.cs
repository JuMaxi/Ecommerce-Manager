using EcommerceManager.Domain.Interfaces;
using EcommerceManager.Domain.Models;
using EcommerceManager.Domain.Services;
using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;

namespace EcommerceManager.Tests.Services
{
    public class ProductServiceTests
    {
        private IProductDbAccess _productDbAccess;
        private ICategoryDbAccess _categoryDbAccess;
        private IBrandDbAccess _brandDbAccess;
        private IValidateProduct _validateProduct;
        private ProductService _productService;

        public ProductServiceTests() 
        {
            _productDbAccess = Substitute.For<IProductDbAccess>();
            _categoryDbAccess = Substitute.For<ICategoryDbAccess>();
            _brandDbAccess = Substitute.For<IBrandDbAccess>();
            _validateProduct = Substitute.For<IValidateProduct>();
            _productService = new ProductService(_productDbAccess, _categoryDbAccess, _brandDbAccess, _validateProduct);
        }

        [Fact]
        public async Task When_Insert_Product_Should_Product_Db_Access_Receive_One_Call()
        {
            Product product = new()
            {
                Category = new() { Id = 1 },
                Brand = new() { Id = 2 },
            };

            await _productService.Insert(product);
            
            await _productDbAccess.Received(1).Insert(product);
        }

        [Fact]
        public async Task When_Insert_Product_Category_Should_Category_Db_Access_Receive_One_Call()
        {
            Product product = new()
            {
                Category = new()
                {
                    Id = 1
                },
                Brand = new() 
                { 
                    Id = 10
                }
            };

            Category category = new();
            _categoryDbAccess.GetById(product.Category.Id).Returns(category);

            await _productService.Insert(product);

            await _productDbAccess.Received(1).Insert(product);

            Assert.Equal(product.Category, category);
        }

        [Fact]
        public async Task When_Insert_Product_Brand_Should_Brand_Db_Access_Receive_One_Call()
        {
            Product product = new()
            {
                Category = new()
                {
                    Id = 1
                },
                Brand = new()
                {
                    Id = 10
                }
            };

            Brand brand = new();
            _brandDbAccess.GetById(product.Brand.Id).Returns(brand);

            await _productService.Insert(product);

            await _productDbAccess.Received(1).Insert(product);

            Assert.Equal(product.Brand, brand);
        }

        [Fact]
        public async Task When_Get_All_If_Limit_Is_Less_Than_Zero_Limit_Should_Be_10()
        {
            int limit = -5;
            int page = 1;
            int skip = 0;
            
            await _productService.GetAll(limit, page);

            int newLimit = 10;

            await _productDbAccess.Received(1).GetAll(skip, newLimit);
        }

        [Fact]
        public async Task When_Get_All_If_Limit_Is_Greater_Than_1000_Limit_Should_Be_10()
        {
            int limit = 1005;
            int page = 1;
            int skip = 0;

            await _productService.GetAll(limit, page);

            int newLimit = 10;

            await _productDbAccess.Received(1).GetAll(skip, newLimit);
        }

        [Fact]
        public async Task When_Get_All_If_Page_Is_Less_Than_Zero_Page_Should_Be_1()
        {
            int limit = 10;
            int page = -3;
            int skip = 0;

            await _productService.GetAll(limit, page);

            //If page is equal 1, skip is equal 0;
            await _productDbAccess.GetAll(skip, limit);
        }

        [Fact]
        public async Task When_Get_All_If_Page_Is_Zero_Page_Should_Be_1()
        {
            int limit = 10;
            int page = 0;
            int skip = 0;

            await _productService.GetAll(limit, page);

            //If page is equal 0, skip is equal 0;
            await _productDbAccess.Received(1).GetAll(skip, limit);
        }

        [Fact]
        public async Task When_Get_All_If_Page_Is_Greater_Than_1_Skip_Should_Be_Limit_Multiplied_For_Page_Less_1()
        {
            int limit = 10;
            int page = 2;
            int skip = limit * (page - 1);

            await _productService.GetAll(limit, page);

            await _productDbAccess.Received(1).GetAll(skip, limit);
        }

        [Fact]
        public async Task When_Get_All_Product_Should_Product_Db_Access_Receive_One_Call()
        {
            int limit = 10;
            int page = 1;
            int skip = 0;

            await _productService.GetAll(limit, page);

            await _productDbAccess.Received(1).GetAll(skip, limit);
        }

        [Fact]
        public async Task When_Update_Received_Product_Should_Be_Equal_Updated_Product()
        {
            Product product = new()
            {
                Id = 1,
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 },
                SKU = "14253678",
                Dimensions = new() { Width = 5, Height = 3, Length = 1 }
            };

            Product toUpdate = new()
            {
                Id = 1,
                Name = "Dress",
                Description = "Women Dress",
                Category = new() { Id = 3 },
                Price = 15,
                Image = "ImageTest",
                Colour = "Purple",
                Brand = new() { Id = 5 },
                SKU = "1005242",
                Dimensions = new() { Width = 3, Height = 9, Length = 8 }
            };

            _productDbAccess.GetById(product.Id).Returns(toUpdate);

            _categoryDbAccess.GetById(product.Category.Id).Returns(product.Category);

            _brandDbAccess.GetById(product.Brand.Id).Returns(product.Brand);

            await _productService.Update(product);

            await _productDbAccess.Received(1).Update(toUpdate);

            Assert.Equal(toUpdate.Id, product.Id);
            Assert.Equal(toUpdate.Name, product.Name);
            Assert.Equal(toUpdate.Description, product.Description);
            Assert.Equal(toUpdate.Category.Id, product.Category.Id);
            Assert.Equal(toUpdate.Price, product.Price);
            Assert.Equal(toUpdate.Image, product.Image);
            Assert.Equal(toUpdate.Colour, product.Colour);
            Assert.Equal(toUpdate.Brand.Id, product.Brand.Id);
            Assert.Equal(toUpdate.SKU, product.SKU);
            Assert.Equal(toUpdate.Dimensions.Width, product.Dimensions.Width);
            Assert.Equal(toUpdate.Dimensions.Height, product.Dimensions.Height);
            Assert.Equal(toUpdate.Dimensions.Length, product.Dimensions.Length);
        }

        [Fact]
        public async Task When_Update_Product_Should_Product_Db_Access_Receive_One_Call()
        {
            Product product = new()
            {
                Id = 1,
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 },
                SKU = "14253678",
                Dimensions = new() { Width = 5, Height = 3, Length = 1 }
            };

            Product toUpdate = new()
            {
                Id = 1,
                Name = "Dress",
                Description = "Women Dress",
                Category = new() { Id = 3 },
                Price = 15,
                Image = "ImageTest",
                Colour = "Purple",
                Brand = new() { Id = 5 },
                SKU = "1005242",
                Dimensions = new() { Width = 3, Height = 9, Length = 8 }
            };

            _productDbAccess.GetById(product.Id).Returns(toUpdate);

            _categoryDbAccess.GetById(product.Category.Id).Returns(product.Category);

            _brandDbAccess.GetById(product.Brand.Id).Returns(product.Brand);

            await _productService.Update(product);

            await _productDbAccess.Received(1).Update(toUpdate);
        }

        [Fact]
        public async Task When_Delete_Product_Should_Product_Db_Access_Receive_One_Call()
        {
            Product product = new() { Id = 1, };

            await _productService.Delete(product.Id);

            await _productDbAccess.Received(1).Delete(product.Id);
        }

    }
}
