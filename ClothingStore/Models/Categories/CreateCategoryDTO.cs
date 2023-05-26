using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Categories
{
    public class CreateCategoryDTO
    {
        [Required]
        public string? NameCategory { get; set; }

    }
}
