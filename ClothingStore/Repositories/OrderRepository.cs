using ClothingStore.Data;
using ClothingStore.Models.Domain;
using ClothingStore.Models.OrderDetails;
using ClothingStore.Models.Orders;
using ClothingStore.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<GetOrderDTO>> GetAll()
        {
            var orderDomainList = await _appDbContext.Order.Select(order => new GetOrderDTO
            {
                ID_Order = order.ID_Order,
                ID_User = order.ID_User,
                UserName = order.UserName,
                Address = order.Address,
                PhoneNumber = order.PhoneNumber,
                DateOrder = order.DateOrder,
                Status = order.Status,
                OrderDetailList = order.Order_Details.Select(orderDetail => new GetOrderDetailDTO
                {
                    ID_Order_Detail = orderDetail.ID_Order_Detail,
                    ID_Product = orderDetail.ID_Product,
                    Price = orderDetail.Price,
                    Quantity = orderDetail.Quantity,
                    PriceTotal = orderDetail.Price * orderDetail.Quantity,
                }).ToList()
            }).ToListAsync();

            return orderDomainList;
        }

        public async Task<GetOrderDTO> GetOrderById(int id)
        {
            var orderById = await _appDbContext.Order.SingleOrDefaultAsync(m => m.ID_Order == id);
            var orderDetailById = _appDbContext.Order_Detail.Where(m => m.ID_Order == orderById.ID_Order);
            var orderDomain = new GetOrderDTO
            {
                ID_Order = orderById.ID_Order,
                ID_User = orderById.ID_User,
                UserName = orderById.UserName,
                Address = orderById.Address,
                PhoneNumber = orderById.PhoneNumber,
                DateOrder = orderById.DateOrder,
                Status = orderById.Status,
                OrderDetailList = orderDetailById.Select(orderDetail => new GetOrderDetailDTO
                {
                    ID_Order_Detail = orderDetail.ID_Order_Detail,
                    ID_Product = orderDetail.ID_Product,
                    Price = orderDetail.Price,
                    Quantity = orderDetail.Quantity,
                    PriceTotal = orderDetail.Price * orderDetail.Quantity,
                }).ToList()
            };
            return orderDomain;
        }

        public async Task<CreateOrderDTO> Order(CreateOrderDTO createOrderDTO)
        {
            var OrderDomain = new Order
            {
                ID_User = createOrderDTO.ID_User,
                UserName = createOrderDTO.UserName,
                Address = createOrderDTO.Address,
                PhoneNumber = createOrderDTO.PhoneNumber,
                DateOrder = DateTime.Now,
                Status = 1
            };

            await _appDbContext.Order!.AddAsync(OrderDomain);
            await _appDbContext.SaveChangesAsync();

            foreach (var cart in createOrderDTO.CartList!)
            {
                var OrderDetailDomain = new Order_Detail
                {
                    ID_Order = OrderDomain.ID_Order,
                    ID_Product = cart.ID_Product,
                    Price = _appDbContext.Product.SingleOrDefault(m => m.ID_Product == cart.ID_Product).Price,
                    Quantity = cart.Quantity
                };

                await _appDbContext.Order_Detail.AddAsync(OrderDetailDomain);
                await _appDbContext.SaveChangesAsync();
            }

            return createOrderDTO;
        }

        public async Task<bool> Cancel(int id)
        {
            var orderDomain = await _appDbContext.Order.SingleOrDefaultAsync(m => m.ID_Order == id);
            if (orderDomain != null)
            {
                orderDomain.Status = 0;
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
