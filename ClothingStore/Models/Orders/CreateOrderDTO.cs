using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Orders
{
    public class CreateOrderDTO
    {
        [Required]
        public int ID_User { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Điện thoại không được để trống")]
        [MaxLength(10, ErrorMessage = "Số điện thoại không hợp lệ")]
        [MinLength(10, ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Điền sản phẩm cần mua")]
        public List<CartDTO>? CartList { get; set; }
    }
}
