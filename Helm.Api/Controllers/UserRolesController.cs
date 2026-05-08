
using Helm.Core.Application.Common;
using Helm.Core.Application.UserRoles.Commands;
using Helm.Core.Application.UserRoles.Queries;
using Helm.Core.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var result = await sender.Send(new GetUserRolesQuery());
            return result switch
            {
                GetOperationResult<UserRolesVm>.Success x => Ok(x.Data.UserRoles),
                _ => BadRequest("")
            };
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateUserRoleCommand command)
        {
            var result = await sender.Send(command);
            return result switch
            {
                GetOperationResult<UserRolesVm>.Success x => Created("", x.Data.UserRoles.First()),
                GetOperationResult<UserRolesVm>.Conflict => Conflict(),
                _ => BadRequest("")
            };
        }
    }
}