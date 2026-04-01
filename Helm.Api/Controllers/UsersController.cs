
using Helm.Core.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Helm.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ISender sender;
        public UsersController(ISender sender) 
        { 
            this.sender = sender;
        }
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var result = await sender.Send(new GetUsersQuery());
            return result.Users;
        }

    }
}
