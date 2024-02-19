using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZstdSharp.Unsafe;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _ProductRepo;
        public ProductController(IProductRepository productRepo)
        {
            _ProductRepo = productRepo;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _ProductRepo.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _ProductRepo.GetProductByIdAsync(id);
        }

        [HttpGet("ProductBrands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
           var brands = await _ProductRepo.GetProductBrandsAsync();
           return Ok(brands);
        }
        [HttpGet("ProductTypes")]
        public async Task<ActionResult<List<ProductType>>> GetTypes()
        {
           var types = await _ProductRepo.GetProductTypesAsync();
           return Ok(types);
        }

    }
}