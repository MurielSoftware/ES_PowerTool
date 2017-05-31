using Desktop.Data.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Dtos.Settings;

namespace ES_PowerTool.Data.DAL
{
    public class SettingsRepository : BaseRepository
    {
        internal SettingsRepository(Connection connection) 
            : base(connection)
        {
        }

        internal List<Settings> FindSettingsToGroup(SettingsGroup settingGroup)
        {
            return GetContext().Set<Settings>()
                .Where(x => x.Group == settingGroup)
                .ToList();
        }
    }
}
