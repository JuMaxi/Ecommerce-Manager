using EcommerceManager.API.Models.Requests;
using EcommerceManager.API.Models.Responses;
using EcommerceManager.Domain.Models;

namespace EcommerceManager.API.Mappers
{
    public interface IProductMapper
    {
        public ProductResponse ConvertToResponse(Product product);
        public List<ProductResponse> ConvertToListResponse(List<Product> products);
        public Product ConvertToProduct(ProductRequest productRequest);

    }
}
