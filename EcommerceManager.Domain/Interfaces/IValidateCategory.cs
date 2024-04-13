using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Interfaces
{
    public interface IValidateCategory
    {
        public Task Validate(Category category);
    }
}
