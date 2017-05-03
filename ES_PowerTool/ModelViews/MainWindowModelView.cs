using Desktop.Ui.Core.Commands;
using Desktop.Ui.Core.Handlers;
using Desktop.Ui.Core.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ES_PowerTool.ModelViews
{
    public class MainWindowModelView : BaseModelView
    {
        public ICommand ImportCommand { get; private set; }

        public MainWindowModelView() 
            : base(typeof(MainWindowModelView).Name)
        {
            ImportCommand = new RelayCommand(OnImportCommand);
        }

        private void OnImportCommand(object obj)
        {
         //   HandlerExecutor.Execute(typeof(OpenWindowHandler<>), typeof(SettingsWindow), ExecutionEvent.Create());
        }
    }
}
