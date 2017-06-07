using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Utils;
using ES_PowerTool.Shared.CSV;
using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.BAL.Projects.Import
{
    public class SuperTypeTransferService : ProjectTransferService, ITransferService<ProjectDto>
    {
        public void DoWork(Connection connection, ProjectDto projectDto)
        {
            GenericRepository genericRepository = new GenericRepository(connection);
            foreach (CSVRow row in projectDto.CsvTypeType.GetValues())
            {
                CSVValue subTypeIdValue = projectDto.CsvTypeType.GetValueToColumn(row, "SUB_TYPE_ID");
                CSVValue superTypeIdValue = projectDto.CsvTypeType.GetValueToColumn(row, "SUPER_TYPE_ID");
                Guid subTypeId = Converter.ConvertValue<Guid>(subTypeIdValue.GetValue());
                Guid superTypeId = Converter.ConvertValue<Guid>(superTypeIdValue.GetValue());

                CompositeType subCompositeType = genericRepository.Find<CompositeType>(subTypeId);
                CompositeType superCompositeType = genericRepository.Find<CompositeType>(superTypeId);
                if (subCompositeType.SuperTypes == null)
                {
                    subCompositeType.SuperTypes = new List<CompositeType>();
                }
                subCompositeType.SuperTypes.Add(superCompositeType);
                genericRepository.Persist<CompositeType>(subCompositeType);
            }
        }

        public string GetMessage()
        {
            return "Initialize super types...";
        }
    }
}
