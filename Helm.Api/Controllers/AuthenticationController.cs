using Helm.Core.Application.Common;
using Helm.Core.Application.UserRoles.Queries;
using MediatR;
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
        private ISender sender;

        public AuthenticationController(ISender sender) 
        {
            this.sender = sender;
        }
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            string? login = HttpContext.User.Claims.FirstOrDefault(c=>c.Type.Contains("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var result = await sender.Send(new GetCurrentUserRolesQuery() { Login = login});
            return result.ToHttp(SuccessCodes.Ok);
        }
    }
}
