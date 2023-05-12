using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Views.Windows;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Views;

internal static class ViewsRegistrator
{
    public static void Register()
    {
        DependenciesContainer.Register<MainWindowView>(DependencyObjectLifetime.Singleton);
        DependenciesContainer.Register<LoadingSoleFromFilesWindowView>(DependencyObjectLifetime.Transient);
    }
}