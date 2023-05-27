﻿using ClothingStore.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.DTO
{
    public class AddImageDTO
    {
        [Required]
        public string? ImageName { get; set; }

        [Required]
        public IFormFile? Image_url { get; set; }

    }
}
