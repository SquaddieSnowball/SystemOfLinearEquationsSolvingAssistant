using System.ComponentModel;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Commands.Base;

public sealed class RelayCommand<T> : Command<T>, IDisposable
{
    private readonly Type _commandType = typeof(RelayCommand<T>);
    private readonly Action<T?> _execute;
    private readonly Func<T?, bool>? _canExecute;
    private INotifyPropertyChanged? _observableObject;
    private readonly IEnumerable<string>? _observableProperties;
    private bool _isDisposed;

    public RelayCommand(Action execute, Func<bool>? canExecute = default,
        INotifyPropertyChanged? observableObject = default, IEnumerable<string>? observableProperties = default)
    {
        if (execute is null)
            throw new ArgumentNullException(nameof(execute), "The execution delegate must not be null.");

        (_execute, _canExecute) = (_ => execute(), (canExecute is null) ? default : (_ => canExecute()));

        if (observableObject is not null)
        {
            _observableObject = observableObject;
            _observableProperties = observableProperties;

            _observableObject.PropertyChanged += ObservableObjectOnPropertyChanged;
        }
    }

    public RelayCommand(Action<T?> execute, Func<T?, bool>? canExecute = default,
        INotifyPropertyChanged? observableObject = default, IEnumerable<string>? observableProperties = default)
    {
        if (execute is null)
            throw new ArgumentNullException(nameof(execute), "The execution delegate must not be null.");

        (_execute, _canExecute) = (execute, canExecute);

        if (observableObject is not null)
        {
            _observableObject = observableObject;
            _observableProperties = observableProperties;

            _observableObject.PropertyChanged += ObservableObjectOnPropertyChanged;
        }
    }

    protected override void Execute(object? parameter)
    {
        if (_isDisposed is true)
            throw new ObjectDisposedException(_commandType.Name, "Cannot access a disposed command.");

        _execute((T?)parameter);
    }

    protected override bool CanExecute(object? parameter)
    {
        if (_isDisposed is true)
            throw new ObjectDisposedException(_commandType.Name, "Cannot access a disposed command.");

        return _canExecute?.Invoke((T?)parameter) ?? true;
    }

    public void Dispose()
    {
        if (_isDisposed is false)
        {
            if (_observableObject is not null)
            {
                _observableObject.PropertyChanged -= ObservableObjectOnPropertyChanged;
                _observableObject = default;
            }

            _isDisposed = true;
        }
    }

    private void ObservableObjectOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if ((_observableProperties is null) || (_observableProperties.Contains(e.PropertyName) is true))
            NotifyCanExecuteChanged();
    }
}