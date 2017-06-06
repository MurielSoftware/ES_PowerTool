using Desktop.Shared.Core.Navigations;
using Desktop.App.Core.Commands;
using Desktop.App.Core.Handlers;
using Desktop.App.Core.ModelViews;
using ES_PowerTool.Handlers;
using System.Collections.Generic;
using System.Windows.Input;
using Desktop.App.Core.Ui.Windows;
using ES_PowerTool.Ui.Windows;
using ES_PowerTool.Settings;

namespace ES_PowerTool.ModelViews
{
    public class MainWindowModelView : BaseModelView
    {
        public ICommand NewProjectCommand { get; private set; }
        public ICommand OpenProjectCommand { get; private set; }
        public ICommand CloseProjectCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand GenerateCSVCommand { get; private set; }
        public ICommand GenerateLiquibaseCommand { get; private set; }
        public ICommand GenerateGuidCommand { get; private set; }
        public ICommand AboutCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }

        public MainWindowModelView() 
            : base(typeof(MainWindowModelView).Name)
        {
            NewProjectCommand = new RelayCommand(OnNewProjectCommand, x => !ProjectProvider.GetInstance().IsProjectActive());
            OpenProjectCommand = new RelayCommand(OnOpenProjectCommand, x => !ProjectProvider.GetInstance().IsProjectActive());
            CloseProjectCommand = new RelayCommand(OnCloseProjectCommand, x => ProjectProvider.GetInstance().IsProjectActive());
            ExitCommand = new RelayCommand(OnExitCommand);
            GenerateCSVCommand = new RelayCommand(OnGenerateCSVCommand, x => ProjectProvider.GetInstance().IsProjectActive() && ModelViewsUtil.IsType(x, NavigationType.PROJECT));
            GenerateLiquibaseCommand = new RelayCommand(OnGenerateLiquibaseCommand, x => ProjectProvider.GetInstance().IsProjectActive() && ModelViewsUtil.IsType(x, NavigationType.PROJECT));
            GenerateGuidCommand = new RelayCommand(OnGenerateGuidCommand);
            SettingsCommand = new RelayCommand(OnSettingsCommand, x => ProjectProvider.GetInstance().IsProjectActive());
            AboutCommand = new RelayCommand(OnAboutCommand);            
        }

        private void OnNewProjectCommand(object obj)
        {
            HandlerExecutor.Execute<NewProjectHandler>(null);
        }

        private void OnOpenProjectCommand(object obj)
        {
            HandlerExecutor.Execute<OpenProjectHandler>(null);
        }

        private void OnCloseProjectCommand(object obj)
        {
            HandlerExecutor.Execute<CloseProjectHandler>(null);
        }

        private void OnExitCommand(object obj)
        {
            System.Environment.Exit(1);
        }

        private void OnGenerateCSVCommand(object obj)
        {
            HandlerExecutor.Execute<GenerateCSVHandler>(ExecutionEvent.Create(obj as List<TreeNavigationItem>));
        }

        private void OnGenerateLiquibaseCommand(object obj)
        {
            HandlerExecutor.Execute<GenerateLiquibaseHandler>(ExecutionEvent.Create(obj as List<TreeNavigationItem>));
        }

        private void OnGenerateGuidCommand(object obj)
        {
            WindowsManager.GetInstance().ShowDialog<GenerateGuidWindow>(new GenerateGuidWindowModelView());
        }

        private void OnSettingsCommand(object obj)
        {
            WindowsManager.GetInstance().ShowDialog<SettingsWindow>(new SettingsModelView());
        }

        private void OnAboutCommand(object obj)
        {
            WindowsManager.GetInstance().ShowDialog<AboutWindow>(new AboutWindowModelView());
        }
    }
}
