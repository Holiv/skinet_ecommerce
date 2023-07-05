using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;

namespace SkinetECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public ProductsController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Products>>> GetProducts()
        {
            List<Products> products = await _storeContext.Products.ToListAsync();

            //List<Products> products = new List<Products>
            //{
            //    new Products() { Id = 1, Name = "teste" },
            //    new Products() { Id = 2, Name = "teste 2"}
            //};

            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            //var products = await _storeContext.Products.Where(prod => prod.Id == id).FirstOrDefaultAsync();
            return await _storeContext.Products.FindAsync(id);
        }

    }
}

