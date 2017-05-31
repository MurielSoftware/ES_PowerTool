using Desktop.App.Core.Commands;
using Desktop.App.Core.ModelViews;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core.Navigations.Generate;
using ES_PowerTool.Shared.Services.Generate;
using ES_PowerTool.Ui.Windows;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ES_PowerTool.ModelViews
{
    public class GenerateCodeWindowModelView : BaseModelView
    {
        public GenerateCodeTypeTreeNavigationItem ItemToGenerate { get; private set; }
        public string DtoGenerated { get; set; }
        public string EntityGenerated { get; set; }

        public bool IsThreadRunning { get; private set; }
        public ICommand LoadCommand { get; private set; }
        public ICommand GenerateCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        private TreeNavigationItem _selectedTreeNavigationItem;
        private IGenerateCodeService _generateCodeService;

        public GenerateCodeWindowModelView(TreeNavigationItem selectedTreeNavigationItem) 
            : base("GenerateCodeWindowModelView")
        {
            _selectedTreeNavigationItem = selectedTreeNavigationItem;
            _generateCodeService = ServiceActivator.Get<IGenerateCodeService>();

            LoadCommand = new RelayCommand(OnLoadCommand);
            GenerateCommand = new RelayCommand(OnGenerateCommand, x => !IsThreadRunning);
            CloseCommand = new RelayCommand(OnCloseCommand, x => !IsThreadRunning);
        }

        private void OnLoadCommand(object obj)
        {
            DoOnLoad();
        }

        private async void DoOnLoad()
        {
            IsThreadRunning = true;
            OnPropertyChanged(() => IsThreadRunning);
            await Task.Run(() =>
            {
                ItemToGenerate = _generateCodeService.GetTypeToGenerate(_selectedTreeNavigationItem.Id);
            }).ContinueWith((x) =>
            {
                IsThreadRunning = false;
                OnPropertyChanged(() => ItemToGenerate);
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
                ItemToGenerate = _generateCodeService.Generate(ItemToGenerate);
            }).ContinueWith((x) =>
            {
                IsThreadRunning = false;
                OnPropertyChanged(() => ItemToGenerate);
                OnPropertyChanged(() => IsThreadRunning);
            });
        }



        private void OnCloseCommand(object obj)
        {
            ((GenerateCodeWindow)obj).Close();
        }
    }
}
