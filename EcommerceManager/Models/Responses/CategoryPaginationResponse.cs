namespace EcommerceManager.Models.Responses
{
    public class CategoryPaginationResponse
    {
        public int Count {  get; set; }
        public List<CategoryResponse> Items { get; set; }
    }
}

