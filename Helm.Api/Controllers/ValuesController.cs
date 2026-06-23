using Helm.Core.Infrastructure.Renderer;
using Helm.Razor.MyFeature.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Helm.Core.Application.EqiupmentTemplate;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Helm.Api.Controllers
{
    [Route("api/[controller]")]
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
            string? result = await htmlRenderer.RenderRazorViewToStringAsync("EquipmentTemplate", new EquipmentTemplateModel());
            return Content(result, "text/html");
        }
    }
}
