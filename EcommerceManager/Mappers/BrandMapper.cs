using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;
using EcommerceManager.Models.Responses;

namespace EcommerceManager.Mappers
{
    public class BrandMapper : IBrandMapper
    {
        public Brand ConvertFromRequest(BrandRequest brandRequest)
        {
            Brand brand = new Brand()
            {
                Name = brandRequest.Name,
                FoundationYear = brandRequest.FoundationYear
            };

            return brand;
        }

        public List<BrandResponse> ConvertToListResponse(List<Brand> brands)
        {
            List<BrandResponse> brandsResponse = new();
            foreach(Brand b in brands)
            {
                BrandResponse response = new()
                {
                    Id = b.Id,
                    Name = b.Name,
                    FoundationYear = b.FoundationYear
                };
                brandsResponse.Add(response);
            }
            return brandsResponse;
        }

        public BrandResponse ConvertToResponse(Brand brand)
        {
            BrandResponse response = new()
            {
                Id = brand.Id,
                Name = brand.Name,
                FoundationYear = brand.FoundationYear
            };

            return response;
        }
    }
}
