using Helm.Application.Common;
using Helm.Application.Equipment.Equipment.Commands;
using Helm.Application.Equipment.Equipment.Queries;
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
        public async Task<IActionResult> GetAllEquipment(CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetAllEquipmentQuery(), cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEquipment([FromBody] CreateEquipmentCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.ToHttp(SuccessCodes.Created);
        }
        [HttpPut]
        public async Task<IActionResult> RenameEquipment([FromBody] RenameEquipmentCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPut("issue")]
        public async Task<IActionResult> IssueEquipment([FromBody] IssueEquipmentCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPut("return")]
        public async Task<IActionResult> ReturnEquipment([FromBody] ReturnEquipmentCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
        [HttpPut("lose")]
        public async Task<IActionResult> LoseEquipment([FromBody] LoseEquipmentCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.ToHttp(SuccessCodes.Ok);
        }
    }
}
