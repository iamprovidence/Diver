using System;
using System.Windows.Input;

namespace Diver.Common
{
    public class RelayCommand<T> : ICommand
        where T : class
    {
        private Action<T> _execute;
        private Func<T, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter as T) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter as T);
        }
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            : base(execute, canExecute) { }
    }
}
