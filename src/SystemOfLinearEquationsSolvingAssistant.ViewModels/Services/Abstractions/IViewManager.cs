namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

public interface IViewManager
{
    void ShowView(string viewName, string? ownerViewName = default, bool isDialogMode = false);

    void CloseView(string viewName);
}