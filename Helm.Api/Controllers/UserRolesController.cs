
using Helm.Core.Application.Common;
using Helm.Core.Application.UserRoles.Commands;
using Helm.Core.Application.UserRoles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Helm.Core.Application.UserRoles.Commands.UpdateUserRole;

namespace Helm.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRolesController : Controller
    {
        private ISender sender;
        public UserRolesController(ISender sender)
        {
            this.sender = sender;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUserRoles()
        {
            var result = await sender.Send(new GetAllUserRolesQuery());
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateUserRoleCommand command)
        {
            var result = await sender.Send(command);
            return result.ToHttp(SuccessCodes.Created);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateUserRoleCommand command)
        {
            var result = await sender.Send(command);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await sender.Send(new DeleteUserRoleCommand() { Id = id});
            return result.ToHttp(SuccessCodes.NoContent);
        }
    }
    
}