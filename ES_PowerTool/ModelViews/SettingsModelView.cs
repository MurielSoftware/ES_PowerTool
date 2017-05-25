using Desktop.App.Core.Commands;
using Desktop.App.Core.ModelViews;
using ES_PowerTool.Shared.Dtos.Settings;
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
        public SettingsDto SettingsDto { get; set; }

        public ICommand LoadCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        public SettingsModelView()
            : base("SettingsModelView")
        {
            LoadCommand = new RelayCommand(OnLoadCommand);
            SaveCommand = new RelayCommand(OnSaveCommand);
            CloseCommand = new RelayCommand(OnCloseCommand);
        }

        private void OnLoadCommand(object obj)
        {
            SettingsDto = new SettingsDto();
            OnPropertyChanged(() => SettingsDto);
        }

        private void OnSaveCommand(object obj)
        {
        }

        private void OnCloseCommand(object obj)
        {
            ((SettingsWindow)obj).Close();
        }
    }
}
