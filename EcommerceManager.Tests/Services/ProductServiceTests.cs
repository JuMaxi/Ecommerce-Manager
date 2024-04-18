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

    }
}
