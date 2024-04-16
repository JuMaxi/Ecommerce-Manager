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
            if(name == null || name.Length == 0)
            {
                throw new Exception("The field name can't be null or empty. Please fill this field do continue.");
            }
        }
        private void ValidateDescription(string description)
        {
            if(description == null ||  description.Length == 0)
            {
                throw new Exception("The field description can't be null or empty. Please fill this field to continue.");
            }
        }

        private async Task ValidateCategory(Product product)
        {
            if(product.Category.Id == 0)
            {
                throw new Exception("The field Id category can't be null or empty. Please fill this field to continue.");
            }
            if(product.Id > 0)
            {
                if (await _categoryDbAccess.GetById(product.Category.Id) == null)
                {
                    throw new Exception("The Id category doesn't exist into data base. Please fill a correct one.");
                }
            }
        }

        private void ValidatePrice(decimal price)
        {
            if(price == 0)
            {
                throw new Exception("The price can't be zero. Please fill a valid price to continue.");
            }
        }

        private void ValidateImage(string image)
        {
            if(image == null || image.Length == 0)
            {
                throw new Exception("The field image can't be null or empty. Please fill this field to continue.");
            }
            if(image.Length > 399)
            {
                throw new Exception("The field image can have until 400 characters. Please fill this field with the correct information");
            }
        }

        private void ValidateColour(string colour)
        {
            if(colour == null || colour.Length == 0)
            {
                throw new Exception("The field colour can't be null or empty. Please fill this field to continue");
            }
        }

        private async Task ValidateBrand(Product product)
        {
            if(product.Brand.Id == 0)
            {
                throw new Exception("The field Id brand can't be null or empty. Please fill this field to continue.");
            }

            if(product.Id > 0)
            {
                if (await _brandDbAccess.GetById(product.Brand.Id) == null)
                {
                    throw new Exception("The Id category doesn't exist into data base. Please fill a correct one.");
                }
            }
        }

        private void ValidateSKU(string SKU)
        {
            if(SKU ==  null || SKU.Length == 0)
            {
                throw new Exception("The field SKU can't be null or empty. Please fill this field to continue.");
            }
        }

        private void ValidateDimensions(Dimensions dimensions)
        {
            if(dimensions.Width <= 0)
            {
                throw new Exception("The field width must be filled with a number greater than zero");
            }
            if(dimensions.Height <= 0)
            {
                throw new Exception("The field height must be filled with a number greater than zero.");
            }
            if(dimensions.Length <= 0)
            {
                throw new Exception("The field length must be filled with a number greater than zero.");
            }
        }
    }
}
