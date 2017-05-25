using Desktop.App.Core.Commands;
using Desktop.App.Core.ModelViews;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core.Services;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ES_PowerTool.ModelViews
{
    public class GenerateLiquibaseWindowModelView : BaseModelView
    {
        public List<GenerateLiquibaseCompositeTypeElementTreeNavigationItem> ItemsToGenerate { get; private set; }
        public string GeneratedLiquibase { get; set; }
        public bool IsThreadRunning { get; private set; }

        public ICommand LoadCommand { get; private set; }
        public ICommand GenerateCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        protected IGenerateLiquibaseService _generateService;
        protected TreeNavigationItem _selectedTreeNavigationItem;

        public GenerateLiquibaseWindowModelView(TreeNavigationItem selectedTreeNavigationItem)
            : base("GenerateLiquibaseWindowModelView")
        {
            _generateService = ServiceActivator.Get<IGenerateLiquibaseService>();
            _selectedTreeNavigationItem = selectedTreeNavigationItem;
            LoadCommand = new RelayCommand(OnLoadCommand);
            GenerateCommand = new RelayCommand(OnGenerateCommand, x => !IsThreadRunning);
            CloseCommand = new RelayCommand(OnCloseCommand, x => !IsThreadRunning);
        }

        private void OnLoadCommand(object obj)
        {
            DoLoadItemsToGenerate();
        }

        private async void DoLoadItemsToGenerate()
        {
            IsThreadRunning = true;
            OnPropertyChanged(() => IsThreadRunning);
            await Task.Run(() =>
            {
                ItemsToGenerate = _generateService.GetCompositeTypeElementsToGenerate(_selectedTreeNavigationItem.ProjectId);
            }).ContinueWith((x) =>
            {
                IsThreadRunning = false;
                OnPropertyChanged(() => ItemsToGenerate);
                OnPropertyChanged(() => IsThreadRunning);
            });
        }

        private void OnGenerateCommand(object obj)
        {
            DoGenerate();
        }

        private async void DoGenerate()
        {
            IsThreadRunning = true;
            OnPropertyChanged(() => IsThreadRunning);
            await Task.Run(() =>
            {
                GeneratedLiquibase = _generateService.GenerateCompositeTypeElements(ItemsToGenerate);
            }).ContinueWith((x) =>
            {
                IsThreadRunning = false;
                OnPropertyChanged(() => GeneratedLiquibase);
                OnPropertyChanged(() => IsThreadRunning);
            });
        }

        private void OnCloseCommand(object obj)
        {

        }
    }
}
