using Microsoft.AspNetCore.Mvc;
using SkinetECommerceAPI.Errors;

namespace SkinetECommerceAPI.Controllers
{
    [Route("errors/{code}")]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
        
    }
}
