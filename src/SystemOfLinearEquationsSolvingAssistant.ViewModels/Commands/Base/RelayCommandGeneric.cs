using System.ComponentModel;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Commands.Base;

/// <summary>
/// Represents a generic relay command.
/// </summary>
/// <typeparam name="T">Command parameter type.</typeparam>
public sealed class RelayCommandGeneric<T> : CommandGeneric<T>, IDisposable
{
    private readonly Type _commandType = typeof(RelayCommandGeneric<T>);
    private readonly Action<T?> _execute;
    private readonly Func<T?, bool>? _canExecute;
    private INotifyPropertyChanged? _observableObject;
    private readonly IEnumerable<string>? _observableProperties;
    private bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of <see cref="RelayCommandGeneric{T}"/> with the specified 
    /// execution delegates, observable object and observable properties.
    /// </summary>
    /// <param name="execute">The delegate used to execute the command.</param>
    /// <param name="canExecute">The delegate used to determine whether the command can be executed.</param>
    /// <param name="observableObject">An object instance for which changes to properties will be observed.</param>
    /// <param name="observableProperties">Properties for which changes will be observed.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public RelayCommandGeneric(Action<T?> execute, Func<T?, bool>? canExecute = default,
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

    /// <summary>
    /// Execute the command.
    /// </summary>
    /// <param name="parameter">Data used by the command.</param>
    /// <exception cref="ObjectDisposedException"></exception>
    protected override void Execute(object? parameter)
    {
        if (_isDisposed is true)
            throw new ObjectDisposedException(_commandType.Name, "Cannot access a disposed command.");

        _execute((T?)parameter);
    }

    /// <summary>
    /// Determines whether the command can be executed.
    /// </summary>
    /// <param name="parameter">Data used by the command.</param>
    /// <returns><see langword="true"/> if this command can be executed; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ObjectDisposedException"></exception>
    protected override bool CanExecute(object? parameter)
    {
        if (_isDisposed is true)
            throw new ObjectDisposedException(_commandType.Name, "Cannot access a disposed command.");

        return _canExecute?.Invoke((T?)parameter) ?? true;
    }

    /// <summary>
    /// Releases unmanaged resources used by the current object instance.
    /// </summary>
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