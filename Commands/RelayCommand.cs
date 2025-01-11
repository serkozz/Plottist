using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plottist.Commands
{
    public class RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null) : ICommand
    {
        private readonly Action<object?> _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        private readonly Func<object?, bool> _canExecute = canExecute ?? (_ => true);

        public bool CanExecute(object? parameter) => _canExecute(parameter);

        public void Execute(object? parameter) => _execute(parameter);

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
