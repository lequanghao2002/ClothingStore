using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Domain
{
    public class Order
    {
        [Key]
        public int ID_Order { get; set; }
        public int ID_Product { get; set; }
        public int ID_User { get; set; }
        public int Quantity { get; set; }

        //navigation properties: one order has many product
        public Product? Products { get; set; }

        //navigation properties: one user has many order
        public User? User { get; set; }

        //navigation properties: one order has one order detail
        public List<Order_Detail>? Order_Details { get; set; }



    }
}
