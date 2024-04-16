using EcommerceManager.Domain.Models;

namespace EcommerceManager.Domain.Interfaces
{
    public interface IValidateProduct
    {
        public Task Validate(Product product);
    }
}
