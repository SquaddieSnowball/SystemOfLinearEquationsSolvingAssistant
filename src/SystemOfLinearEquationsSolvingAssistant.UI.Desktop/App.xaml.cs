using System.Windows;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.DependencyRegistrators;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ServicesRegistrator.Register();
        ViewModelsRegistrator.Register();
        ViewsRegistrator.Register();

        DependenciesContainer.Resolve<IViewManager>()?.ShowView("Main");
    }
}