using Desktop.App.Core;
using Desktop.Shared.Core;
using ES_PowerTool.Shared.Dtos.Settings;
using ES_PowerTool.Shared.Services.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Settings
{
    public class SettingsProvider : Singleton<SettingsProvider>
    {
        private ISettingsCRUDService _settingsCRUDService;
        private SettingsDto _settingsDto;

        public SettingsDto GetSettings()
        {
            if(_settingsDto == null)
            {
                _settingsDto = ReadSettings();
            }
            return _settingsDto;
        }

        public void SetSettings(SettingsDto settingsDto)
        {
            _settingsDto = settingsDto;
        }

        protected override void Init()
        {
            base.Init();
            _settingsCRUDService = ServiceActivator.Get<ISettingsCRUDService>();
        }

        private SettingsDto ReadSettings()
        {
            return _settingsCRUDService.Read(Guid.Empty);
        }
    }
}
