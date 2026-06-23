using Helm.Core.Infrastructure.Renderer;
using Helm.Razor.MyFeature.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Helm.Core.Application.Equipment.EquipmentReceipt;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Helm.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("text/html")]
    public class EquipmentReceiptsController : ControllerBase
    {
        private IHtmlRenderer htmlRenderer;
        public EquipmentReceiptsController(IHtmlRenderer htmlRenderer)
        {
            this.htmlRenderer = htmlRenderer;
        }
        [HttpGet]
        public async Task<IActionResult> Print()
        {
            string? result = await htmlRenderer.RenderRazorViewToStringAsync("EquipmentTemplate", new EquipmentReceiptModel());
            return Content(result, "text/html");
        }
    }
}
