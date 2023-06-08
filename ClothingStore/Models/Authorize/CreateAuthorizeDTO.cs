using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Authorize
{
    public class CreateAuthorizeDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập quyền")]
        public string? Authorize { get; set; }
    }
}
