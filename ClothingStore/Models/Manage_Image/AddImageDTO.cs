using ClothingStore.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Manage_Image
{
    public class AddImageDTO
    {
        [Required]
        public string? ImageName { get; set; }

        [Required]
        public IFormFile? Image_url { get; set; }

    }
}
