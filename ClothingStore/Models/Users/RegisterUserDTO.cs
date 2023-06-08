using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Users
{
    public class RegisterUserDTO
    {
        [Required]
        public string? Account { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
