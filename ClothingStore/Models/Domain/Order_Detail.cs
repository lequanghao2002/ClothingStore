using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Domain
{
    public class Order_Detail
    {
        [Key]
        public int ID_Order_Detail { get; set; }
        public int ID_Order { get; set; }
        public int ID_Product { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }

        // navigation properties: one order has one order_detail
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
