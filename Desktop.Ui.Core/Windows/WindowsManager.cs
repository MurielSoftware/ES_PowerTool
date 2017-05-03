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
        public T ShowDialog<T>() where T : Window
        {
            T window = Activator.CreateInstance<T>();
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
            return window;
        }

        public T ShowWizard<T>(BaseModelView baseModelView) where T : Wizard
        {
            T wizard = Activator.CreateInstance<T>();
            wizard.Owner = Application.Current.MainWindow;
            wizard.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wizard.DataContext = baseModelView;
            wizard.CreateUi();
            wizard.ShowDialog();
            return wizard;
        }
    }
}
