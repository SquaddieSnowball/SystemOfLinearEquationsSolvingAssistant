using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers.Implementations;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Solvers.Implementations;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Solvers;
using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services.Implementations;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Implementations;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Interfaces;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.DependencyRegistrators;

internal static class ServicesRegistrator
{
    public static void Register()
    {
        DependenciesContainer
            .Register<ISoleSolver, SoleSolver>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<ISoleParser, SoleFileParser>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<IEventBusService, EventBusService>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<ISoleSolvingAlgorithmNameService, SoleSolvingAlgorithmNameService>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IViewManagerService, WpfViewManagerService>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IUserDialogService, WpfUserDialogService>(DependencyObjectLifetime.Transient);
    }
}