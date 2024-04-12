using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Models.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ParentName { get; set; }
        public string ParentId { get; set; }
    }
}
