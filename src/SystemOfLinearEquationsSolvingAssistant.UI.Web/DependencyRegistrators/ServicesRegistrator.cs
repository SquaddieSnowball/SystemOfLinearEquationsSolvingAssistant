using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Implementations;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;
using SimpleHttpServer.Services.Abstractions;
using SimpleHttpServer.Services.Implementations;
using SimpleHttpServer;
using SimpleTcpServer;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Implementations;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleHttpServer.Extensions.Options;
using SimpleTcpServer.Extensions.Options;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.DependencyRegistrators;

internal static class ServicesRegistrator
{
    public static void Register()
    {
        DependenciesContainer
            .Register<IHtmlViewVisualsParser, HtmlViewVisualsParser>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<IHtmlViewLinker, HtmlViewLinker>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<ISoleSolver, SoleSolver>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<IEventBus, EventBus>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<ISoleSolvingAlgorithmNameService, SoleSolvingAlgorithmNameService>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IViewManager, HtmlViewManager>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<IUserDialogService, HtmlUserDialogService>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<IHttpRequestParser, HttpRequestParser>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IHttpResponseBuilder, HttpResponseBuilder>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IHttpServerResponseGenerator, HttpServerResponseGenerator>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IHttpServerManager, HttpServerManager>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IOptions<TcpServerOptions>, TcpServerOptionsProvider>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<IOptions<HttpServerOptions>, HttpServerOptionsProvider>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<ILogger<TcpServer>, TcpServerLoggerProvider>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<ILogger<HttpServer>, HttpServerLoggerProvider>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<TcpServer>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<HttpServer>(DependencyObjectLifetime.Singleton);
    }
}