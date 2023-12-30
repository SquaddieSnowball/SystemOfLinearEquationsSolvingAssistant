using System.Windows.Input;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Commands.Base;

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

    public void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public event EventHandler? CanExecuteChanged;

    protected abstract void Execute(object? parameter);

    protected abstract bool CanExecute(object? parameter);
}