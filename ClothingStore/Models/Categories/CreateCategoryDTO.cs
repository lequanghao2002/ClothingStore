using Microsoft.Build.Framework;

namespace ClothingStore.Models.Categories
{
    public class CreateCategoryDTO
    {
        [Required]
        public string? NameCategory { get; set; }

    }
}
