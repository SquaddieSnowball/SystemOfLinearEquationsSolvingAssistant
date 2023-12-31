using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModels.Base;

/// <summary>
/// Serves as the base class for view models.
/// </summary>
public abstract class ViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Sets the property value.
    /// </summary>
    /// <typeparam name="T">Property type.</typeparam>
    /// <param name="field">Property field.</param>
    /// <param name="value">Property value.</param>
    /// <param name="propertyName">Property name.</param>
    /// <returns><see langword="true"/> if the property value has been set; otherwise, <see langword="false"/>.</returns>
    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = default)
    {
        if (Equals(field, value) is true)
            return false;

        field = value;
        OnPropertyChanged(propertyName);

        return true;
    }

    /// <summary>
    /// Represents the method that will handle the event raised when a property is changed.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = default) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}