using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.DependencyRegistrators;

/// <summary>
/// Provides the method used to register views.
/// </summary>
internal static class ViewsRegistrator
{
    /// <summary>
    /// Registers views in a dependency container.
    /// </summary>
    public static void Register()
    {
        DependenciesContainer
            .Register<MainView>(DependencyObjectLifetime.Singleton);
    }
}