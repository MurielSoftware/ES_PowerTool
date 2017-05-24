using Desktop.Shared.Core.Navigations;
using Desktop.App.Core.Handlers;
using Desktop.Shared.Core;
using ES_PowerTool.Shared.Dtos.OOE;

namespace ES_PowerTool.Handlers
{
    public class NewFolderHandler : NewEntityHandler<FolderDto>
    {
        protected override FolderDto CreateNewDto(ExecutionEvent executionEvent)
        {
            FolderDto folderDto = base.CreateNewDto(executionEvent);
            folderDto.State = State.NEW;
            TreeNavigationItem selectedTreeNavigationItem = executionEvent.GetFirstSelectedTreeNavigationItem();
            if(NavigationType.PROJECT.Equals(selectedTreeNavigationItem.Type))
            {
                folderDto.ProjectId = selectedTreeNavigationItem.Id;
                folderDto.ParentId = null;
            }
            else
            {
                folderDto.ProjectId = selectedTreeNavigationItem.ProjectId;
                folderDto.ParentId = selectedTreeNavigationItem.Id;
            }
            return folderDto;
        }
    }
}
