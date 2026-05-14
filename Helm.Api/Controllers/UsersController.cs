
using Helm.Core.Application.Common;
using Helm.Core.Application.Users.Commands;
using Helm.Core.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Helm.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private ISender sender;
        public UsersController(ISender sender) 
        { 
            this.sender = sender;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await sender.Send(new GetUsersQuery());
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserCommand command)
        {
            var result = await sender.Send(command);
            return result.ToHttp(SuccessCodes.Created);
        }
    }
}
