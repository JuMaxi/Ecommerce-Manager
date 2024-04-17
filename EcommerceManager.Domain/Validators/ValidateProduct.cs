using EcommerceManager.Domain.Interfaces;
using EcommerceManager.Domain.Models;
using EcommerceManager.Interfaces;

namespace EcommerceManager.Domain.Validators
{
    public class ValidateProduct : IValidateProduct
    {
        private readonly ICategoryDbAccess _categoryDbAccess;
        private readonly IBrandDbAccess _brandDbAccess;
        public ValidateProduct(ICategoryDbAccess categoryDbAccess, IBrandDbAccess brandDbAccess)
        {
            _categoryDbAccess = categoryDbAccess;
            _brandDbAccess = brandDbAccess;
        }
        public async Task Validate(Product product)
        {
            ValidateName(product.Name);
            ValidateDescription(product.Description);
            await ValidateCategory(product);
            ValidatePrice(product.Price);
            ValidateImage(product.Image);
            ValidateColour(product.Colour);
            await ValidateBrand(product);
            ValidateSKU(product.SKU);
            ValidateDimensions(product.Dimensions);
        }
        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("The field name can't be null, empty or just white spaces. Please fill this field do continue.");
            }
            if (name.Length > 100)
            {
                throw new Exception("The maximum characters for Name is 100. Please change it to continue.");
            }
        }
        private void ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new Exception("The field description can't be null, empty or just white spaces. Please fill this field to continue.");
            }
        }

        private async Task ValidateCategory(Product product)
        {
            if (product.Category.Id == 0)
            {
                throw new Exception("The field Id category can't be null or empty. Please fill this field to continue.");
            }
            if (await _categoryDbAccess.GetById(product.Category.Id) == null)
            {
                throw new Exception("The Id category doesn't exist into data base. Please fill a correct one.");
            }
        }

        private void ValidatePrice(decimal price)
        {
            if (price <= 0)
            {
                throw new Exception("The price must be filled with a number greater than zero. Please fill a correct one to continue.");
            }
        }

        private void ValidateImage(string image)
        {
            if (string.IsNullOrWhiteSpace(image))
            {
                throw new Exception("The field image can't be null, empty or just white spaces. Please fill this field to continue.");
            }
            if (image.Length > 400)
            {
                throw new Exception("The maximum characters for Image is 400. Please change it to continue.");
            }
        }

        private void ValidateColour(string colour)
        {
            if (string.IsNullOrWhiteSpace(colour))
            {
                throw new Exception("The field colour can't be null, empty or just white spaces. Please fill this field to continue.");
            }
            if (colour.Length > 20)
            {
                throw new Exception("The maximum characters for Colour is 20. Please change it to continue.");
            }
        }

        private async Task ValidateBrand(Product product)
        {
            if (product.Brand.Id == 0)
            {
                throw new Exception("The field Id brand can't be null or empty. Please fill this field to continue.");
            }

            if (await _brandDbAccess.GetById(product.Brand.Id) == null)
            {
                throw new Exception("The Id category doesn't exist into data base. Please fill a correct one.");
            }
        }

        private void ValidateSKU(string SKU)
        {
            if (string.IsNullOrWhiteSpace(SKU))
            {
                throw new Exception("The field SKU can't be null, empty or just white spaces. Please fill this field to continue.");
            }
            if (SKU.Length > 20)
            {
                throw new Exception("The maximum characters for SKU is 20. Please change it to continue.");
            }
        }

        private void ValidateDimensions(Dimensions dimensions)
        {
            if (dimensions.Width <= 0)
            {
                throw new Exception("The field width must be filled with a number greater than zero");
            }
            if (dimensions.Height <= 0)
            {
                throw new Exception("The field height must be filled with a number greater than zero.");
            }
            if (dimensions.Length <= 0)
            {
                throw new Exception("The field length must be filled with a number greater than zero.");
            }
        }
    }
}
