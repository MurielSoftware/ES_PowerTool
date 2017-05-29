using Desktop.Shared.Core.Services;
using ES_PowerTool.Shared.Dtos.OOE;

namespace ES_PowerTool.Shared.Services.OOE
{
    public interface IFolderCRUDService : ICRUDService<FolderDto>, IMoveAwareCRUDService
    {
    }
}
