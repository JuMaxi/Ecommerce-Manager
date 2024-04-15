using EcommerceManager.API.Mappers;
using EcommerceManager.API.Models.Requests;
using EcommerceManager.API.Models.Responses;
using EcommerceManager.Domain.Models;
using FluentAssertions;

namespace EcommerceManager.Tests.MappersTests
{
    public class ProductMapperTests
    {
        [Fact]
        public void When_Converting_Product_It_Should_Be_Equal_Product_Response()
        {
            Product product = new()
            {
                Id = 4,
                Name = "Trouser",
                Description = "Women Trouser",
                Category = new() { Id = 2, Name = "Clothing"},
                Price = 35.5m,
                Image = "https://trouser.com/pants/women",
                Colour = "Green",
                Brand = new() { Id = 3, Name = "Guess"},
                SKU = "25367891",
                Dimensions = new()
                {
                    Width = 10.5,
                    Height = 12.3,
                    Length = 15.1
                }
            };

            ProductMapper mapper = new();

            ProductResponse response = mapper.ConvertToResponse(product);

            response.Id.Should().Be(product.Id);
            response.Name.Should().Be(product.Name);
            response.Description.Should().Be(product.Description);
            response.CategoryId.Should().Be(product.Category.Id);
            response.CategoryName.Should().Be(product.Category.Name);
            response.Price.Should().Be(product.Price);
            response.Image.Should().Be(product.Image);
            response.Colour.Should().Be(product.Colour);
            response.BrandId.Should().Be(product.Brand.Id);
            response.BrandName.Should().Be(product.Brand.Name);
            response.SKU.Should().Be(product.SKU);
            response.Width.Should().Be(product.Dimensions.Width);
            response.Height.Should().Be(product.Dimensions.Height);
            response.Length.Should().Be(product.Dimensions.Length);
        }

        [Fact]
        public void When_Converting_List_Product_It_Should_Be_Equal_List_Response()
        {
            Product product = new()
            {
                Id = 4,
                Name = "Trouser",
                Description = "Women Trouser",
                Category = new() { Id = 2, Name = "Clothing" },
                Price = 35.5m,
                Image = "https://trouser.com/pants/women",
                Colour = "Green",
                Brand = new() { Id = 3, Name = "Guess" },
                SKU = "25367891",
                Dimensions = new()
                {
                    Width = 10.5,
                    Height = 12.3,
                    Length = 15.1
                }
            };

            List<Product> listProduct = new() { product };

            ProductMapper mapper = new();

            List<ProductResponse> listResponse = mapper.ConvertToListResponse(listProduct);

            listResponse[0].Id.Should().Be(product.Id);
            listResponse[0].Name.Should().Be(product.Name);
            listResponse[0].Description.Should().Be(product.Description);
            listResponse[0].CategoryId.Should().Be(product.Category.Id);
            listResponse[0].CategoryName.Should().Be(product.Category.Name);
            listResponse[0].Price.Should().Be(product.Price);
            listResponse[0].Image.Should().Be(product.Image);
            listResponse[0].Colour.Should().Be(product.Colour);
            listResponse[0].BrandId.Should().Be(product.Brand.Id);
            listResponse[0].BrandName.Should().Be(product.Brand.Name);
            listResponse[0].SKU.Should().Be(product.SKU);
            listResponse[0].Width.Should().Be(product.Dimensions.Width);
            listResponse[0].Height.Should().Be(product.Dimensions.Height);
            listResponse[0].Length.Should().Be(product.Dimensions.Length);
        }

        [Fact]
        public void When_Converting_Product_Request_It_Should_Be_Equal_Product()
        {
            ProductRequest productRequest = new()
            {
                Name = "Trouser",
                Description = "Women Trouser",
                CategoryId = 3,
                Price = 10.5m,
                Image = "https://trouser.com/pants/women",
                Colour = "Green",
                BrandId = 5,
                SKU = "45263897",
                Width = 32,
                Height = 33,
                Length = 35
            };

            ProductMapper mapper = new();

            Product product = mapper.ConvertToProduct(productRequest);

            product.Name.Should().Be(productRequest.Name);
            product.Description.Should().Be(productRequest.Description);
            product.Category.Id.Should().Be(productRequest.CategoryId);
            product.Price.Should().Be(productRequest.Price);
            product.Image.Should().Be(productRequest.Image);
            product.Colour.Should().Be(productRequest.Colour);
            product.Brand.Id.Should().Be(productRequest.BrandId);
            product.SKU.Should().Be(productRequest.SKU);
            product.Dimensions.Width.Should().Be(productRequest.Width);
            product.Dimensions.Height.Should().Be(productRequest.Height);
            product.Dimensions.Length.Should().Be(productRequest.Length);
        }
    }
}
