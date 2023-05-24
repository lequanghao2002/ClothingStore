using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Domain
{
    public class Category
    {
        [Key]
        public int ID_Category { get; set; }
        public string? NameCategory { get; set; }

        // navigation properties: one product has many category
        public Product? Product { get; set; }
    }
}
