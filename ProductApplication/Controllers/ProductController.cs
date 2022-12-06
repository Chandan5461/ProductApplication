using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApplication.Data;
using ProductApplication.Models.Domain;
using ProductApplication.Repostries;

namespace ProductApplication.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin,User,Manager")]

    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = productRepository.GetAll();



            return Ok(products);

        }

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetProduct")]

        public IActionResult GetProducts(int id)
        {
            var product = productRepository.Get(id);
            if (product == null)
            {
                return NotFound("productNotFound");
            }

            return Ok(product);
        }

        [HttpPost]

        public IActionResult AddProduct(Models.DTO.DeleteProductRequest addProductRequest)
        {

            if (addProductRequest == null)
            {
                return NotFound("product not found");
            }
            var product = new Models.Domain.Product()
            {
                Name = addProductRequest.Name,
                quantity = addProductRequest.quantity,
                Price = addProductRequest.Price,
                status = addProductRequest.status,

            };



            product = productRepository.Add(product);

            var productDTO = new Models.DTO.Product
            {



                Id = product.Id,
                Name = product.Name,
                quantity = product.quantity,
                Price = product.Price,
                status = product.status,


            };

            return CreatedAtAction("GetProduct", new { id = productDTO.Id }, productDTO);
            //return Ok(productDTO);



        }


        [HttpDelete]
        [Route("{id}")]

        public IActionResult DeleteProduct(int id)
        {
            var product = productRepository.Delete(id);

            if (product == null)
            {
                return NotFound("product Not found");
            }

            var productDTO = new Models.DTO.Product
            {
                Id = product.Id,
                Name = product.Name,
                quantity = product.quantity,
                Price = product.Price,
                status = product.status,
            };

            return Ok(productDTO);

        }


        [HttpPut]
        [Route("{id:int}")]

        public IActionResult updateProduct([FromRoute] int id, [FromBody] Models.DTO.UpdateProductRequest updateProductRequest)
        {

            if (updateProductRequest == null)
            {
                return NotFound("product not found");
            }
            var product = new Models.Domain.Product()
            {
                Id = id,
                Name = updateProductRequest.Name,
                quantity = updateProductRequest.quantity,
                Price = updateProductRequest.Price,
                status = updateProductRequest.status,

            };

            product = productRepository.Update(id, product);

            if (product == null)
            {
                return NotFound("product not found");
            }

            var productDTO = new Models.DTO.Product
            {
                Id = product.Id,
                Name = product.Name,
                quantity = product.quantity,
                Price = product.Price,
                status = product.status,
            };

            return Ok(productDTO);
        }
    }
}
