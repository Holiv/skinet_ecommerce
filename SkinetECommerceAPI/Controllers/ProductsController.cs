using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using AutoMapper;
using SkinetECommerceAPI.DTOs;
using SkinetECommerceAPI.Errors;
using System.Text.Json;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using SkinetECommerceAPI.Helpers;

namespace SkinetECommerceAPI.Controllers
{
    public class ProductsController : BaseApiController
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
        public async Task<ActionResult<Pagination<Products>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            
            var specCount = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await productsRepo.CountAsync(specCount);

            IReadOnlyList<Products> productsEntity = await productsRepo.ListAsync(spec);

            var productsDto = mapper.Map<IReadOnlyList<Products>, IReadOnlyList<ProductToReturnDto>>(productsEntity);


            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, productsDto));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var productEntity = await productsRepo.GetEntityWithSpec(spec);

            if (productEntity is null)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            }

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

        //[HttpGet("brands/teste")]
        //public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        //{
        //    var spec = new ProductBrandByIdSpecification(id);
        //    var brand = await productsBrandRepo.GetEntityByIdWithSpec(spec);

        //    return Ok(brand);
        //}

    }
}

