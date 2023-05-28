using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.DTO.User
{
    public class RegisterUserDTO
    {
        [Required]
        public string? Account { get; set; }
        [Required]
        public string? Password { get; set; }
        /*[Required]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat (DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true) ]
        public DateTime Birthday { get; set; }
        public string? Address { get; set; }*/
    }
}
