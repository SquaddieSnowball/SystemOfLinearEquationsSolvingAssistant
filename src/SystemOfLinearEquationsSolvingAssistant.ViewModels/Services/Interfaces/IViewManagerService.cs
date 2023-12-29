namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Interfaces;

public interface IViewManagerService
{
    void ShowView(string viewName, string? ownerViewName = default, bool isDialogMode = false);

    void CloseView(string viewName);
}