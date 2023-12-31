using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services.Implementations;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Implementations;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Implementations.SoleParser;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Implementations;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.DependencyRegistrators;

/// <summary>
/// Provides the method used to register services.
/// </summary>
internal static class ServicesRegistrator
{
    /// <summary>
    /// Registers services in a dependency container.
    /// </summary>
    public static void Register()
    {
        DependenciesContainer
            .Register<IEventBus, EventBus>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<ISoleSolvingAlgorithmNameService, SoleSolvingAlgorithmNameService>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IViewManager, WpfViewManager>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<ISoleParser, SoleFileParser>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<ISoleSolver, SoleSolver>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<IUserDialogService, WpfUserDialogService>(DependencyObjectLifetime.Transient);
    }
}