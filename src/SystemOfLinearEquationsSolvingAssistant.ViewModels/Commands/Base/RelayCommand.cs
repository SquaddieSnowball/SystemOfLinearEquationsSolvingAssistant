using System.ComponentModel;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Commands.Base;

/// <summary>
/// Represents a relay command.
/// </summary>
public sealed class RelayCommand : Command, IDisposable
{
    private readonly Type _commandType = typeof(RelayCommand);
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute;
    private INotifyPropertyChanged? _observableObject;
    private readonly IEnumerable<string>? _observableProperties;
    private bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of <see cref="RelayCommand"/> with the specified 
    /// execution delegates, observable object and observable properties.
    /// </summary>
    /// <param name="execute">The delegate used to execute the command.</param>
    /// <param name="canExecute">The delegate used to determine whether the command can be executed.</param>
    /// <param name="observableObject">An object instance for which changes to properties will be observed.</param>
    /// <param name="observableProperties">Properties for which changes will be observed.</param>
    /// <exception cref="ArgumentNullException"></exception>
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

    /// <summary>
    /// Initializes a new instance of <see cref="RelayCommand"/> with the specified 
    /// execution delegates, observable object and observable properties.
    /// </summary>
    /// <param name="execute">The delegate used to execute the command.</param>
    /// <param name="canExecute">The delegate used to determine whether the command can be executed.</param>
    /// <param name="observableObject">An object instance for which changes to properties will be observed.</param>
    /// <param name="observableProperties">Properties for which changes will be observed.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = default,
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

        _execute(parameter);
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

        return _canExecute?.Invoke(parameter) ?? true;
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