using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;
using EcommerceManager.Models.Responses;

namespace EcommerceManager.Mappers
{
    public class BrandMapper : IBrandMapper
    {
        public Brand ConvertBrandRequestToBrand(BrandRequest brandRequest)
        {
            Brand brand = new Brand()
            {
                Name = brandRequest.Name,
                FoundationYear = brandRequest.FoundationYear
            };

            return brand;
        }

        public List<BrandResponse> ConvertBrandToBrandResponse(List<Brand> brands)
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
    }
}
