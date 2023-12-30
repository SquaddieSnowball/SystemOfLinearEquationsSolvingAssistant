using System.Windows.Input;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Commands.Base;

public abstract class Command : ICommand
{
    void ICommand.Execute(object? parameter)
    {
        if (((ICommand)this).CanExecute(parameter) is true)
            Execute(parameter);
    }

    bool ICommand.CanExecute(object? parameter) => CanExecute(parameter);

    public void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public event EventHandler? CanExecuteChanged;

    protected abstract void Execute(object? parameter);

    protected abstract bool CanExecute(object? parameter);
}