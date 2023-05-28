using ClothingStore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClothingStore.Models.DTO;
using ClothingStore.Models.Domain;

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Manage_ImageController : ControllerBase
    {
        private readonly AppDbContext? _dbContext;

        public Manage_ImageController (AppDbContext? dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> AddImage ([FromForm] AddImageDTO imageDTO)
        {
            if (imageDTO.Image_url == null || imageDTO.Image_url.Length == 0)
            {
                return BadRequest("Hình ảnh không tồn tại");
            }
            string image_url;
            using (var memorystream =  new MemoryStream())
            {
                await imageDTO.Image_url.CopyToAsync(memorystream);
                var imageBytes = memorystream.ToArray();    
                image_url = Convert.ToBase64String(imageBytes);
            }

            var image = new Manage_Image
            {
                ImageName = imageDTO.ImageName,
                Image_url = image_url,
            };

            _dbContext.Manage_Image.Add(image);
            _dbContext.SaveChanges();

            return Ok("Hình ảnh tải lên thành công");
        }
    }
}
