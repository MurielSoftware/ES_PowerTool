using Desktop.Ui.Core.ModelViews;
using Desktop.Ui.Core.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Ui.Core.Jobs
{
    public class LongRunningJob<T>
    {
        private ProgressWindow _progressWindow;
        private ProgressWindowModelView _progressWindowModelView;
        private Action<T> _action;

        public LongRunningJob(Action<T> action, string title)
        {
            _action = action;
            _progressWindowModelView = new ProgressWindowModelView();
            _progressWindowModelView.Title = title;
            _progressWindowModelView.CurrentStep = string.Empty;
            _progressWindowModelView.CurrentProgress = 0;

            _progressWindow = WindowsManager.GetInstance().ShowDialog<ProgressWindow>();
            _progressWindow.DataContext = _progressWindowModelView;
        }

        public async void Execute(T parameter)
        {
            _progressWindow.Show();
            //_action.Invoke(parameter);
            await AsyncExecute(parameter);
            _progressWindow.Close();
        }

        private async Task AsyncExecute(T parameter)
        {
            await Task.Factory.StartNew(() => _action.Invoke(parameter));
        }
    }
}
