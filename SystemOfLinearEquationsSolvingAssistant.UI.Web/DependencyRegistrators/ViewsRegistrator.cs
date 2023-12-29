using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.DependencyRegistrators;

internal static class ViewsRegistrator
{
    public static void Register()
    {
        DependenciesContainer.Register<MainView>(DependencyObjectLifetime.Singleton);
    }
}