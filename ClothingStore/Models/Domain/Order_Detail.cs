using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Domain
{
    public class Order_Detail
    {
        [Key]
        public int ID_Order_Detail { get; set; }
        public int ID_Order { get; set; }
        public string? UserName { get; set; }
        public string? NameProduct { get; set; }
        public string? ProductDetail { get; set; }
        public string? Image_url { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public float TotalPrice { get; set; }
        public DateTime DateOrder { get; set; }

        // navigation properties: one order has one order_detail
        public Order? Order { get; set; }
    }
}
