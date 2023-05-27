using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Products
{
    public class GetProductDTO
    {
        public int ID_Product { get; set; }
        public string? NameProduct { get; set; }
        public string? ProductDetail { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; } = 0;
        public int ID_MI { get; set; }
        public int ID_Category { get; set; }
    }
}
