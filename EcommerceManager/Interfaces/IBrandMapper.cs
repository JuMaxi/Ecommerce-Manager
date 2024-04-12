using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;
using EcommerceManager.Models.Responses;

namespace EcommerceManager.Interfaces
{
    public interface IBrandMapper
    {
        public Brand ConvertFromRequest(BrandRequest brandRequest);
        public List<BrandResponse> ConvertToListResponse(List<Brand> brands);
        public BrandResponse ConvertToResponse(Brand brand);
    }
}
