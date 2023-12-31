namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

/// <summary>
/// Provides methods used to interact with the user.
/// </summary>
public interface IUserDialogService
{
    /// <summary>
    /// Shows an information message.
    /// </summary>
    /// <param name="message">Message to show.</param>
    /// <param name="title">Title of the message.</param>
    void ShowInformationMessage(string message, string title = "Information");

    /// <summary>
    /// Shows a warning message.
    /// </summary>
    /// <param name="message">Message to show.</param>
    /// <param name="title">Title of the message.</param>
    void ShowWarningMessage(string message, string title = "Warning");

    /// <summary>
    /// Shows an error message.
    /// </summary>
    /// <param name="message">Message to show.</param>
    /// <param name="title">Title of the message.</param>
    void ShowErrorMessage(string message, string title = "Error");

    /// <summary>
    /// Shows a common dialog that allows the user to specify the name of the file to open.
    /// </summary>
    /// <param name="filter">A filter string that determines what types of files are displayed.</param>
    /// <param name="title">Title of the dialog.</param>
    /// <returns>The full path of the file selected in a dialog.</returns>
    string ShowOpenFileDialog(string filter = "All files (*.*)|*.*", string title = "Open file");
}