using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public string Image {  get; set; }
        public string Colour {  get; set; }
        public Brand Brand { get; set; }
        public string SKU {  get; set; }
        public Dimensions Dimensions {  get; set; }
    }
}
