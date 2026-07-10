using Helm.Application.Common;
using Helm.Application.Equipment.EquipmentTemplate.Commands;
using Helm.Application.Equipment.EquipmentTemplate.Queries;
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
        public async Task<IActionResult> GetAllEquipmentTemplates(CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetAllEquipmentTemplatesQuery(), cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPost]
        public async Task<IActionResult> CreatEquipmentTemplate([FromBody] CreateEquipmentTemplateCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.ToHttp(SuccessCodes.Created);
        }
    }
}
