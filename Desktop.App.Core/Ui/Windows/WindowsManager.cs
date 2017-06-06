using Desktop.App.Core;
using Desktop.App.Core.ModelViews;
using Desktop.Shared.Core;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Desktop.App.Core.Ui.Windows
{
    public class WindowsManager : Singleton<WindowsManager>
    {
        public T ShowDialog<T>(BaseModelView modelView = null) where T : Window
        {
            T window = Activator.CreateInstance<T>();
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = modelView;
            window.Show();
            return window;
        }

        public bool ShowWizard<T>(BaseModelView modelView = null) where T : Wizard
        {
            T wizard = Activator.CreateInstance<T>();
            wizard.Owner = Application.Current.MainWindow;
            wizard.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wizard.DataContext = modelView;
            wizard.CreateUi();
            bool? dialogResult = wizard.ShowDialog();
            if(dialogResult.HasValue)
            {
                return dialogResult.Value;
            }
            return false;
        }

        public void ShowDialogThreadSafe(Window window, BaseModelView modelView = null)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = modelView;
            //window.ShowDialog();
            //wizard.CreateUi();
            //bool? dialogResult = wizard.ShowDialog();
            window.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(window.Show));
            //if (dialogResult.HasValue)
            //{
            //    return dialogResult.Value;
            //}
            //return false;
        }

        //private void ShowDialog(Window window)
        //{
        //    window.ShowDialog();
        //}
    }
}
