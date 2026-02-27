using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Helm.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        public AuthenticationController() 
        { 
        }
        [HttpGet]
        public async Task<IResult> Get()
        {
            var userName = HttpContext.User.Identity?.Name;            
            return Results.Ok(userName);
        }
        [HttpPost]
        public async Task<IResult> Post()
        {
            var userName = HttpContext.User.Identity?.Name;
            return Results.Ok(userName);
        }
    }
}
