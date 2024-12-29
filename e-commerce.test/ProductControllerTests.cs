using Data;
using Data.DTOs;
using e_commerce.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductShoppingCartMvcUI.Repositories;
using Xunit;

namespace e_commerce.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;

        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _controller = new ProductController(_mockRepo.Object);

        }

        // Test case for Index action
        [Fact]
        public async Task Index_Returns_ViewResult_With_Products()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 100 },
                new Product { Id = 2, Name = "Product 2", Price = 200 }
            };
           var data= _mockRepo.Setup(repo => repo.GetProducts()).ReturnsAsync(products);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Equal(2, ((List<Product>)model).Count);
        }

        // Test case for AddProduct GET action
        [Fact]
        public async Task AddProduct_Returns_ViewResult()
        {
            // Act
            var result = await _controller.AddProduct();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task AddProduct_Post_Calls_AddProduct_Method_When_Valid()
        {
            // Arrange
            var productToAdd = new ProductDTO { Id = 1, Name = "New Product", Price = 150 };

            // Mock the repository method to simulate a successful add
            _mockRepo.Setup(repo => repo.AddProduct(It.IsAny<Product>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddProduct(productToAdd);

            // Assert
            // Verify that the AddProduct method was called once with the correct Product object
            _mockRepo.Verify(repo => repo.AddProduct(It.Is<Product>(p => p.Name == productToAdd.Name && p.Price == productToAdd.Price)), Times.Once);

            // Verify that the result is a redirect to the Index action (assuming it redirects after successful add)
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        // Test case for AddProduct POST action - invalid model state
        [Fact]
        public async Task AddProduct_Post_Returns_ViewResult_When_InvalidModel()
        {
            // Arrange
            var productToAdd = new ProductDTO { Id = 1, Name = "New Product", Price = -150 };  // Invalid price
            _controller.ModelState.AddModelError("Price", "Invalid price");

            // Act
            var result = await _controller.AddProduct(productToAdd);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(productToAdd, viewResult.Model);
        }

        // Test case for UpdateProduct GET action
        [Fact]
        public async Task UpdateProduct_Returns_ViewResult_With_ProductDTO()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Existing Product", Price = 200 };
            _mockRepo.Setup(repo => repo.GetProductById(1)).ReturnsAsync(product);

            // Act
            var result = await _controller.UpdateProduct(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ProductDTO>(viewResult.Model);
            Assert.Equal("Existing Product", model.Name);
            Assert.Equal(200, model.Price);
        }

        [Fact]
        public async Task UpdateProduct_Post_Calls_UpdateProduct_Method_When_Valid()
        {
            // Arrange
            var productToUpdate = new ProductDTO { Id = 1, Name = "Updated Product", Price = 200 };

            // Mock the repository method (UpdateProduct)
            _mockRepo.Setup(repo => repo.UpdateProduct(It.IsAny<Product>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateProduct(productToUpdate);

            // Assert
            // Verify that the UpdateProduct method was called once with the correct Product object
            _mockRepo.Verify(repo => repo.UpdateProduct(It.Is<Product>(p => p.Id == productToUpdate.Id && p.Name == productToUpdate.Name && p.Price == productToUpdate.Price)), Times.Once);

            // Verify the result is a redirect to the Index action (assuming it redirects after successful update)
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        // Test case for UpdateProduct POST action - invalid model state
        [Fact]
        public async Task UpdateProduct_Post_Returns_ViewResult_When_InvalidModel()
        {
            // Arrange
            var productToUpdate = new ProductDTO { Id = 1, Name = "Updated Product", Price = -200 }; // Invalid price
            _controller.ModelState.AddModelError("Price", "Invalid price");

            // Act
            var result = await _controller.UpdateProduct(productToUpdate);

            // Assert
            // Verify that the UpdateProduct method was not called due to invalid model state
            _mockRepo.Verify(repo => repo.UpdateProduct(It.IsAny<Product>()), Times.Never);

            // Verify that it returns the view with the same model
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(productToUpdate, viewResult.Model);
        }

        // Test case for UpdateProduct GET action - product not found
        [Fact]
        public async Task UpdateProduct_Returns_RedirectToAction_When_Product_Not_Found()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetProductById(It.IsAny<int>())).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.UpdateProduct(1); // Product ID 1 does not exist

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        // Test case for DeleteProduct - success
        [Fact]
        public async Task DeleteProduct_Returns_RedirectToAction_When_Successful()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product to Delete", Price = 300 };
            _mockRepo.Setup(repo => repo.GetProductById(1)).ReturnsAsync(product);
            _mockRepo.Setup(repo => repo.DeleteProduct(It.IsAny<Product>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteProduct(1);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        // Test case for DeleteProduct - product not found
        [Fact]
        public async Task DeleteProduct_Returns_RedirectToAction_When_ProductNotFound()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetProductById(1)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.DeleteProduct(1);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }
    }
}
