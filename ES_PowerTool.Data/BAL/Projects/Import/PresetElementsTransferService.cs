using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Dtos.OOE.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.BAL.Projects.Import
{
    public class PresetElementsTransferService : ProjectTransferService, ITransferService<ProjectDto>
    {
        public void DoWork(Connection connection, ProjectDto projectDto)
        {
            PersistEntities<CompositePresetElement, CompositePresetElementDto>(connection, projectDto.Id, projectDto.CsvPresetElements);
        }

        public string GetMessage()
        {
            return "Importing preset elements...";
        }
    }
}
