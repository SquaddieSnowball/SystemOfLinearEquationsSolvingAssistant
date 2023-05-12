namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services.Interfaces;

internal interface IViewManagerService
{
    void ShowView(string viewName, string? ownerViewName = default, bool isDialogMode = false);

    void CloseView(string viewName);
}