using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOS;
using AutoMapper;
using Core.Entity;
using Core.Interfaces;
using Core.Specification;
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
        private readonly IGenericRepository<Product> _genrepo;
        private readonly IGenericRepository<ProductType> _genrepoType;
        private readonly IGenericRepository<ProductBrand> _genrepoBrand;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> genrepo,IGenericRepository<ProductType> genrepoType,
        IGenericRepository<ProductBrand> genrepoBrand,IMapper mapper)
        {
            _genrepo = genrepo;
            _genrepoBrand = genrepoBrand;
            _mapper = mapper;
            _genrepoType = genrepoType;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _genrepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product= await _genrepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product,ProductToReturnDto>(product);
        }

        [HttpGet("ProductBrands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
           var brands = await _genrepoBrand.GetAllAsync();
           return Ok(brands);
        }
        [HttpGet("ProductTypes")]
        public async Task<ActionResult<List<ProductType>>> GetTypes()
        {
           var types = await _genrepoType.GetAllAsync();
           return Ok(types);
        }

    }
}