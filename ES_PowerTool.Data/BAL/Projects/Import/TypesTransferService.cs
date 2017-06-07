using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Dtos.OOE.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Data.BAL.Projects.Import
{
    public class TypesTransferService : ProjectTransferService, ITransferService<ProjectDto>
    {
        public void DoWork(Connection connection, ProjectDto projectDto)
        {
            PersistEntities<CompositeType, CompositeTypeDto>(connection, projectDto.Id, projectDto.CsvTypes, "COM");
            PersistEntities<PrimitiveType, PrimitiveTypeDto>(connection, projectDto.Id, projectDto.CsvTypes, "PRI");
        }

        public string GetMessage()
        {
            return "Importing types...";
        }
    }
}
