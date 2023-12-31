namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

/// <summary>
/// Provides methods used to manage views.
/// </summary>
public interface IViewManager
{
    /// <summary>
    /// Shows the view.
    /// </summary>
    /// <param name="viewName">The name of the view to show.</param>
    /// <param name="ownerViewName">Owner of the view.</param>
    /// <param name="isDialogMode">Determines whether the view should be shown in dialog mode.</param>
    void ShowView(string viewName, string? ownerViewName = default, bool isDialogMode = false);

    /// <summary>
    /// Closes the view.
    /// </summary>
    /// <param name="viewName">The name of the view to close.</param>
    void CloseView(string viewName);
}