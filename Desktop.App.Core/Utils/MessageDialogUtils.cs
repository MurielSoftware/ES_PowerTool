using Desktop.Ui.I18n;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.App.Core.Utils
{
    public class MessageDialogUtils
    {
        public static void ErrorMessage(string resourceKey, params object[] args)
        {
            MessageBox.Show(ResourceUtils.GetMessage(resourceKey, args), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void WarningMessage(string resourceKey, params object[] args)
        {
            MessageBox.Show(ResourceUtils.GetMessage(resourceKey, args), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void InfoMessage(string resourceKey, params object[] args)
        {
            MessageBox.Show(ResourceUtils.GetMessage(resourceKey, args), "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static MessageBoxResult Question(string resourceKey, params object[] args)
        {
            return MessageBox.Show(ResourceUtils.GetMessage(resourceKey, args), "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}
