using EcommerceManager.API.Models.Requests;
using EcommerceManager.API.Models.Responses;
using EcommerceManager.Domain.Models;

namespace EcommerceManager.API.Mappers
{
    public class ProductMapper : IProductMapper
    {
        public ProductResponse ConvertToResponse(Product product)
        {
            ProductResponse response = new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.Category.Id,
                CategoryName = product.Category.Name,
                Price = product.Price,
                Image = product.Image,
                Colour = product.Colour,
                BrandId = product.Brand.Id,
                BrandName = product.Brand.Name,
                SKU = product.SKU,
                Width = product.Dimensions.Width,
                Height = product.Dimensions.Height,
                Length = product.Dimensions.Length,
            };

            return response;
        }

        public List<ProductResponse> ConvertToListResponse(List<Product> products)
        {
            List<ProductResponse> listResponse = new();

            foreach (Product product in products)
            {
                ProductResponse p = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CategoryId = product.Category.Id,
                    CategoryName = product.Category.Name,
                    Price = product.Price,
                    Image = product.Image,
                    Colour = product.Colour,
                    BrandId = product.Brand.Id,
                    BrandName = product.Brand.Name,
                    SKU = product.SKU,
                    Width = product.Dimensions.Width,
                    Height = product.Dimensions.Height,
                    Length = product.Dimensions.Length,
                };

                listResponse.Add(p);
            }

            return listResponse;
        }
        
        public Product ConvertToProduct(ProductRequest productRequest)
        {
            Product product = new()
            {
                Name = productRequest.Name,
                Description = productRequest.Description,
                Category = new() { Id = productRequest.CategoryId},
                Price = productRequest.Price,
                Image = productRequest.Image,
                Colour = productRequest.Colour,
                Brand = new() { Id = productRequest.BrandId},
                SKU = productRequest.SKU,
                Dimensions = new()
                {
                    Width = productRequest.Width,
                    Height = productRequest.Height,
                    Length = productRequest.Length
                }
            };

            return product;
        }
    }
}
