using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace SkinetECommerceAPI.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet("exception")]
        public ActionResult<string> GetException()
        {
             var product = context.Products.Find(45).ToString();

            return Ok(product);
        }
    }
}
