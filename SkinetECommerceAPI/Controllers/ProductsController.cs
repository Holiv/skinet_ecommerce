using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;

namespace SkinetECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Products> productsRepo;
        private readonly IGenericRepository<ProductBrand> productsBrandRepo;
        private readonly IGenericRepository<ProductType> productsTypeRepo;

        public ProductsController(IGenericRepository<Products> productsRepo, IGenericRepository<ProductBrand> productsBrandRepo, IGenericRepository<ProductType> productsTypeRepo)
        {
            this.productsRepo = productsRepo;
            this.productsBrandRepo = productsBrandRepo;
            this.productsTypeRepo = productsTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Products>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            IReadOnlyList<Products> products = await productsRepo.ListAsync(spec);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            //var products = await _storeContext.Products.Where(prod => prod.Id == id).FirstOrDefaultAsync();
            return Ok(await productsRepo.GetByIdAsync(id));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await productsBrandRepo.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBTypes()
        {
            
            var types = await productsTypeRepo.ListAllAsync();
            return Ok(types);
        }

    }
}

