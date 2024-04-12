using EcommerceManager.Mappers;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;
using EcommerceManager.Models.Responses;
using FluentAssertions;

namespace EcommerceManager.Tests.MappersTests
{
    public class CategoryMapperTests
    {
        [Fact]
        public void Checking_If_CategoryRequest_is_Equal_Category()
        {
            CategoryRequest categoryRequest = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Image = "https:myclothes.co.uk/Trousers-Tailored-Business-Straight-Everpress",
                ParentId = 3,
            };

            CategoryMapper categoryMapper = new();
            Category category = categoryMapper.ConvertFromRequest(categoryRequest);

            category.Name.Should().Be(categoryRequest.Name);
            category.Description.Should().Be(categoryRequest.Description);
            category.Image.Should().Be(categoryRequest.Image);
            category.Parent.Id.Should().Be(categoryRequest.ParentId);
        }

        [Fact]
        public void Checking_If_Category_is_Equal_CategoryResponse()
        {
            Category category = new()
            {
                Name = "Trousers",
                Description = "Women Trousers",
                Image = "https:myclothes.co.uk/Trousers-Tailored-Business-Straight-Everpress",
                Parent = new()
                {
                    Id = 3,
                    Name = "Trousers"
                }
            };

            List<Category> categories = new() { { category } };

            CategoryMapper categoryMapper = new();
            List<CategoryResponse> listCategoriesResponse = categoryMapper.ConvertToListResponse(categories);

            listCategoriesResponse[0].Name.Should().Be(category.Name);
            listCategoriesResponse[0].Description.Should().Be(category.Description);
            listCategoriesResponse[0].Image.Should().Be(category.Image);
            listCategoriesResponse[0].ParentName.Should().Be(category.Parent.Name);
            listCategoriesResponse[0].ParentId.Should().Be(category.Parent.Id.ToString());
        }

        [Fact]
        public void When_Category_Request_Parent_Is_Zero_Category_Parent_Should_Be_Null()
        {
            CategoryRequest categoryRequest = new()
            {
                ParentId = 0
            };

            CategoryMapper categoryMapper = new();

            Category category = categoryMapper.ConvertFromRequest(categoryRequest);

            category.Parent.Should().BeNull();
        }

        [Fact]
        public void When_Category_Parent_Is_Not_Null_Category_Response_Parent_Should_Be_Equal_Category_Parent()
        {
            Category category = new()
            {
                Parent = new() { Id = 10, Name = "ParentTest" },
            };

            List<Category> listCategories = new() { category };
            
            CategoryMapper mapper = new();

            List<CategoryResponse> listCategoriesResponse = mapper.ConvertToListResponse(listCategories);

            listCategoriesResponse[0].ParentId.Should().Be(category.Parent.Id.ToString());
            listCategoriesResponse[0].ParentName.Should().Be(category.Parent.Name);
        }

        [Fact]
        public void When_Converting_Category_It_Should_Be_Equal_Category_Response()
        {
            Category category = new()
            {
                Id = 5,
                Name = "Trousers",
                Description = "Green Trousers",
                Image = "www.trousersgreen.com",
                Parent = new()
                {
                    Id = 2,
                    Name = "Clothes"
                }
            };

            CategoryMapper mapper = new();

            CategoryResponse response = mapper.ConvertToResponse(category);

            response.Id.Should().Be(category.Id);
            response.Name.Should().Be(category.Name);
            response.Description.Should().Be(category.Description);
            response.Image.Should().Be(category.Image);
            response.ParentId.Should().Be(category.Parent.Id.ToString());
            response.ParentName.Should().Be(category.Parent.Name);
        }
    }
}
