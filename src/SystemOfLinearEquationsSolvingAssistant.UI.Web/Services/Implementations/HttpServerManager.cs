using SimpleHttpServer;
using SimpleHttpServer.Entities;
using SimpleHttpServer.Entities.Components;
using System.Reflection;
using System.Text;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views.Base;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModels.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

internal sealed class HttpServerManager : IHttpServerManager
{
    private const string ViewsNamespace = "SystemOfLinearEquationsSolvingAssistant.UI.Web.Views";
    private const string ViewModelsNamespace = "SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModels";
    private const string PagesPath = @"Content\html\";
    private const string StylesPath = @"Content\css\";
    private const string ScriptsPath = @"Content\js\";

    private readonly HttpServer _httpServer;
    private readonly IHtmlViewLinker _htmlViewLinker;

    private readonly List<HtmlView> _views = new();
    private HtmlView? _defaultView;

    public HttpServerManager(HttpServer httpServer, IHtmlViewLinker htmlViewLinker)
    {
        if (httpServer is null)
            throw new ArgumentNullException(nameof(httpServer), "HTTP server must not be null.");

        if (htmlViewLinker is null)
            throw new ArgumentNullException(nameof(htmlViewLinker), "HTML view linker must not be null.");

        (_httpServer, _htmlViewLinker) = (httpServer, htmlViewLinker);

        ConfigureViews();
        ConfigureEndPoints();
    }

    public void SetDefaultView<TView>()
    {
        _defaultView = _views.FirstOrDefault(v => v.GetType().Equals(typeof(TView)) is true) ??
            throw new ArgumentException("The view with the specified type " +
            "does not exist in the view collection.", nameof(TView));

        ConfigurePage(_defaultView, "/");
    }

    public void StartServer() => _httpServer.Start();

    public void StopServer() => _httpServer.Stop();

    private void ConfigureViews()
    {
        Type[] viewTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
            (t.Namespace?.Contains(ViewsNamespace) ?? false) &&
            (t.IsSubclassOf(typeof(HtmlView)) is true)).ToArray();
        Type[] viewModelTypes = Assembly.GetAssembly(typeof(ViewModel))!.GetTypes().Where(t =>
            (t.Namespace?.Contains(ViewModelsNamespace) ?? false) &&
            (t.IsSubclassOf(typeof(ViewModel)) is true)).ToArray();

        HtmlView view;
        ViewModel viewModel;

        foreach (Type viewType in viewTypes)
        {
            try
            {
                Type viewModelType = viewModelTypes.First(t => t.Name.Remove(t.Name.LastIndexOf("ViewModel"))
                    .Equals(viewType.Name.Remove(viewType.Name.LastIndexOf("View")), StringComparison.Ordinal) is true);

                view = (HtmlView)DependenciesContainer.Resolve(viewType)!;
                viewModel = (ViewModel)DependenciesContainer.Resolve(viewModelType)!;

                view.ViewModel = viewModel;
                view.HttpServerResponseGenerator = _httpServer.ResponseGenerator;
                view.RelinkRequest += (_, _) => _htmlViewLinker.Link(view, Path.Combine(PagesPath, view.PagePath));
                _htmlViewLinker.Link(view, Path.Combine(PagesPath, view.PagePath));

                _views.Add(view);
            }
            catch
            {
                throw;
            }
        }
    }

    private void ConfigureEndPoints()
    {
        ConfigurePages();
        ConfigureStyles();
        ConfigureScripts();
    }

    private void ConfigurePages()
    {
        foreach (HtmlView view in _views)
            ConfigurePage(view);
    }

    private void ConfigureStyles()
    {
        foreach (string stylePath in Directory.EnumerateFiles(StylesPath, "*.css", SearchOption.AllDirectories))
        {
            _ = _httpServer!.MapGet(
                $"/{Path.GetRelativePath(StylesPath, stylePath)}",
                req => new HttpResponse(
                    HttpResponseStatus.OK,
                    new HttpHeader[]
                    {
                        new(HttpHeaderGroup.Response, "Content-Type", "text/css; charset=utf-8")
                    },
                    Encoding.UTF8.GetBytes(File.ReadAllText(stylePath))));
        }
    }

    private void ConfigureScripts()
    {
        foreach (string scriptPath in Directory.EnumerateFiles(ScriptsPath, "*.js", SearchOption.AllDirectories))
        {
            _ = _httpServer!.MapGet(
                $"/{Path.GetRelativePath(ScriptsPath, scriptPath)}",
                req => new HttpResponse(
                    HttpResponseStatus.OK,
                    new HttpHeader[]
                    {
                        new(HttpHeaderGroup.Response, "Content-Type", "text/javascript; charset=utf-8")
                    },
                    Encoding.UTF8.GetBytes(File.ReadAllText(scriptPath))));
        }
    }

    private void ConfigurePage(HtmlView view, string? target = default)
    {
        if (view.GetHandler is not null)
            _ = _httpServer.MapGet(target ?? $"/{view.PagePath}", view.GetHandler);

        if (view.HeadHandler is not null)
            _ = _httpServer.MapHead(target ?? $"/{view.PagePath}", view.HeadHandler);

        if (view.PostHandler is not null)
            _ = _httpServer.MapPost(target ?? $"/{view.PagePath}", view.PostHandler);

        if (view.PutHandler is not null)
            _ = _httpServer.MapPut(target ?? $"/{view.PagePath}", view.PutHandler);

        if (view.PatchHandler is not null)
            _ = _httpServer.MapPatch(target ?? $"/{view.PagePath}", view.PatchHandler);

        if (view.DeleteHandler is not null)
            _ = _httpServer.MapDelete(target ?? $"/{view.PagePath}", view.DeleteHandler);

        if (view.ConnectHandler is not null)
            _ = _httpServer.MapConnect(target ?? $"/{view.PagePath}", view.ConnectHandler);

        if (view.OptionsHandler is not null)
            _ = _httpServer.MapOptions(target ?? $"/{view.PagePath}", view.OptionsHandler);
    }
}