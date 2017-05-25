using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Dtos.Settings;
using ES_PowerTool.Shared.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Data.BAL.OOE.Set
{
    public class SettingsCRUDService : GenericCRUDService<SettingsDto, Settings>, ISettingsCRUDService
    {
        public SettingsCRUDService(Connection connection) 
            : base(connection)
        {
        }
    }
}
