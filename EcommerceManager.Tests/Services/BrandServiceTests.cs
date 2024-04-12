using EcommerceManager.DbAccess;
using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Services;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace EcommerceManager.Tests.Services
{
    public class BrandServiceTests
    {
        private IBrandDbAccess dbAccessFake;
        private IValidateBrand validatorFake;
        private BrandService brandService;

        public BrandServiceTests()
        {
            dbAccessFake = Substitute.For<IBrandDbAccess>();
            validatorFake = Substitute.For<IValidateBrand>();

            brandService = new BrandService(dbAccessFake, validatorFake);
        }

        [Fact]
        public async Task When_Insert_Brand_Should_Save_On_Data_Base()
        {
            Brand brand = new()
            {
                Name = "Guess",
                FoundationYear = 1999
            };

            await brandService.Insert(brand);

            await dbAccessFake.Received(1).Insert(brand);
        }

        [Fact]
        public async Task When_Update_Brand_Should_Be_Equal_Received_Data()
        {
            Brand toUpdate = new()
            {
                Id = 1,
                Name = "Guess",
                FoundationYear = 1990
            };

            Brand updated = new()
            {
                Id = 1,
                Name = "Forum",
                FoundationYear = 1993
            };

            dbAccessFake.GetById(toUpdate.Id).Returns(updated);

            await brandService.Update(toUpdate);

            await dbAccessFake.Received(1).Update(updated);

            Assert.Equal(toUpdate.Id, updated.Id);
            Assert.Equal(toUpdate.Name, updated.Name);
            Assert.Equal(toUpdate.FoundationYear, updated.FoundationYear);
        }
    }
}
