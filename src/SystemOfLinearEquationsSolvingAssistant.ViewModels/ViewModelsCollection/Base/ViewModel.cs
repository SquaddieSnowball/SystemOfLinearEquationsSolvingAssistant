using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModelsCollection.Base;

public abstract class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = default)
    {
        if (Equals(field, value) is true)
            return false;

        field = value;
        OnPropertyChanged(propertyName);

        return true;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = default) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}