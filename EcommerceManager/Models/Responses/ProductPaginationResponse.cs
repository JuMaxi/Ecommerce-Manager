namespace EcommerceManager.API.Models.Responses
{
    public class ProductPaginationResponse
    {
        public int Count {  get; set; }
        public List<ProductResponse> Items { get; set; }
    }
}
