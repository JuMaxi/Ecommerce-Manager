using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;
using EcommerceManager.Models.Responses;

namespace EcommerceManager.Interfaces
{
    public interface IBrandMapper
    {
        public Brand ConvertBrandRequestToBrand(BrandRequest brandRequest);
        public List<BrandResponse> ConvertBrandToBrandResponse(List<Brand> brands);
    }
}
