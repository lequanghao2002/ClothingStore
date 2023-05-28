using ClothingStore.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.DTO
{
    public class ImageDTO
    {
        public int ID_MI { get; set; }
        public string? ImageName { get; set; }
        public string? Image_url { get; set; }
    }

    public class ImageNoIdDTO
    {
      
        public string? ImageName { get; set; }
       
        public IFormFile? Image_url { get; set; }
    }
}
