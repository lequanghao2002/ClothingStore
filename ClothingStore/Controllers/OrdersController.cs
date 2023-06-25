using ClothingStore.Models.Categories;
using ClothingStore.Models.Orders;
using ClothingStore.Repositories.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("get-all-order")]
        [AuthorizeRoles("Read", "Write", "Admin")]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                var orderList = await _orderRepository.GetAll();
                return Ok(orderList);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("get-order-by-id/{id}")]
        [AuthorizeRoles("Read", "Write", "Admin")]
        public async Task<IActionResult> GetOrderById([Required]int id)
        {
            try
            {
                var orderById = await _orderRepository.GetOrderById(id);
                if (orderById == null)
                    return BadRequest($"Không tìm thấy sản phẩm có id: {id}");
                return Ok(orderById);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("order")]
        [AuthorizeRoles("Write", "Admin")]
        public async Task<IActionResult> Order(CreateOrderDTO createOrderDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var order = await _orderRepository.Order(createOrderDTO);
                    if (order != null)
                    {
                        return Ok(order);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("cancel/{id}")]
        [AuthorizeRoles("Write", "Admin")]
        public async Task<IActionResult> Cancel([Required]int id)
        {
            try
            {
                var order = await _orderRepository.Cancel(id);
                if (order == true)
                {
                    return Ok("Đã hủy hàng");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
