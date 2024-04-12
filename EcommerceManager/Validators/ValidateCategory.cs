using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Validators
{
    public class ValidateCategory : IValidateCategory
    {
        private readonly ICategoryDbAccess _categoryDbAccess;
        public ValidateCategory(ICategoryDbAccess categoryDbAccess)
        {
            _categoryDbAccess = categoryDbAccess;
        }

        public async Task Validate(Category category)
        {
            await ValidateName(category);
            await ValidateDescription(category);
            ValidateImage(category.Image);
            await ValidateParentExists(category);
        }

        private async Task ValidateName(Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                throw new Exception("The Name field must be filled to continue.");
            }

            if (category.Id == 0)
            {
                if (await _categoryDbAccess.GetByName(category.Name) != null)
                {
                    throw new Exception("This category Name " + category.Name + " is already registered. Change the name to continue.");
                }
            }
        }

        private async Task ValidateDescription(Category category)
        {
            if (string.IsNullOrEmpty(category.Description))
            {
                throw new Exception("The Description field must be filled to continue");
            }

            if (category.Id == 0)
            {
                if (await _categoryDbAccess.GetByDescription(category.Description) != null)
                {
                    throw new Exception("The category Description " + category.Description + " is already registered. Change the description to continue");
                }
            }
        }
        private static void ValidateImage(string image)
        {
            if (string.IsNullOrEmpty(image))
            {
                throw new Exception("The Image field must be filled to continue");
            }
        }

        private async Task ValidateParentExists(Category category)
        {
            if (category.Parent != null)
            {
                if (await _categoryDbAccess.GetById(category.Parent.Id) == null)
                {
                    throw new Exception("The parent Id " + category.Parent.Id + " doesn't exist. Please verify before continue.");
                }
            }
        }
    }
}
