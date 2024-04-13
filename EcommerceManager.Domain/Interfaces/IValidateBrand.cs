using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Interfaces
{
    public interface IValidateBrand
    {
        public Task Validate(Brand brand);
    }
}
