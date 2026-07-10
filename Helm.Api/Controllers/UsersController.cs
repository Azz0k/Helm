
using Helm.Application.Common;
using Helm.Application.Users.Commands;
using Helm.Application.Users.Queries;
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
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetUsersQuery(), cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.ToHttp(SuccessCodes.Created);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] UpdateUserStatusCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var result = await sender.Send(command, cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPut("role")]
        public async Task<IActionResult> ReplaceRoleToUser([FromBody] ReplaceUserRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }

        [HttpPut("{userId}/role/{roleId}")]
        public async Task<IActionResult> AssignRoleToUser(int userId, int roleId, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new AssignUserRoleCommand() { UserId = userId, RoleId = roleId}, cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpDelete("{userId}/role/{roleId}")]
        public async Task<IActionResult> RemoveRoleFromUser(int userId, int roleId, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new RemoveUserRoleCommand() { UserId = userId, RoleId = roleId }, cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteuser(int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new DeleteUserCommand() { Id = id}, cancellationToken);
            return result.ToHttp(SuccessCodes.NoContent);
        }
    }
}
