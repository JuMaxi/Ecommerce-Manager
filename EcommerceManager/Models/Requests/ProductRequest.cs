namespace EcommerceManager.API.Models.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Colour { get; set; }
        public int BrandId { get; set; }
        public string SKU { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
    }
}
