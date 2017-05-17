using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;

namespace ES_PowerTool.Data.BAL
{
    public class FolderCRUDService : GenericCRUDService<FolderDto, Folder>, IFolderCRUDService
    {
        public FolderCRUDService(Connection connection) 
            : base(connection)
        {
        }
    }
}
