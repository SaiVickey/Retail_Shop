using Moq;
using OnlineRetailShop.Models;
using OnlineRetailShop.Repositories;
using OnlineRetailShop.Services;
using Xunit;

namespace OnlineRetailShop.MockTest
{
    public class MockClass
    {
        [Fact]
        public void GetProduct()
        {
            var mockDataAccess = new Mock<IProductRepository>();
            var productService = new ProductService(mockDataAccess.Object);
            var productId = 5;
            var product = new Product
            {
                IsActive = true,
                ProductId = productId,
                ProductName = "WaterBottle",
                Quantity = 40,
            };

            mockDataAccess.Setup(repository => repository.GetProductById(productId).Result).Returns(product);
            var result = productService.GetProductById(productId).Result;
            Assert.Equal(result, product);
        }

        [Fact]
        public void AddProduct_Valid()
        {
            var mockDataAccess = new Mock<IProductRepository>();
            var productService = new ProductService(mockDataAccess.Object);
            var productId = 15;
            var product = new Product()
            {
                IsActive = true,
                ProductId = productId,
                ProductName = "Coolers",
                Quantity = 40,
            };
            productService.AddProduct(product);
            mockDataAccess.Verify(repository => repository.AddProduct(product), Times.Once());
        }

        [Fact]
        public void UpdateProduct_Valid()
        {
            var mockDataAccess = new Mock<IProductRepository>();
            var productService = new ProductService(mockDataAccess.Object);
            var productId = 15;
            var updatedProduct = new Product()
            {
                IsActive = true,
                ProductId = productId,
                ProductName = "Coolers",
                Quantity = 40,
            };
            productService.UpdateProduct(productId, updatedProduct);

            mockDataAccess.Verify(repository => repository.EditProduct(productId, updatedProduct), Times.Once());
        }

        [Fact]
        public void DeleteProduct_Valid()
        {
            var mockDataAccess = new Mock<IProductRepository>();
            var productService = new ProductService(mockDataAccess.Object);
            var productId = 15;

            productService.DeleteProduct(productId);

            mockDataAccess.Verify(repository => repository.DeleteProduct(productId), Times.Once());
        }

        [Fact]
        public async void GetInvalid_Product()
        {
            var mockDataAccess = new Mock<IProductRepository>();
            var productService = new ProductService(mockDataAccess.Object);
            var productId = 14505;
            await Assert.ThrowsAsync<ArgumentException>(() => productService.GetProductById(productId));
        }
    }
}