using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.DependencyRegistrators;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views;

ServicesRegistrator.Register();
ViewModelsRegistrator.Register();
ViewsRegistrator.Register();

IHttpServerManager httpServerManager = DependenciesContainer.Resolve<IHttpServerManager>()!;

httpServerManager.SetDefaultView<MainView>();
httpServerManager.StartServer();

Console.ReadLine();