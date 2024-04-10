using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;
using EcommerceManager.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceManager.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BrandController
    {
        readonly IBrandService _brandService;
        readonly IBrandMapper _brandMapper;

        public BrandController(IBrandService brandService, IBrandMapper brandMapper)
        {
            _brandService = brandService;
            _brandMapper = brandMapper;
        }

        [HttpPost]
        public async Task AddNewBrand(BrandRequest brandRequest)
        {
            await _brandService.InsertNewBrand(_brandMapper.ConvertBrandRequestToBrand(brandRequest));
        }

        [HttpGet]
        public async Task<List<BrandResponse>> GetBrandsFromDataBase()
        {
            return _brandMapper.ConvertBrandToBrandResponse(await _brandService.GetBrandsFromDataBase());
        }

        [HttpPut("{id}")]
        public async Task UpdateBrand([FromRoute] int id, [FromBody] BrandRequest brandRequest)
        {
            Brand brand = _brandMapper.ConvertBrandRequestToBrand(brandRequest);
            brand.Id = id; 

            await _brandService.UpdateBrand(brand);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCategory([FromRoute] int id)
        {
            await _brandService.DeleteBrand(id);
        }
    }
}
