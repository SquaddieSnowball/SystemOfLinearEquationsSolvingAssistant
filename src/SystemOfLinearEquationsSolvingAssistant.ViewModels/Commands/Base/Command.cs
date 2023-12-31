using System.Windows.Input;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Commands.Base;

/// <summary>
/// Represents a command.
/// </summary>
public abstract class Command : ICommand
{
    void ICommand.Execute(object? parameter)
    {
        if (((ICommand)this).CanExecute(parameter) is true)
            Execute(parameter);
    }

    bool ICommand.CanExecute(object? parameter) => CanExecute(parameter);

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