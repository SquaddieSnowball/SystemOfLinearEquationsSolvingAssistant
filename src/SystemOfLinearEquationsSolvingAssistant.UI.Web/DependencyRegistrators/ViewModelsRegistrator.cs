using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModels;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.DependencyRegistrators;

internal static class ViewModelsRegistrator
{
    public static void Register()
    {
        DependenciesContainer.Register<MainViewModel>(DependencyObjectLifetime.Singleton);
    }
}