
using Helm.Application.Common;
using Helm.Application.UserRoles.Commands;
using Helm.Application.UserRoles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Helm.Application.UserRoles.Commands.UpdateUserRole;

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
        public async Task<IActionResult> GetAllUserRoles(CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetAllUserRolesQuery(), cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateUserRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.ToHttp(SuccessCodes.Created);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateUserRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new DeleteUserRoleCommand() { Id = id}, cancellationToken);
            return result.ToHttp(SuccessCodes.NoContent);
        }
    }
    
}