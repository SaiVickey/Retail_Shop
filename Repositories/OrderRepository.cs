using MongoDB.Driver;
using OnlineRetailShop.Models;

namespace OnlineRetailShop.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IOnlineStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var dataBase = mongoClient.GetDatabase(settings.DataBaseName);
            _orders = dataBase.GetCollection<Order>(settings.OrderCollectionName);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _orders.Find(order => true).ToListAsync();
        }

        public async Task<Order> GetOrderById(string id)
        {
            return await _orders.Find(order => order.OrderId == id).FirstOrDefaultAsync();
        }

        public async Task<Order> PlaceOrder(Order order)
        {
            await _orders.InsertOneAsync(order);
            return order;
        }

        public async Task<bool> UpdateOrder(string id, Order order)
        {
            var result = await _orders.ReplaceOneAsync(existingOrder => existingOrder.OrderId == id, order);
            return result.ModifiedCount > 0;
        }
    }
}