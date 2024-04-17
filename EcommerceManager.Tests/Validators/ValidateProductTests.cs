using EcommerceManager.Domain.Models;
using EcommerceManager.Domain.Validators;
using EcommerceManager.Interfaces;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace EcommerceManager.Tests.Validators
{
    public class ValidateProductTests
    {
        private ICategoryDbAccess _categoryDbAccessFake;
        private IBrandDbAccess _brandDbAccessFake;
        private ValidateProduct validator;

        public ValidateProductTests() 
        { 
            _categoryDbAccessFake = Substitute.For<ICategoryDbAccess>();
            _brandDbAccessFake = Substitute.For<IBrandDbAccess>();
            validator = new ValidateProduct(_categoryDbAccessFake, _brandDbAccessFake);
        }


        [Fact]
        public async Task When_Name_Is_Null_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = null
            };

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field name can't be null, empty or just white spaces. Please fill this field do continue.");
        }

        [Fact]
        public async Task When_Name_Is_Empty_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = ""
            };

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field name can't be null, empty or just white spaces. Please fill this field do continue.");
        }

        [Fact]
        public async Task When_Name_Is_White_Space_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = " "
            };

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field name can't be null, empty or just white spaces. Please fill this field do continue.");
        }

        [Fact]
        public async Task When_Name_Length_Is_Greater_Than_100_Should_Trown_Exception()
        {
            Product product = new()
            {
                Name = new string('n',101)
            };

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The maximum characters for Name is 100. Please change it to continue.");
        }

        [Fact]
        public async Task When_Description_Is_Null_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = null
            };

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field description can't be null, empty or just white spaces. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_Description_Is_Empty_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = ""
            };

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field description can't be null, empty or just white spaces. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_Description_Is_White_Space_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = " "
            };

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field description can't be null, empty or just white spaces. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_Category_Id_Is_Zero_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 0 }
            };

            await validator.Invoking(validator => validator.Validate(product))
            .Should().ThrowAsync<Exception>()
                .WithMessage("The field Id category can't be null or empty. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_Category_Is_Null_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 10}
            };

            _categoryDbAccessFake.GetById(product.Category.Id).ReturnsNull();

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The Id category doesn't exist into data base. Please fill a correct one.");
        }

        [Fact]
        public async Task When_Price_Is_Equal_Zero_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 0
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The price must be filled with a number greater than zero. Please fill a correct one to continue.");
        }

        [Fact]
        public async Task When_Price_Is_Less_Than_Zero_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = -5
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The price must be filled with a number greater than zero. Please fill a correct one to continue.");
        }

        [Fact]
        public async Task When_Image_Is_Null_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = null
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field image can't be null, empty or just white spaces. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_Image_Is_Empty_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = ""
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field image can't be null, empty or just white spaces. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_Image_Is_White_Spaces_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = " "
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field image can't be null, empty or just white spaces. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_Image_Length_Is_Greater_Than_400_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = new string('i',401)
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The maximum characters for Image is 400. Please change it to continue.");
        }

        [Fact]
        public async Task When_Colour_Is_Null_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = null
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field colour can't be null, empty or just white spaces. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_Colour_Is_Empty_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = ""
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field colour can't be null, empty or just white spaces. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_Colour_Is_White_Spaces_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = " "
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field colour can't be null, empty or just white spaces. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_Brand_Id_Is_Zero_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 0}
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field Id brand can't be null or empty. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_Brand_Is_Null_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 }
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);
            _brandDbAccessFake.GetById(product.Brand.Id).ReturnsNull();

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The Id category doesn't exist into data base. Please fill a correct one.");
        }

        [Fact]
        public async Task When_SKU_Is_Null_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 },
                SKU = null
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);
            _brandDbAccessFake.GetById(product.Brand.Id).Returns(product.Brand);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field SKU can't be null, empty or just white spaces. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_SKU_Is_Empty_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 },
                SKU = ""
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);
            _brandDbAccessFake.GetById(product.Brand.Id).Returns(product.Brand);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field SKU can't be null, empty or just white spaces. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_SKU_Is_White_Spaces_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 },
                SKU = " "
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);
            _brandDbAccessFake.GetById(product.Brand.Id).Returns(product.Brand);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field SKU can't be null, empty or just white spaces. Please fill this field to continue.");
        }

        [Fact]
        public async Task When_SKU_Length_Is_Greater_Than_20_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 },
                SKU = new string('s',21)
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);
            _brandDbAccessFake.GetById(product.Brand.Id).Returns(product.Brand);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The maximum characters for SKU is 20. Please change it to continue.");
        }

        [Fact]
        public async Task When_Dimension_Width_Is_Less_Than_Zero_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 },
                SKU = "14253678",
                Dimensions = new() { Width = -3}
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);
            _brandDbAccessFake.GetById(product.Brand.Id).Returns(product.Brand);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field width must be filled with a number greater than zero");
        }

        [Fact]
        public async Task When_Dimension_Width_Is_Zero_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 },
                SKU = "14253678",
                Dimensions = new() { Width = 0 }
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);
            _brandDbAccessFake.GetById(product.Brand.Id).Returns(product.Brand);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field width must be filled with a number greater than zero");
        }

        [Fact]
        public async Task When_Dimension_Height_Is_Less_Than_Zero_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 },
                SKU = "14253678",
                Dimensions = new() { Width = 5, Height = -1 }
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);
            _brandDbAccessFake.GetById(product.Brand.Id).Returns(product.Brand);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field height must be filled with a number greater than zero.");
        }

        [Fact]
        public async Task When_Dimension_Height_Is_Zero_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 },
                SKU = "14253678",
                Dimensions = new() { Width = 5, Height = 0 }
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);
            _brandDbAccessFake.GetById(product.Brand.Id).Returns(product.Brand);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field height must be filled with a number greater than zero.");
        }

        [Fact]
        public async Task When_Dimension_Length_Is_Less_Than_Zero_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 },
                SKU = "14253678",
                Dimensions = new() { Width = 5, Height = 3, Length = -1 }
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);
            _brandDbAccessFake.GetById(product.Brand.Id).Returns(product.Brand);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field length must be filled with a number greater than zero.");
        }

        [Fact]
        public async Task When_Dimension_Length_Is_Zero_Should_Throw_Exception()
        {
            Product product = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Category = new() { Id = 1 },
                Price = 10,
                Image = "ImageTest",
                Colour = "Green",
                Brand = new() { Id = 10 },
                SKU = "14253678",
                Dimensions = new() { Width = 5, Height = 3, Length = 0 }
            };

            _categoryDbAccessFake.GetById(product.Category.Id).Returns(product.Category);
            _brandDbAccessFake.GetById(product.Brand.Id).Returns(product.Brand);

            await validator.Invoking(validator => validator.Validate(product))
                .Should().ThrowAsync<Exception>()
                .WithMessage("The field length must be filled with a number greater than zero.");
        }
    }
}
