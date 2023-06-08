using ClothingStore.Models.OrderDetails;

namespace ClothingStore.Models.Orders
{
    public class GetOrderDTO
    {
        public int ID_Order { get; set; }
        public int ID_User { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOrder { get; set; }
        public int Status { get; set; }

        public List<GetOrderDetailDTO>? OrderDetailList { get; set; }
    }
}
