using Desktop.Ui.Core.Commands;
using Desktop.Ui.Core.Handlers;
using Desktop.Ui.Core.ModelViews;
using ES_PowerTool.Handlers;
using System.Windows.Input;

namespace ES_PowerTool.ModelViews
{
    public class MainWindowModelView : BaseModelView
    {
        public ICommand NewProjectCommand { get; private set; }

        public MainWindowModelView() 
            : base(typeof(MainWindowModelView).Name)
        {
            NewProjectCommand = new RelayCommand(OnNewProjectCommand);
        }

        private void OnNewProjectCommand(object obj)
        {
            HandlerExecutor.Execute<NewProjectHandler>(null);
        }
    }
}
