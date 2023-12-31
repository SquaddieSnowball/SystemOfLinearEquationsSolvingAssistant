using System.Windows.Input;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Commands.Base;

/// <summary>
/// Represents a generic command.
/// </summary>
/// <typeparam name="T">Command parameter type.</typeparam>
public abstract class CommandGeneric<T> : ICommand
{
    private readonly Type _parameterType = typeof(T);

    void ICommand.Execute(object? parameter)
    {
        if (parameter?.GetType().Equals(_parameterType) is false)
            throw new ArgumentException("The parameter type must match the command parameter type.", nameof(parameter));

        if (((ICommand)this).CanExecute(parameter) is true)
            Execute(parameter);
    }

    bool ICommand.CanExecute(object? parameter)
    {
        if (parameter?.GetType().Equals(_parameterType) is false)
            throw new ArgumentException("The parameter type must match the command parameter type.", nameof(parameter));

        return CanExecute(parameter);
    }

    /// <summary>
    /// Notifies about changes that affect whether or not the command can execute.
    /// </summary>
    public void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Occurs when changes occur that affect whether or not the command can execute.
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Execute the command.
    /// </summary>
    /// <param name="parameter">Data used by the command.</param>
    protected abstract void Execute(object? parameter);

    /// <summary>
    /// Determines whether the command can be executed.
    /// </summary>
    /// <param name="parameter">Data used by the command.</param>
    /// <returns><see langword="true"/> if this command can be executed; otherwise, <see langword="false"/>.</returns>
    protected abstract bool CanExecute(object? parameter);
}