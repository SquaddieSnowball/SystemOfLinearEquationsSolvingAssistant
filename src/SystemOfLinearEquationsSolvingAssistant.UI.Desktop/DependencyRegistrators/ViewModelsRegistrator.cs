using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModelsCollection;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.DependencyRegistrators;

internal static class ViewModelsRegistrator
{
    public static void Register()
    {
        DependenciesContainer.Register<MainViewModel>(DependencyObjectLifetime.Singleton);
        DependenciesContainer.Register<LoadingSoleFromFilesViewModel>(DependencyObjectLifetime.Singleton);
    }
}