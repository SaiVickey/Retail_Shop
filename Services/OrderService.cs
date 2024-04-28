using MongoDB.Bson;
using OnlineRetailShop.Models;
using OnlineRetailShop.Repositories;

namespace OnlineRetailShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            var orders = await _orderRepository.GetOrders();
            if (orders.Any())
            {
                return orders;
            }
            else
            {
                throw new ArgumentException("No Orders found");
            }
        }

        public async Task<Order> GetOrderById(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (isValid)
            {
                return await _orderRepository.GetOrderById(id);
            }
            else
            {
                throw new ArgumentException($"Invalid Order id = {id}");
            }
        }

        public async Task<Order> PlaceOrder(Order order)
        {
            var product = await _productRepository.GetProductById(order.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with Product Id {order.ProductId} not found");
            }
            var productQuantity = product.Quantity;

            if (productQuantity > order.Quantity)
            {
                var newOrder = await _orderRepository.PlaceOrder(order);

                product.Quantity = productQuantity - order.Quantity;
                if (product.Quantity <= 0)
                    product.IsActive = false;
                await _productRepository.EditProduct(product.ProductId, product);
                return order;
            }
            else
            {
                throw new InvalidOperationException("Ordered quantity exceeds the available stock");
            }
        }

        public async Task<bool> UpdateOrderQuantity(string id, int quantity)
        {
            var order = await GetOrderById(id);
            var product = await _productRepository.GetProductById(order.ProductId);
            var productQuantity = product.Quantity;

            if (productQuantity > order.Quantity)
            {
                var newOrder = await _orderRepository.PlaceOrder(order);

                product.Quantity = productQuantity - order.Quantity;
                if (product.Quantity <= 0)
                    product.IsActive = false;
                await _productRepository.EditProduct(product.ProductId, product);
            }
            else
            {
                throw new InvalidOperationException("Ordered quantity exceeds the available stock");
            }
            order.Quantity = quantity;
            var result = await _orderRepository.UpdateOrder(id, order);
            if (result)
                return result;
            else
            {
                throw new Exception("Unable to update the order quantity");
            }
        }

        public async Task<bool> CancelOrder(string id)
        {
            bool result;
            var order = await GetOrderById(id);
            {
                order.IsOrderActive = false;
                await _orderRepository.UpdateOrder(id, order);

                var product = await _productRepository.GetProductById(order.ProductId);
                product.Quantity += order.Quantity;
                if (product.IsActive == false)
                    product.IsActive = true;
                result = await _productRepository.EditProduct(product.ProductId, product);
            }

            if (result)
                return result;
            else
            {
                throw new Exception("Unable to cancel the order");
            }
        }
    }
}