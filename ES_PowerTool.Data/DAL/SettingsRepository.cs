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
        public SettingsRepository(Connection connection) 
            : base(connection)
        {
        }

        public List<Settings> FindLiquibaseDataTypeConversion()
        {
            return GetContext().Set<Settings>()
                .Where(x => x.Group == SettingsGroup.LIQUIBASE_CONVERT_DATA_TYPE)
                .ToList();
        }
    }
}
