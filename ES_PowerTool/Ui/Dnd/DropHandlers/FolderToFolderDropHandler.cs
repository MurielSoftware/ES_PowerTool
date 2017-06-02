using Desktop.App.Core.Ui.Dnd.DropHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Navigations;
using ES_PowerTool.Shared.Services.OOE;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Services;
using Desktop.App.Core.Events.Publishing;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Ui.Dnd.DropHandlers
{
    public class FolderToFolderDropHandler : IDropHandler
    {
        public void Drop(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            Connection.GetInstance().StartTransaction();
            IMoveAwareCRUDService moveAwareCRUDService = ServiceActivator.Get<IFolderCRUDService>();
            moveAwareCRUDService.Move(draggedTreeNavigationItem.Id, targetTreeNavigationItem.Id);
            Connection.GetInstance().EndTransaction();
            Publisher.GetInstance().Publish(PublishEvent.CreateDeletionEvent(draggedTreeNavigationItem.Id, draggedTreeNavigationItem.GetParentId()));
            Publisher.GetInstance().Publish(PublishEvent.CreateCreationEvent(draggedTreeNavigationItem.Id, targetTreeNavigationItem.GetParentId()));
        }

        public bool IsActive(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            return NavigationType.FOLDER.Equals(draggedTreeNavigationItem.Type) &&
                (NavigationType.FOLDER.Equals(targetTreeNavigationItem.Type) || NavigationType.PROJECT.Equals(targetTreeNavigationItem.Type));
        }
    }
}
