using Desktop.Shared.Core.Navigations;
using Desktop.App.Core.Commands;
using Desktop.App.Core.Handlers;
using Desktop.App.Core.ModelViews;
using ES_PowerTool.Handlers;
using System.Collections.Generic;
using System.Windows.Input;
using Desktop.App.Core.Ui.Windows;

namespace ES_PowerTool.ModelViews
{
    public class MainWindowModelView : BaseModelView
    {
        public ICommand NewProjectCommand { get; private set; }
        public ICommand OpenProjectCommand { get; private set; }
        public ICommand GenerateCommand { get; private set; }
        public ICommand AboutCommand { get; private set; }

        public MainWindowModelView() 
            : base(typeof(MainWindowModelView).Name)
        {
            NewProjectCommand = new RelayCommand(OnNewProjectCommand);
            OpenProjectCommand = new RelayCommand(OnOpenProjectCommand);
            GenerateCommand = new RelayCommand(OnGenerateCommand);
            AboutCommand = new RelayCommand(OnOpenCommand);
        }

        private void OnNewProjectCommand(object obj)
        {
            HandlerExecutor.Execute<NewProjectHandler>(null);
        }

        private void OnOpenProjectCommand(object obj)
        {
            HandlerExecutor.Execute<OpenProjectHandler>(null);
        }

        private void OnGenerateCommand(object obj)
        {
            HandlerExecutor.Execute<GenerateHandler>(ExecutionEvent.Create(obj as List<TreeNavigationItem>));
        }

        private void OnOpenCommand(object obj)
        {
            WindowsManager.GetInstance().ShowDialog<AboutWindow>(new AboutWindowModelView());
        }
    }
}
