using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Views.Windows;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.DependencyRegistrators;

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
        DependenciesContainer
            .Register<LoadingSoleFromFilesView>(DependencyObjectLifetime.Transient);
    }
}