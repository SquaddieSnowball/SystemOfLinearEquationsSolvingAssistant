using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.ViewModels.Windows;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.ViewModels;

internal static class ViewModelsRegistrator
{
    public static void Register()
    {
        DependenciesContainer.Register<MainWindowViewModel>(DependencyObjectLifetime.Singleton);
        DependenciesContainer.Register<LoadingSoleFromFilesWindowViewModel>(DependencyObjectLifetime.Singleton);
    }
}