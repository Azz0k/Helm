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
        [HttpPost]
        public async Task<IResult> Post()
        {
            string? userName = HttpContext.User.Claims.FirstOrDefault(c=>c.Type.Contains("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            
            return Results.Ok(userName);
        }
    }
}
