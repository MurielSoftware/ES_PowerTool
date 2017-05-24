using Desktop.App.Core.Commands;
using Desktop.App.Core.Ui.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.App.Core.ModelViews
{
    public class AboutWindowModelView : BaseModelView
    {
        public ICommand CloseCommand { get; private set; }

        public AboutWindowModelView() 
            : base("AboutWindowModelView")
        {
            CloseCommand = new RelayCommand(OnCloseCommand);
        }

        private void OnCloseCommand(object obj)
        {
            ((AboutWindow)obj).Close();
        }
    }
}
