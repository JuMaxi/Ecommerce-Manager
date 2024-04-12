using EcommerceManager.Mappers;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;
using EcommerceManager.Models.Responses;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceManager.Tests.MappersTests
{
    public class BrandMapperTests
    {
        [Fact]
        public void When_Converting_BrandRequest_It_Should_Be_Equal_Brand()
        {
            BrandRequest brandRequest = new()
            {
                Name = "Guess",
                FoundationYear = 1999
            };

            BrandMapper brandMapper = new();

            Brand brand = brandMapper.ConvertFromRequest(brandRequest);

            brand.Name.Should().Be(brandRequest.Name);
            brand.FoundationYear.Should().Be(brandRequest.FoundationYear);
        }

        [Fact]
        public void When_Converting_ListBrand_It_Should_Be_Equal_ListBrandResponse()
        {
            Brand brand = new()
            {
                Id = 1,
                Name = "Forum",
                FoundationYear = 1950
            };
            List<Brand> listBrand = new() { brand };

            BrandMapper brandMapper = new();

            List<BrandResponse> brandResponse = brandMapper.ConvertToListResponse(listBrand);

            brandResponse[0].Id.Should().Be(brand.Id);
            brandResponse[0].Name.Should().Be(listBrand[0].Name);
            brandResponse[0].FoundationYear.Should().Be(listBrand[0].FoundationYear);
        }

        [Fact]
        public void When_Converting_Brand_It_Should_Be_Equal_BrandResponse()
        {
            Brand brand = new()
            {
                Id = 10,
                Name = "Guess",
                FoundationYear = 1995
            };

            BrandMapper brandMapper = new();

            BrandResponse brandResponse = brandMapper.ConvertToResponse(brand);

            brandResponse.Id.Should().Be(brand.Id);
            brandResponse.Name.Should().Be(brand.Name);
            brandResponse.FoundationYear.Should().Be(brand.FoundationYear);
        }
    }
}
