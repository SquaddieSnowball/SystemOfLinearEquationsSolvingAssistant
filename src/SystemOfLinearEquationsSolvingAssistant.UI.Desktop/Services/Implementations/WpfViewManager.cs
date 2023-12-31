using System.Reflection;
using System.Windows;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModels.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services.Implementations;

/// <summary>
/// Provides methods used to manage views.
/// </summary>
internal sealed class WpfViewManager : IViewManager
{
    private const string ViewsNamespace = "SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Views";
    private const string ViewModelsNamespace = "SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModels";

    /// <summary>
    /// Shows the view.
    /// </summary>
    /// <param name="viewName">The name of the view to show.</param>
    /// <param name="ownerViewName">Owner of the view.</param>
    /// <param name="isDialogMode">Determines whether the view should be shown in dialog mode.</param>
    /// <exception cref="ArgumentException"></exception>
    public void ShowView(string viewName, string? ownerViewName = default, bool isDialogMode = false)
    {
        if (string.IsNullOrEmpty(viewName) is true)
            throw new ArgumentException("View name must not be null or empty.", nameof(viewName));

        Type viewType = GetViewType(viewName) ??
            throw new ArgumentException("The view with the specified name does not exist.", nameof(viewName));
        Type viewModelType = GetViewModelType(viewName) ??
            throw new ArgumentException("The view model for the view with the specified name does not exist.", nameof(viewName));

        Window view = (Window)DependenciesContainer.Resolve(viewType)!;
        ViewModel viewModel = (ViewModel)DependenciesContainer.Resolve(viewModelType)!;

        view.DataContext = viewModel;

        if (string.IsNullOrEmpty(ownerViewName) is false)
        {
            Type ownerViewType = GetViewType(ownerViewName) ??
                throw new ArgumentException("The view with the specified name does not exist.", nameof(ownerViewName));

            Window ownerView = (Window)DependenciesContainer.Resolve(ownerViewType)!;

            view.Owner = ownerView;
        }

        if (isDialogMode is false)
            view.Show();
        else
            _ = view.ShowDialog();
    }

    /// <summary>
    /// Closes the view.
    /// </summary>
    /// <param name="viewName">The name of the view to close.</param>
    /// <exception cref="ArgumentException"></exception>
    public void CloseView(string viewName)
    {
        if (string.IsNullOrEmpty(viewName) is true)
            throw new ArgumentException("View name must not be null or empty.", nameof(viewName));

        Type viewType = GetViewType(viewName) ??
            throw new ArgumentException("The view with the specified name does not exist.", nameof(viewName));

        foreach (Window window in Application.Current.Windows)
        {
            if (window.GetType().Equals(viewType) is true)
            {
                window.Close();

                return;
            }
        }

        throw new ArgumentException("The view with the specified name is not currently shown.", nameof(viewName));
    }

    private static Type? GetViewType(string viewName)
    {
        IEnumerable<Type> viewTypes =
            Assembly.GetAssembly(typeof(App))!.GetTypes().Where(t => t.Namespace?.Contains(ViewsNamespace) ?? false);

        foreach (Type viewType in viewTypes)
        {
            if (viewType.Name.Equals($"{viewName}View", StringComparison.Ordinal) is true)
                return viewType;
        }

        return default;
    }

    private static Type? GetViewModelType(string viewName)
    {
        IEnumerable<Type> viewModelTypes =
            Assembly.GetAssembly(typeof(ViewModel))!.GetTypes().Where(t => t.Namespace?.Contains(ViewModelsNamespace) ?? false);

        foreach (Type viewModelType in viewModelTypes)
        {
            if (viewModelType.Name.Equals($"{viewName}ViewModel", StringComparison.Ordinal) is true)
                return viewModelType;
        }

        return default;
    }
}