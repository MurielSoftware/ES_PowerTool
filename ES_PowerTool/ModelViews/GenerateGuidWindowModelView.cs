using Desktop.App.Core.Commands;
using Desktop.App.Core.ModelViews;
using Desktop.Shared.Core;
using ES_PowerTool.Shared.Dtos.Generate;
using ES_PowerTool.Shared.Services.Generate;
using ES_PowerTool.Ui.Windows;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ES_PowerTool.ModelViews
{
    public class GenerateGuidWindowModelView : BaseModelView
    {
        public bool IsThreadRunning { get; private set; }
        public GenerateGuidDto GenerateGuidDto { get; private set; }

        public ICommand GenerateCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        private IGenerateGuidService _generateGuidService;

        public GenerateGuidWindowModelView() 
            : base("GenerateGuidWindowModelView")
        {
            _generateGuidService = ServiceActivator.Get<IGenerateGuidService>();
            GenerateGuidDto = new GenerateGuidDto();
            CloseCommand = new RelayCommand(OnCloseCommand);
            GenerateCommand = new RelayCommand(OnGenerateCommand, x => !IsThreadRunning);
        }

        private void OnCloseCommand(object obj)
        {
            ((GenerateGuidWindow)obj).Close();
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
                GenerateGuidDto = _generateGuidService.Generate(GenerateGuidDto);
            }).ContinueWith((x) =>
            {
                IsThreadRunning = false;
                OnPropertyChanged(() => GenerateGuidDto);
                OnPropertyChanged(() => IsThreadRunning);
            });
        }
    }
}
