using OnlineRetailShop.Models;

namespace OnlineRetailShop.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<bool> EditProduct(int id, Product product);
        Task<bool> DeleteProduct(int id);
    }
}