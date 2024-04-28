using MongoDB.Driver;
using OnlineRetailShop.Models;

namespace OnlineRetailShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IOnlineStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var dataBase = mongoClient.GetDatabase(settings.DataBaseName);
            _products = dataBase.GetCollection<Product>(settings.ProductCollectionName);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _products.Find(product => product.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _products.Find(product => true).ToListAsync();
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> EditProduct(int id, Product updatedProduct)
        {
            var result = await _products.ReplaceOneAsync(product => product.ProductId == id, updatedProduct);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var result = await _products.DeleteOneAsync(product => product.ProductId == id);
            return result.IsAcknowledged;
        }
    }
}