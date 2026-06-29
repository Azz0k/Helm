using Helm.Core.Application.Common;
using Helm.Core.Application.Equipment.Equipment.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Helm.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class EquipmentController : ControllerBase
    {
        private ISender sender;
        public EquipmentController(ISender sender)
        {
            this.sender = sender;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEquipment()
        {
            var result = await sender.Send(new GetAllEquipmentQuery());
            return result.ToHttp(SuccessCodes.Ok);
        }
    }
}
