using Desktop.App.Core.Commands;
using Desktop.App.Core.ModelViews;
using ES_PowerTool.Ui.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ES_PowerTool.ModelViews
{
    public class GenerateGuidWindowModelView : BaseModelView
    {
        public bool IsThreadRunning { get; private set; }
        public string GeneratedGuids { get; private set; }

        public ICommand CloseCommand { get; private set; }

        public GenerateGuidWindowModelView() 
            : base("GenerateGuidWindowModelView")
        {
            CloseCommand = new RelayCommand(OnCloseCommand);
        }

        private void OnCloseCommand(object obj)
        {
            ((GenerateGuidWindow)obj).Close();
        }
    }
}
