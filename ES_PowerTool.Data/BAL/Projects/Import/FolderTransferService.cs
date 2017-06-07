using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Dtos.OOE;

namespace ES_PowerTool.Data.BAL.Projects.Import
{
    public class FolderTransferService : ProjectTransferService, ITransferService<ProjectDto>
    {
        public void DoWork(Connection connection, ProjectDto projectDto)
        {
            PersistEntities<Folder, FolderDto>(connection, projectDto.Id, projectDto.CsvFolders);
        }

        public string GetMessage()
        {
            return "Importing folders...";
        }
    }
}
