using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Data.BAL.Setting;

namespace ES_PowerTool.Data.BAL.Projects.Import
{
    public class SettingsTransferService : ProjectTransferService, ITransferService<ProjectDto>
    {
        public void DoWork(Connection connection, ProjectDto dto)
        {
            SettingsCRUDService settingsCRUDService = new SettingsCRUDService(connection);
            settingsCRUDService.CreateAndPresistDefaultSettings();
        }

        public string GetMessage()
        {
            return "Craete default settings...";
        }
    }
}
