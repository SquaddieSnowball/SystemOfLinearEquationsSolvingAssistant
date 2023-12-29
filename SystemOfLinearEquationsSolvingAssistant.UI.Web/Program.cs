using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.DependencyRegistrators;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Interfaces;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views;

ToolsRegistrator.Register();
ServicesRegistrator.Register();
ViewModelsRegistrator.Register();
ViewsRegistrator.Register();

IHttpServerManagerService httpServerManagerService = DependenciesContainer.Resolve<IHttpServerManagerService>()!;
httpServerManagerService.SetDefaultView<MainView>();
httpServerManagerService.StartServer();

Console.ReadLine();