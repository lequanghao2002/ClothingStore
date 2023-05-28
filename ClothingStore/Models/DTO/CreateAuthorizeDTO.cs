using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.DTO
{
    public class CreateAuthorizeDTO
    {
        [Required (ErrorMessage = "Vui lòng nhập quyền")]
        public string? Authorize { get; set; }
    }
}
