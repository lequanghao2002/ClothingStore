using ClothingStore.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Models.DTO
{
    public class AddImageDTO
    {
        public string? ImageName { get; set; }
        public IFormFile? Image_url { get; set; }

    }
}
