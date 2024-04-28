using OnlineRetailShop.Models;
using OnlineRetailShop.Repositories;

namespace OnlineRetailShop.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            var products = await _productRepository.GetAllProduct();
            if (products.Any())
                return products;
            else
            {
                throw new ArgumentException($"No Product available");
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new ArgumentException($"Product with id {id} is not found");
            }
        }

        public async Task<Product> AddProduct(Product product)
        {
            var isExist = await _productRepository.GetProductById(product.ProductId);
            if (isExist == null)
            {
                var addedProduct = await _productRepository.AddProduct(product);
                if (addedProduct != null)
                {
                    return product;
                }
                else
                {
                    throw new ArgumentException($"Unable to add the product");
                }
            }
            else
            {
                throw new ArgumentException($"Product with id {product.ProductId} is already available");
            }
        }

        public async Task<bool> UpdateProduct(int id, Product product)
        {
            var result = await _productRepository.EditProduct(id, product);
            if (result)
                return result;
            else
            {
                throw new Exception("Unable to update the product");
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
           var result = await _productRepository.DeleteProduct(id);
           if (result)
               return result;
           else
           {
               throw new Exception("Unable to delete the product");
           }
        }
    }
}
