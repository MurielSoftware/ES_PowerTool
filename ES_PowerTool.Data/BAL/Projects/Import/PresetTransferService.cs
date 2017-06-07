using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Dtos.OOE;
using ES_PowerTool.Shared.Dtos.OOE.Presets;

namespace ES_PowerTool.Data.BAL.Projects.Import
{
    public class PresetTransferService : ProjectTransferService, ITransferService<ProjectDto>
    {
        public void DoWork(Connection connection, ProjectDto projectDto)
        {
            PersistEntities<Preset, PresetDto>(connection, projectDto.Id, projectDto.CsvPresets);
        }

        public string GetMessage()
        {
            return "Importing presets...";
        }
    }
}
