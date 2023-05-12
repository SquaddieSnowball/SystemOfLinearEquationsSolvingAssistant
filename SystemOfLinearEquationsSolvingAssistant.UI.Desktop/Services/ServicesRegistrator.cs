using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Solvers.Implementations;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Solvers;
using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services.Implementations;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services.Interfaces;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers.Implementations;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services;

internal static class ServicesRegistrator
{
    public static void Register()
    {
        DependenciesContainer
            .Register<ISoleSolver, SoleSolver>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<ISoleParser, SoleFileParser>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<IViewManagerService, ViewManagerService>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IEventBusService, EventBusService>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<IUserDialogService, UserDialogService>(DependencyObjectLifetime.Transient);
        DependenciesContainer
            .Register<ISoleSolvingAlgorithmNameService, SoleSolvingAlgorithmNameService>(DependencyObjectLifetime.Singleton);
    }
}