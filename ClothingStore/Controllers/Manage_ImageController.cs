﻿using ClothingStore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClothingStore.Models.Domain;
using ClothingStore.CustomActionFilter;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ClothingStore.Models.Manage_Image;
using System.ComponentModel.DataAnnotations;
using ClothingStore.Repositories.Manage_Images;
using Microsoft.AspNetCore.Authorization;

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Manage_ImageController : ControllerBase
    {
        private readonly AppDbContext? _dbContext;
        private readonly IManage_ImageRepository _imageRepository;

        public Manage_ImageController (AppDbContext? dbContext, IManage_ImageRepository imageRepository)
        {
            _dbContext = dbContext;
            _imageRepository = imageRepository;
        }

        [HttpGet ("Get-All-Image")]
        [AuthorizeRoles("Read", "Write", "Admin")]
        public async Task<IActionResult> GetAllImage()
        {
            try
            {
                var AllImage = await _imageRepository.GetAllImage();
                return Ok(AllImage);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Upload-Image")]
        [ValidateModel]
        [AuthorizeRoles("Write", "Admin")]
        public async Task<IActionResult> AddImage ([FromForm] AddImageDTO imageDTO)
        {
            try
            {
                if (imageDTO.Image_url == null || imageDTO.Image_url.Length == 0)
                {
                    return BadRequest("Hình ảnh không tồn tại");
                }
                var ImageAdd = await _imageRepository.AddImage(imageDTO);
                return Ok(ImageAdd);

            }
            catch
            {
                return BadRequest("Tải ảnh không thành công");
            }
           
        }

        [HttpPut ("Update-Image-By-Id")]
        [ValidateModel]
        [AuthorizeRoles("Write", "Admin")]
        public async Task<IActionResult> UpdateImage([Required]int id, [FromForm] ImageNoIdDTO imageNoIdDTO)
        {
            try
            {
                if (imageNoIdDTO.ImageName == null && imageNoIdDTO.Image_url == null)
                {
                    return Ok("Không có trường nào được cập nhật");
                }    
                var ImageUpdate = await _imageRepository.UpdateImage(id, imageNoIdDTO);
                if (ImageUpdate != null)
                {
                    return Ok(ImageUpdate);
                }
                return BadRequest($"Không tìm thấy hình ảnh có id: {id}");
            }
            catch
            {
                return BadRequest();
            }
           
        }

        [HttpDelete("Delete-Image-By-Id")]
        [AuthorizeRoles("Write", "Admin")]
        public async Task<IActionResult>? DeleteImage([Required]int id)
        {
            try
            {
                var ImageDelete = await _imageRepository.DeleteImage(id);
                if ( ImageDelete != null )
                {
                    return Ok(ImageDelete);
                }
                return BadRequest($"Không tìm thấy hình ảnh có id: {id}");
            }
            catch
            {
                return BadRequest() ;
            }
           
        }
    }
}
