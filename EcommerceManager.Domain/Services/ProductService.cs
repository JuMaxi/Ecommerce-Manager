using EcommerceManager.Domain.Interfaces;
using EcommerceManager.Domain.Models;
using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDbAccess _productDbAccess;
        private readonly ICategoryDbAccess _categoryDbAccess;
        private readonly IBrandDbAccess _brandDbAccess;
        private readonly IValidateProduct _validateProduct;
       
        public ProductService(IProductDbAccess productDbAccess, ICategoryDbAccess categoryDbAccess, IBrandDbAccess brandDbAccess, IValidateProduct validateProduct)
        {
            _productDbAccess = productDbAccess;
            _categoryDbAccess = categoryDbAccess;
            _brandDbAccess = brandDbAccess;
            _validateProduct = validateProduct;
        }

        public async Task Insert(Product product)
        {
            await _validateProduct.Validate(product);

            product.Category = await _categoryDbAccess.GetById(product.Category.Id);

            product.Brand = await _brandDbAccess.GetById(product.Brand.Id);

            await _productDbAccess.Insert(product);
        }

        public async Task<Product> GetById(int id)
        {
            return await _productDbAccess.GetById(id);
        }

        public async Task<List<Product>> GetAll(int limit, int page)
        {
            int skip = 0;

            if (limit < 0 || limit > 1000)
            {
                limit = 10;
            }

            if(page <= 0)
            {
                page = 1;
            }

            if (page > 1)
            {
                skip = limit * (page - 1);
            }

            return await _productDbAccess.GetAll(skip, limit);
        }

        public async Task Update(Product product)
        {
            await _validateProduct.Validate(product);

            Product toUpdate = await _productDbAccess.GetById(product.Id);

            toUpdate.Name = product.Name;
            toUpdate.Description = product.Description;

            Category category = await _categoryDbAccess.GetById(product.Category.Id);

            toUpdate.Category = category;
            toUpdate.Price = product.Price;
            toUpdate.Image = product.Image;
            toUpdate.Colour = product.Colour;

            Brand brand = await _brandDbAccess.GetById(product.Brand.Id);

            toUpdate.Brand = brand;
            toUpdate.SKU = product.SKU;

            Dimensions dimensions = new()
            {
                Width = product.Dimensions.Width,
                Height = product.Dimensions.Height,
                Length = product.Dimensions.Length
            };

            toUpdate.Dimensions = dimensions;

            await _productDbAccess.Update(toUpdate);
        }

        public async Task Delete(int id)
        {
            await _productDbAccess.Delete(id);
        }

        public async Task<int> GetCount()
        {
            return await _productDbAccess.GetCount();
        }
    }
}
