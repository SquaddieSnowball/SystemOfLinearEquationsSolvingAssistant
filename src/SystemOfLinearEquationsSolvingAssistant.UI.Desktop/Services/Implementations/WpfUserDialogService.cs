using Microsoft.Win32;
using System.Windows;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services.Implementations;

/// <summary>
/// Provides methods used to interact with the user.
/// </summary>
internal sealed class WpfUserDialogService : IUserDialogService
{
    /// <summary>
    /// Shows an information message.
    /// </summary>
    /// <param name="message">Message to show.</param>
    /// <param name="title">Title of the message.</param>
    public void ShowInformationMessage(string message, string title = "Information") =>
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);

    /// <summary>
    /// Shows a warning message.
    /// </summary>
    /// <param name="message">Message to show.</param>
    /// <param name="title">Title of the message.</param>
    public void ShowWarningMessage(string message, string title = "Warning") =>
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);

    /// <summary>
    /// Shows an error message.
    /// </summary>
    /// <param name="message">Message to show.</param>
    /// <param name="title">Title of the message.</param>
    public void ShowErrorMessage(string message, string title = "Error") =>
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);

    /// <summary>
    /// Shows a common dialog that allows the user to specify the name of the file to open.
    /// </summary>
    /// <param name="filter">A filter string that determines what types of files are displayed.</param>
    /// <param name="title">Title of the dialog.</param>
    /// <returns>The full path of the file selected in a dialog.</returns>
    public string ShowOpenFileDialog(string filter = "All files (*.*)|*.*", string title = "Open file")
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = filter,
            Title = title
        };

        _ = openFileDialog.ShowDialog();

        return openFileDialog.FileName;
    }
}