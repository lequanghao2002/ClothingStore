using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Domain
{
    public class Product
    {
        [Key]
        public int ID_Product { get; set; }
        public string? NameProduct { get; set; }
        public string? ProductDetail { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }

        // navigation properties: one product has many image
        public int? ID_MI { get; set; }

        public List<Manage_Image>? Manage_Images { get; set; }

        // navigation properties: one product has many category
        public int? ID_Category { get; set; }
        public List<Category>? Categories{ get; set; }

        //navigation properties: one order has many product
        public Order? Order { get; set; }
    }
}
