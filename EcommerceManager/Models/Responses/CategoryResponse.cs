using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Models.Responses
{
    public class CategoryResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ParentName { get; set; }
        public int ParentId { get; set; }
    }
}
