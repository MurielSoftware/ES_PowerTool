using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.App.Core.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> _methodToExecute;
        private Func<object, bool> _canExecuteEvaluator;
        //private Action<PublishEvent> onDelete;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> methodToExecute, Func<object, bool> canExecuteEvaluator)
        {
            _methodToExecute = methodToExecute;
            _canExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action<object> methodToExecute)
            : this(methodToExecute, null)
        {
        }

        //public RelayCommand(Action<PublishEvent> onDelete)
        //{
        //    this.onDelete = onDelete;
        //}

        public virtual bool CanExecute(object parameter)
        {
            return _canExecuteEvaluator == null || _canExecuteEvaluator.Invoke(parameter);
        }

        public virtual void Execute(object parameter)
        {
            _methodToExecute.Invoke(parameter);
        }
    }
}
