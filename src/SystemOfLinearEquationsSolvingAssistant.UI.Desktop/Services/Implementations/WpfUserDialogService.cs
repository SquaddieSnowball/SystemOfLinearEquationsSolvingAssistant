using Microsoft.Win32;
using System.Windows;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services.Implementations;

internal sealed class WpfUserDialogService : IUserDialogService
{
    public void ShowInformationMessage(string message, string title = "Information") =>
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);

    public void ShowWarningMessage(string message, string title = "Warning") =>
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);

    public void ShowErrorMessage(string message, string title = "Error") =>
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);

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