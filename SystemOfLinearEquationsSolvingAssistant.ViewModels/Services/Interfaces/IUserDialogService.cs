namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Interfaces;

public interface IUserDialogService
{
    void ShowInformationMessage(string message, string title = "Information");

    void ShowWarningMessage(string message, string title = "Warning");

    void ShowErrorMessage(string message, string title = "Error");

    string ShowOpenFileDialog(string filter = "All files (*.*)|*.*", string title = "Open file");
}