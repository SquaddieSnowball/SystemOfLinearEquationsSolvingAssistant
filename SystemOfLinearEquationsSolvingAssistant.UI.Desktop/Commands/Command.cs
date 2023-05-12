using System;
using System.Windows.Input;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Commands;

internal abstract class Command : ICommand
{
    private bool _isExecutable = true;

    public bool IsExecutable
    {
        get => _isExecutable;
        set
        {
            if (value == _isExecutable)
                return;

            _isExecutable = value;

            CommandManager.InvalidateRequerySuggested();
        }
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    void ICommand.Execute(object? parameter)
    {
        if (((ICommand)this).CanExecute(parameter) is true)
            Execute(parameter);
    }

    bool ICommand.CanExecute(object? parameter) => _isExecutable && CanExecute(parameter);

    protected abstract void Execute(object? parameter);

    protected abstract bool CanExecute(object? parameter);
}