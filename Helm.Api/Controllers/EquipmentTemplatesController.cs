using Helm.Core.Application.Common;
using Helm.Core.Application.Equipment.EquipmentTemplate.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Helm.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class EquipmentTemplatesController : ControllerBase
    {
        private ISender sender;
        public EquipmentTemplatesController(ISender sender)
        {
            this.sender = sender;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEquipmentTemplates()
        {
            var result = await sender.Send(new GetAllEquipmentTemplatesQuery());
            return result.ToHttp(SuccessCodes.Ok);
        }
    }
}
