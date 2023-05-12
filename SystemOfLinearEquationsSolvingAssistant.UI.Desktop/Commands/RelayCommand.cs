using System;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Commands;

internal sealed class RelayCommand : Command
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = default)
    {
        if (execute is null)
            throw new ArgumentNullException(nameof(execute), "The execution delegate must not be null.");

        (_execute, _canExecute) = (_ => execute(), (canExecute is null) ? default : (_ => canExecute()));
    }

    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = default)
    {
        if (execute is null)
            throw new ArgumentNullException(nameof(execute), "The execution delegate must not be null.");

        (_execute, _canExecute) = (execute, canExecute);
    }

    protected override void Execute(object? parameter) => _execute(parameter);

    protected override bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
}