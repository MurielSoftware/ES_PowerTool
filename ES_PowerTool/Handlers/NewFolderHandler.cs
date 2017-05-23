using Desktop.Shared.Core.Navigations;
using Desktop.App.Core.Handlers;
using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core;

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
