using MongoDB.Bson;
using OnlineRetailShop.Models;

namespace OnlineRetailShop.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrderById(string id);
        Task<Order> PlaceOrder(Order order);
        Task<bool> UpdateOrder(string id, Order order);
    }
}