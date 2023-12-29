using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Solvers.Implementations;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Solvers;
using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Implementations;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Interfaces;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleHttpServer.Extensions.Options;
using SimpleHttpServer.Services.Abstractions;
using SimpleHttpServer.Services.Implementations;
using SimpleHttpServer;
using SimpleTcpServer.Extensions.Options;
using SimpleTcpServer;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Providers;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.DependencyRegistrators;

internal static class ServicesRegistrator
{
    public static void Register()
    {
        DependenciesContainer
            .Register<ISoleSolver, SoleSolver>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<IEventBusService, EventBusService>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<ISoleSolvingAlgorithmNameService, SoleSolvingAlgorithmNameService>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IViewManagerService, HtmlViewManagerService>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<IUserDialogService, HtmlUserDialogService>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<IOptions<TcpServerOptions>, TcpServerOptionsProvider>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<ILogger<TcpServer>, TcpServerLoggerProvider>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<TcpServer>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IOptions<HttpServerOptions>, HttpServerOptionsProvider>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<ILogger<HttpServer>, HttpServerLoggerProvider>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<IHttpRequestParser, HttpRequestParser>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IHttpResponseBuilder, HttpResponseBuilder>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IHttpServerResponseGenerator, HttpServerResponseGenerator>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<HttpServer>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IHttpServerManagerService, HttpServerManagerService>(DependencyObjectLifetime.Singleton);
    }
}