using ClothingStore.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Orders
{
    public class CartDTO
    {
        [Required(ErrorMessage = "Id sản phẩm không được để trống")]
        [Range(1, float.MaxValue, ErrorMessage = "Id không hợp lệ")]
        public int ID_Product { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, float.MaxValue, ErrorMessage = "Số lượng phải lơn hơn 0")]
        public int Quantity { get; set; }
    }
}
