using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Data.BAL
{
    public class PresetCRUDService : GenericCRUDService<PresetDto, Preset>, IPresetCRUDService
    {
        public PresetCRUDService(Connection connection) 
            : base(connection)
        {
        }
    }
}
