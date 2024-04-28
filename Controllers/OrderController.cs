using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Models;
using OnlineRetailShop.Services;

namespace OnlineRetailShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                return Ok(await _orderService.GetAllOrder());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderById(string id)
        {
            try
            {
                return Ok(await _orderService.GetOrderById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> CancelOrder(string id)
        {
            try
            {
                var isCancelled = await _orderService.CancelOrder(id);
                if (isCancelled)
                {
                    return Ok($"Order with Id={id} is cancelled");
                }
                else
                {
                    return NotFound($"Product with Id = {id} is not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrder(Order order)
        {
            try
            {
                await _orderService.PlaceOrder(order);
                return CreatedAtAction(nameof(GetOrders), new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id,quantity}")]
        public async Task<ActionResult> UpdateOrderQuantity(string id, int quantity)
        {
            try
            {
                var isUpdated = await _orderService.UpdateOrderQuantity(id, quantity);
                if (isUpdated)
                {
                    return Ok($"Product with Order Id {id} is updated");
                }
                else
                {
                    return NotFound($"Product with Id = {id} is not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}