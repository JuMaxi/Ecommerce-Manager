using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Validators
{
    public class ValidateBrand : IValidateBrand
    {
        readonly IBrandDbAccess _brandDbAccess;

        public ValidateBrand(IBrandDbAccess brandDbAccess)
        {
            _brandDbAccess = brandDbAccess;
        }

        public async Task Validate(Brand brand)
        {
            ValidateName(brand.Name);
            await ValidateNameAlreadyExistsDataBase(brand);
            ValidateFoundationYear(brand.FoundationYear);
        }

        private static void ValidateName(string name)
        {
            if (name == null)
            {
                throw new Exception("The Name brand can't be null. Please, fill the name to continue.");
            }
            if (name == "")
            {
                throw new Exception("The name brand can't be empty. Please, fill the name to continue.");
            }
        }

        public async Task ValidateNameAlreadyExistsDataBase(Brand brand)
        {
            Brand brandDb = await _brandDbAccess.GetBrandFromDataBaseByName(brand.Name);

            if (brandDb is not null && brand.Id != brandDb.Id)
            {
                throw new Exception("The name " + brand.Name + " already exist into database, please verify to continue.");
            }
        }

        private static void ValidateFoundationYear(int year)
        {
            if (year == 0)
            {
                throw new Exception("The Foundation Year can't be zero. Please fill the year to continue.");
            }
            if(year < 0)
            {
                throw new Exception("The Foundation Year can't be less than zero. Please fill the year to continue");
            }

            int actualYear = DateTime.Now.Year; 

            if(year > actualYear)
            {
                throw new Exception("This year " + year + " is greater than the actual year " + actualYear + "." +
                    " Please fill the correct year to continue");
            }
        }
    }
}
