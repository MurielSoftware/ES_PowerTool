using Desktop.App.Core.Commands;
using Desktop.App.Core.ModelViews;
using Desktop.Shared.Core;
using ES_PowerTool.Settings;
using ES_PowerTool.Shared.Dtos.Settings;
using ES_PowerTool.Shared.Services.Settings;
using ES_PowerTool.Ui.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ES_PowerTool.ModelViews
{
    public class SettingsModelView : BaseModelView
    {
        public bool IsThreadRunning { get; private set; }
        public SettingsDto SettingsDto { get; set; }

        public ICommand LoadCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        protected ISettingsCRUDService _settingsCRUDService;

        public SettingsModelView()
            : base("SettingsModelView")
        {
            _settingsCRUDService = ServiceActivator.Get<ISettingsCRUDService>();

            LoadCommand = new RelayCommand(OnLoadCommand);
            SaveCommand = new RelayCommand(OnSaveCommand, x => !IsThreadRunning);
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
                SettingsDto = SettingsProvider.GetInstance().GetSettings();
            }).ContinueWith((x) =>
            {
                IsThreadRunning = false;
                OnPropertyChanged(() => SettingsDto);
                OnPropertyChanged(() => IsThreadRunning);
            });
        }

        private async void OnSaveCommand(object obj)
        {
            IsThreadRunning = true;
            OnPropertyChanged(() => IsThreadRunning);
            await Task.Run(() =>
            {
                _settingsCRUDService.Persist(SettingsDto);
            }).ContinueWith((x) =>
            {
                SettingsProvider.GetInstance().SetSettings(SettingsDto);
                IsThreadRunning = false;
                OnPropertyChanged(() => IsThreadRunning);
            });                       
        }

        private void OnCloseCommand(object obj)
        {
            ((SettingsWindow)obj).Close();
        }
    }
}
