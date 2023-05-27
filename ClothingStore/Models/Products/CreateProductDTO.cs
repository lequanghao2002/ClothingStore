using System.ComponentModel.DataAnnotations;


namespace ClothingStore.Models.Products
{
    public class CreateProductDTO
    {
        [Required]
        public string? NameProduct { get; set; }
        public string? ProductDetail { get; set; }

        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public float Price { get; set; }

        [Range(0, 100, ErrorMessage = "Giảm giá phải nằm trong khoảng 0% đến 100%")]
        public float Discount { get; set; }
        public int ID_MI { get; set; }

        [Required]
        public int ID_Category { get; set; }
    }

}
