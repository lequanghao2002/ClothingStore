using ClothingStore.Data;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Models.Domain;
using ClothingStore.Models.DTO;
using Microsoft.Build.Framework;
using System;
using System.IO;

namespace ClothingStore.Repositories
{
    public class Manage_ImageRepository : IManage_ImageRepository
    {
        private readonly AppDbContext? _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;
       
        public Manage_ImageRepository(AppDbContext? context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<ImageDTO>> GetAllImage()
        {
            var AllImage = _context.Manage_Image.AsQueryable();

            var ImageList = await AllImage.Select(image => new ImageDTO
            {
                ID_MI = image.ID_MI,
                ImageName = image.ImageName,
                Image_url = image.Image_url
            }).ToListAsync();
           
            return ImageList;
        }

        public async Task<AddImageDTO> AddImage(AddImageDTO imageDTO)
        {
            // Lưu trữ hình ảnh vào thư mục lưu trữ trên server
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath,"Images",imageDTO.Image_url.FileName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                imageDTO.Image_url.CopyTo(stream);
            }

            // Lưu trữ đường dẫn hình ảnh vào cơ sở dữ liệu
            var request = _contextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host.Value}/";
            var imageUrl = $"{baseUrl}/Images/{imageDTO.Image_url.FileName}";
           
            var image = new Manage_Image
            {
                ImageName = imageDTO.ImageName,
                Image_url = imageUrl,
            };

            await _context.Manage_Image.AddAsync(image);
            await _context.SaveChangesAsync();

            return imageDTO;
        }

        public async Task<ImageNoIdDTO> UpdateImage(int id, ImageNoIdDTO imageNoIdDTO)
        {
            var ImageDomain = _context.Manage_Image?.FirstOrDefault(i => i.ID_MI == id);
            if (ImageDomain != null)
            {
                if (imageNoIdDTO.ImageName == null && imageNoIdDTO.Image_url != null)
                {
                    // Lưu trữ hình ảnh vào thư mục lưu trữ trên server
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imageNoIdDTO.Image_url.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        imageNoIdDTO.Image_url.CopyTo(stream);
                    }
                    var request = _contextAccessor.HttpContext.Request;
                    var baseUrl = $"{request.Scheme}://{request.Host.Value}/";
                    var imageUrl = $"{baseUrl}/Images/{imageNoIdDTO.Image_url.FileName}";

                    // Lưu trữ đường dẫn hình ảnh vào cơ sở dữ liệu
                    ImageDomain.Image_url = imageUrl;
                    await _context.SaveChangesAsync();
                }    
                else if (imageNoIdDTO.Image_url == null)
                {
                    ImageDomain.ImageName = imageNoIdDTO.ImageName;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imageNoIdDTO.Image_url.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        imageNoIdDTO.Image_url.CopyTo(stream);
                    }

                    // Lưu trữ đường dẫn hình ảnh vào cơ sở dữ liệu
                    var request = _contextAccessor.HttpContext.Request;
                    var baseUrl = $"{request.Scheme}://{request.Host.Value}/";
                    var imageUrl = $"{baseUrl}/Images/{imageNoIdDTO.Image_url.FileName}";

                    ImageDomain.ImageName = imageNoIdDTO.ImageName;
                    ImageDomain.Image_url = imageUrl;
                    await _context.SaveChangesAsync();
                }         
            }
            return imageNoIdDTO;
        }

        public async Task<Manage_Image>? DeleteImage(int id )
        {
            var ImageDomain = _context.Manage_Image!.SingleOrDefault( i => i.ID_MI == id); 
            if (ImageDomain != null)
            {
                _context.Manage_Image.Remove(ImageDomain);
                await _context.SaveChangesAsync();
            }
            else return null!;
            return ImageDomain;
        }
    }
}
