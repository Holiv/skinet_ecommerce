using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using AutoMapper;
using SkinetECommerceAPI.DTOs;

namespace SkinetECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Products> productsRepo;
        private readonly IGenericRepository<ProductBrand> productsBrandRepo;
        private readonly IGenericRepository<ProductType> productsTypeRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Products> productsRepo, IGenericRepository<ProductBrand> productsBrandRepo, IGenericRepository<ProductType> productsTypeRepo, IMapper mapper)
        {
            this.productsRepo = productsRepo;
            this.productsBrandRepo = productsBrandRepo;
            this.productsTypeRepo = productsTypeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Products>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            IReadOnlyList<Products> productsEntity = await productsRepo.ListAsync(spec);
            var productsDto = mapper.Map<IReadOnlyList<Products>, IReadOnlyList<ProductToReturnDto>>(productsEntity);

            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var productEntity = await productsRepo.GetEntityWithSpec(spec);
            var productDto = mapper.Map<Products, ProductToReturnDto>(productEntity);

            return Ok(productDto);
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

