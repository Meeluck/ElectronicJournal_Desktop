using System;
using System.Windows.Input;

namespace ElectronicJournal_Desktop.Infrastructure
{
    //Класс, отвечающий за проверку возможности выполения команды
    class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }
        //Функция CanExecute возвращает true, если команда включена и доступна для использования, и false, если команда отключена
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute.Invoke(parameter);
        }
        // Событие CanExecuteChanged вызывается при изменении состояния команды
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

        //Метод Execute предназначен для хранения логики команды.
        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
