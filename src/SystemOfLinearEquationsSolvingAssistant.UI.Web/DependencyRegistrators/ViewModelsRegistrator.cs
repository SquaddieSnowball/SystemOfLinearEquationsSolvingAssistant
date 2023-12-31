using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModels;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.DependencyRegistrators;

/// <summary>
/// Provides the method used to register view models.
/// </summary>
internal static class ViewModelsRegistrator
{
    /// <summary>
    /// Registers view models in a dependency container.
    /// </summary>
    public static void Register()
    {
        DependenciesContainer
            .Register<MainViewModel>(DependencyObjectLifetime.Singleton);
    }
}