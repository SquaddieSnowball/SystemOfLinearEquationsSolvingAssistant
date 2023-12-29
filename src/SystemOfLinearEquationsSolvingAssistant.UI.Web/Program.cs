using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.DependencyRegistrators;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views;

ServicesRegistrator.Register();
ViewModelsRegistrator.Register();
ViewsRegistrator.Register();

IHttpServerManager httpServerManagerService = DependenciesContainer.Resolve<IHttpServerManager>()!;
httpServerManagerService.SetDefaultView<MainView>();
httpServerManagerService.StartServer();

Console.ReadLine();