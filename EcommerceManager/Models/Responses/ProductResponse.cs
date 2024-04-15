using EcommerceManager.Models.DataBase;

namespace EcommerceManager.API.Models.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Colour { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string SKU { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
    }
}
