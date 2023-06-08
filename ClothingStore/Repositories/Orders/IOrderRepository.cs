using ClothingStore.Models.Categories;
using ClothingStore.Models.OrderDetails;
using ClothingStore.Models.Orders;

namespace ClothingStore.Repositories.Orders
{
    public interface IOrderRepository
    {
        public Task<List<GetOrderDTO>> GetAll();
        public Task<GetOrderDTO> GetOrderById(int id);
        public Task<CreateOrderDTO> Order(CreateOrderDTO createOrderDTO);
        public Task<bool> Cancel(int id);
    }
}
