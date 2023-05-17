using System.Windows;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.ViewModels;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Views;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Interfaces;

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

        DependenciesContainer.Resolve<IViewManagerService>()!.ShowView("Main");
    }
}