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
        public async Task Insert(BrandRequest brandRequest)
        {
            await _brandService.Insert(_brandMapper.ConvertFromRequest(brandRequest));
        }

        [HttpGet]
        public async Task<List<BrandResponse>> GetAll()
        {
            return _brandMapper.ConvertToListResponse(await _brandService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<BrandResponse> GetById([FromRoute] int id)
        {
            return _brandMapper.ConvertToResponse(await _brandService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task Update([FromRoute] int id, [FromBody] BrandRequest brandRequest)
        {
            Brand brand = _brandMapper.ConvertFromRequest(brandRequest);
            brand.Id = id; 

            await _brandService.Update(brand);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _brandService.Delete(id);
        }
    }
}
