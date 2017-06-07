using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Dtos.OOE.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Shared.Dtos.OOE.Elements;

namespace ES_PowerTool.Data.BAL.Projects.Import
{
    public class CompositeTypeElementsTransferService : ProjectTransferService, ITransferService<ProjectDto>
    {
        public void DoWork(Connection connection, ProjectDto projectDto)
        {
            PersistEntities<CompositeTypeElement, CompositeTypeElementDto>(connection, projectDto.Id, projectDto.CsvTypeElements);
        }

        public string GetMessage()
        {
            return "Importing composite type elements...";
        }
    }
}
