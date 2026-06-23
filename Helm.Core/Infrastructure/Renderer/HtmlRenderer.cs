using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
namespace Helm.Core.Infrastructure.Renderer;

public interface IHtmlRenderer
{
    Task<string> RenderRazorViewToStringAsync<TModel>(string viewName, TModel model);
}
public class HtmlRenderer : IHtmlRenderer
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IRazorViewEngine razorViewEngine;
    private readonly ITempDataProvider tempDataProvider;

    public HtmlRenderer(
        IHttpContextAccessor httpContextAccessor,
        IRazorViewEngine razorViewEngine,
        ITempDataProvider tempDataProvider,
         IServiceScopeFactory serviceScopeFactory)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.razorViewEngine = razorViewEngine;
        this.tempDataProvider = tempDataProvider;
    }

    /// <summary>
    /// Renders a razor view as html.
    /// </summary>
    /// <typeparam name="TModel">The type of the view model.</typeparam>
    /// <param name="viewName">The name of the view. ex. ~/Views/MyView.cshtml</param>
    /// <param name="model">The view model.</param>
    /// <returns>Task with string of html from rendered view.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<string> RenderRazorViewToStringAsync<TModel>(string viewName, TModel model)
    {
        var actionContext = new ActionContext(httpContextAccessor.HttpContext, httpContextAccessor.HttpContext.GetRouteData(), new ActionDescriptor());
        var view = FindView(actionContext, viewName);

        using var output = new StringWriter();

        var viewContext = new ViewContext(
            actionContext,
            view,
            new ViewDataDictionary<TModel>(
                metadataProvider: new EmptyModelMetadataProvider(),
                modelState: new ModelStateDictionary())
            {
                Model = model
            },
            new TempDataDictionary(
                actionContext.HttpContext,
                tempDataProvider),
            output,
            new HtmlHelperOptions());

        await view.RenderAsync(viewContext);

        return output.ToString();
        IView FindView(ActionContext actionContext, string viewName)
        {
            var getViewResult = razorViewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
            if (getViewResult.Success)
            {
                return getViewResult.View;
            }

            var findViewResult = razorViewEngine.FindView(actionContext, viewName, isMainPage: true);
            if (findViewResult.Success)
            {
                return findViewResult.View;
            }

            var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
            var errorMessage = string.Join(
                Environment.NewLine,
                new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations));
            throw new InvalidOperationException(errorMessage);
        }
    }
}
