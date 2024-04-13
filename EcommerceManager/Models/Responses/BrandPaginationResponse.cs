namespace EcommerceManager.Models.Responses
{
    public class BrandPaginationResponse
    {
        public int Count {  get; set; }
        public List<BrandResponse> Items { get; set; }
    }
}
