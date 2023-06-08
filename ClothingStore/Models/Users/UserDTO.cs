using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Users
{
    public class UserDTO
    {
        public int ID_User { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public DateTime Birthday { get; set; }
        public string? Address { get; set; }
        public DateTime DateCreate { get; set; }
        public int ID_Authorize { get; set; }
    }

    public class GetUserAPDTO
    {
        public string? Username { get; set; }
        public DateTime Birthday { get; set; }
        public string? Address { get; set; }
    }
    
    public class UpdateUserDTO
    {
      
        public string? Password { get; set; }
     
        public string? Username { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        public string? Address { get; set; }
    }
    public class AddAuthorizeUserDTO
    {
        [Required]
        public int ID_Authorize { get; set; }
    }

    public class LoginUser
    {
        [Required]
        public string? Account { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
