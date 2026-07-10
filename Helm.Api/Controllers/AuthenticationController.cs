using Helm.Application.Common;
using Helm.Application.UserRoles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace Helm.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private ISender sender;

        public AuthenticationController(ISender sender) 
        {
            this.sender = sender;
        }
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            string? login = HttpContext.User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await sender.Send(new GetCurrentUserRolesQuery() { Login = login});
            return result.ToHttp(SuccessCodes.Ok);
        }
    }
}
