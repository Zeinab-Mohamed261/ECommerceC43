using Microsoft.AspNetCore.Mvc;
using ServicesAbstrations;
using Shared;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contollers
{
    [ApiController]
    [Route("api/[Controller]")]  //baseUrl/api/Products
    public class ProductsController(IServiceManager _serviceManager ) : ControllerBase
    {
        // Get All Products
        [HttpGet]
        //GET  BaseUrl/api/Products
        public async Task<ActionResult<PaginationResponce<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParameters queryParameters)
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync(queryParameters);
            return Ok(Products);
        }
        // GetProductById
        [HttpGet("{id:int}")]
        //GET  BaseUrl/api/Products/{id}
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }
        //Get All Types
        [HttpGet("types")]
        //GET  BaseUrl/api/Products/types
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync(); 
            return Ok(Types);
        }
        //Get All Brands
        [HttpGet("brands")]
        //GET  BaseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
    }
}
