using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Views.Windows;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.DependencyRegistrators;

internal static class ViewsRegistrator
{
    public static void Register()
    {
        DependenciesContainer
            .Register<MainView>(DependencyObjectLifetime.Singleton);
        DependenciesContainer
            .Register<LoadingSoleFromFilesView>(DependencyObjectLifetime.Transient);
    }
}