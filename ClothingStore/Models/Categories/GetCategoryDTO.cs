using Microsoft.Build.Framework;

namespace ClothingStore.Models.Categories
{
    public class GetCategoryDTO
    {
        public int ID_Category { get; set; }
        public string? NameCategory { get; set; }
    }
}
