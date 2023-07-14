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
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await productsRepo.GetEntityWithSpec(spec);

            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await productsBrandRepo.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("brands/{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        {
            var spec = new ProductBrandByIdSpecification(id);
            var brand = await productsBrandRepo.GetEntityByIdWithSpec(spec);

            return Ok(brand);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBTypes()
        {
            
            var types = await productsTypeRepo.ListAllAsync();
            return Ok(types);
        }

    }
}

