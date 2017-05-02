using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.Ui.Core.Commands
{
    public class AsyncCommand : ICommand
    {
        private bool _isExecuting;
        private Action<object> _methodToExecute;
        private IProgress<int> _progress;
        //private ProgressWindow _progressWindow;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public IProgress<int> GetProgress()
        {
            return _progress;
        }

        public AsyncCommand(Action<object> methodToExecute)
        {
            _methodToExecute = methodToExecute;
            _progress = new Progress<int>(SetProgress);
        }

        public bool CanExecute(object parameter)
        {
            if (_isExecuting)
            {
                return false;
            }
            return true;
        }

        public async void Execute(object parameter)
        {
            OnStart();
            try
            {
                await AsyncExecute(parameter);
            }
            finally
            {
                OnFinish();
            }
        }

        private async Task AsyncExecute(object parameter)
        {
            await Task.Factory.StartNew(() => _methodToExecute.Invoke(parameter));
        }

        protected virtual void OnStart()
        {
            _isExecuting = true;
            //     _progressWindow = WindowsManager.GetInstance().ShowWindow<ProgressWindow>();
            //     _progressWindow.SetIndeterminate(true);
        }

        protected virtual void OnFinish()
        {
            _isExecuting = false;
            //   _progressWindow.Close();
        }

        protected virtual void SetProgress(int value)
        {
            // _progressWindow.UpdateProgress(value);
        }
    }
}
