using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Models.Requests
{
    public class CategoryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int ParentId { get; set; }
    }
}
