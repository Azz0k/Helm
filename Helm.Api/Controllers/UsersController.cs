
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
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var result = await sender.Send(command);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] UpdateUserStatusCommand command)
        {
            command.Id = id;
            var result = await sender.Send(command);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPut("{id}/password")]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] UpdateUserPasswordCommand command)
        {
            command.Id = id;
            var result = await sender.Send(command);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPut("{userId}/role/{roleId}")]
        public async Task<IActionResult> AssignRoleToUser(int userId, int roleId)
        {
            var result = await sender.Send(new AssignUserRoleCommand() { UserId = userId, RoleId = roleId});
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpDelete("{userId}/role/{roleId}")]
        public async Task<IActionResult> RemoveRoleFromUser(int userId, int roleId)
        {
            var result = await sender.Send(new RemoveUserRoleCommand() { UserId = userId, RoleId = roleId });
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteuser(int id)
        {
            var result = await sender.Send(new DeleteUserCommand() { Id = id});
            return result.ToHttp(SuccessCodes.NoContent);
        }
    }
}
