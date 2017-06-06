using Desktop.App.Core.Ui.Windows;
using Desktop.App.Core.ModelViews;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desktop.App.Core.Jobs
{
    public class LongRunningJob<T>
    {
        private ProgressWindow _progressWindow;
        private ProgressWindowModelView _progressWindowModelView;
        private Action<T> _action;
        private List<Action<T>> _afterAction = new List<Action<T>>();
            
        public LongRunningJob(Action<T> action, string title)
        {
            _action = action;
            _progressWindowModelView = new ProgressWindowModelView();
            _progressWindowModelView.Title = title;

            _progressWindow = WindowsManager.GetInstance().ShowDialog<ProgressWindow>();
            _progressWindow.DataContext = _progressWindowModelView;
        }

        public void AddAction(Action<T> action)
        {
            _afterAction.Add(action);
        }

        public async void Execute(T parameter)
        {
            _progressWindow.Show();
            await AsyncExecute(parameter);
            _progressWindow.Close();
        }

        private async Task AsyncExecute(T parameter)
        {
            Task task = Task.Factory.StartNew(() => _action.Invoke(parameter));
            foreach(Action<T> action in _afterAction)
            {
                await task.ContinueWith(t => action.Invoke(parameter));
            }
            await task;
        }
    }
}
