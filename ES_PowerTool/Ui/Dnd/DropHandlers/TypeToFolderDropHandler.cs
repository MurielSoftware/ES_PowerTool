using Desktop.App.Core.Ui.Dnd.DropHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Navigations;
using ES_PowerTool.Shared.Services.OOE.Types;
using Desktop.App.Core.Events.Publishing;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Services;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Ui.Dnd.DropHandlers
{
    public class TypeToFolderDropHandler : IDropHandler
    {
        public void Drop(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            IMoveAwareCRUDService moveAwareCRUDService = ServiceActivator.Get<ICompositeTypeCRUDService>();
            Connection.GetInstance().StartTransaction();
            moveAwareCRUDService.Move(draggedTreeNavigationItem.Id, targetTreeNavigationItem.Id);
            Connection.GetInstance().EndTransaction();
            Publisher.GetInstance().Publish(PublishEvent.CreateDeletionEvent(draggedTreeNavigationItem.Id, draggedTreeNavigationItem.GetParentId()));
            Publisher.GetInstance().Publish(PublishEvent.CreateCreationEvent(draggedTreeNavigationItem.Id, targetTreeNavigationItem.GetParentId()));
        }

        public bool IsActive(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            return NavigationType.COMPOSITE_TYPE.Equals(draggedTreeNavigationItem.Type) && NavigationType.FOLDER.Equals(targetTreeNavigationItem.Type);
        }
    }
}
