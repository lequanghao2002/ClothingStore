using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Domain
{
    public class Order
    {
        [Key]
        public int ID_Order { get; set; }
        public int ID_User { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOrder { get; set; }
        public int Status { get; set; }

        //navigation properties: one order has one order detail
        public List<Order_Detail>? Order_Details { get; set; }

        public User? user {get; set; }

    }
}
