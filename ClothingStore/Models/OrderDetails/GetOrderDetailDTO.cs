namespace ClothingStore.Models.OrderDetails
{
    public class GetOrderDetailDTO
    {
        public int ID_Order_Detail { get; set; }
        public int ID_Product { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float PriceTotal { get; set; }
    }
}
