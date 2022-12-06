using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApplication.Controllers;
using ProductApplication.Models.DTO;
using ProductApplication.Repostries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApplicationTest
{
    public class GetProductIdTest
    {
        private MockRepository _repository;
        private Mock<IProductRepository> _productRepository;



        [SetUp]
        public void Setup()
        {
            this._repository = new MockRepository(MockBehavior.Strict);
            this._productRepository = this._repository.Create<IProductRepository>();
        }



        private ProductsController CreateProductController()
        {
            return new ProductsController(this._productRepository.Object);
        }



        [Test]
        public void GetProductId_ShouldReturnOkResult_WhenProductFound()
        {
            // Arrange
            var productController = this.CreateProductController();
            var product = new ProductApplication.Models.Domain.Product() { Id = 1, Name = "shirt", Price = 2000, quantity = 2 };
            int Id = 1;
            _productRepository.Setup(i => i.Get(Id)).Returns(product);



            // Act
            var result = productController.GetProducts(Id) as ObjectResult;



            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public void GetProductId_ShouldReturnNotFound_WhenProductIsNotFound()
        {
            // Arrange
            var productController = this.CreateProductController();
            ProductApplication.Models.Domain.Product? product = null;
            int Id = 1;
            _productRepository.Setup(i => i.Get(Id)).Returns(product);



            // Act
            var result = productController.GetProducts(Id) as ObjectResult;



            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void PostProduct_ShouldReturnCreatedResult_WhenProductCreationSuccess()
        {
            // Arrange
            var productController = CreateProductController();
            var product = new ProductApplication.Models.Domain.Product() { Id = 1, Name = "Sofa", Price = 3000, quantity = 2 };
            var productDTO = new DeleteProductRequest() { Name = "Sofa", Price = 3000, quantity = 2 };
            _productRepository.Setup(i => i.Add(product)).Returns(product);



            // Act
            var result = productController.AddProduct(productDTO) as ObjectResult;



            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }
        [Test]
        public void PostProduct_ShouldReturnNotFOund_WhenWePostEmptyProduct()
        {
            // Arrange
            var productController = CreateProductController();
            ProductApplication.Models.Domain.Product? product = null;
            DeleteProductRequest? productDTO = null;
            //int Id = 1;
            _productRepository.Setup(i => i.Add(product)).Returns(product);



            // Act
            var result = productController.AddProduct(productDTO) as ObjectResult;



            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }


        [Test]
        public void DeleteProduct_ShouldReturnOkResult_WhenProductDeleteSuccess()
        {
            // Arrange
            var productController = CreateProductController();
            var product = new ProductApplication.Models.Domain.Product() { Id = 1, Name = "Sofa", Price = 3000, quantity = 2 };
            int Id = 1;
            _productRepository.Setup(i => i.Delete(Id)).Returns(product);



            // Act
            var result = productController.DeleteProduct(Id) as ObjectResult;



            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public void DeleteProduct_ShouldReturnNotFound_WhenProductNotFound()
        {
            // Arrange
            var productController = CreateProductController();
            //var product = new Product() { Id = 1, Name = "Sofa", Price = 3000, Quantity = 2 };
            ProductApplication.Models.Domain.Product? product = null;
            int Id = 1;
            _productRepository.Setup(i => i.Delete(Id)).Returns(product);



            // Act
            var result = productController.DeleteProduct(Id) as ObjectResult;



            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }


        [Test]
        public void PutProduct_ShouldReturnOkResult_WhenProductDeleteSuccess()
        {
            // Arrange
            var productController = CreateProductController();
            var product = new ProductApplication.Models.Domain.Product() { Id = 1, Name = "Sofa", Price = 3000, quantity = 2 };
            var productDTO = new UpdateProductRequest() { Name = "Sofa", Price = 3000, quantity = 2 };
            int Id = 1;
            _productRepository.Setup(i => i.Update(Id, product)).Returns(product);



            // Act
            var result = productController.updateProduct(Id, productDTO) as ObjectResult;



            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public void UpdateProduct_ShouldReturnNotFound_WhenProductNotFound()
        {
            // Arrange
            var productController = CreateProductController();
            ProductApplication.Models.Domain.Product? product = null;
            UpdateProductRequest? productDTO = null;
            int Id = 1;
            _productRepository.Setup(i => i.Update(Id, product)).Returns(product);



            // Act
            var result = productController.updateProduct(Id, productDTO) as ObjectResult;



            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

    }
}
