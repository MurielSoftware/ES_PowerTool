using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Dtos.OOE;
using ES_PowerTool.Shared.CSV;
using Desktop.Shared.Utils;
using Desktop.Data.Core.DAL;

namespace ES_PowerTool.Data.BAL.Projects.Import
{
    public class DefaultPresetTransferService : ProjectTransferService, ITransferService<ProjectDto>
    {
        public void DoWork(Connection connection, ProjectDto projectDto)
        {
            GenericRepository genericRepository = new GenericRepository(connection);
            foreach (CSVRow row in projectDto.CsvDefaultPreset.GetValues())
            {
                CSVValue typeIdValue = projectDto.CsvDefaultPreset.GetValueToColumn(row, "ID");
                CSVValue defaultPresetIdValue = projectDto.CsvDefaultPreset.GetValueToColumn(row, "DEFAULT_PRESET_ID");
                Guid typeId = Converter.ConvertValue<Guid>(typeIdValue.GetValue());
                Guid defaultPresetId = Converter.ConvertValue<Guid>(defaultPresetIdValue.GetValue());

                CompositeType compositeType = genericRepository.Find<CompositeType>(typeId);
                compositeType.DefaultPresetId = defaultPresetId;
                genericRepository.Persist<CompositeType>(compositeType);
            }
        }

        public string GetMessage()
        {
            return "Initialize default presets...";
        }
    }
}
