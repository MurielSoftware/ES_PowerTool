using Desktop.Ui.Core.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.Ui.Core.Windows
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
    }
}
