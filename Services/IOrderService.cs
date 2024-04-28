using OnlineRetailShop.Models;

namespace OnlineRetailShop.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrder();
        Task<Order> GetOrderById(string id);
        Task<Order> PlaceOrder(Order order);
        Task<bool> UpdateOrderQuantity(string id, int quantity);
        Task<bool> CancelOrder(string id);
    }
}