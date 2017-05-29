using Desktop.App.Core.Ui.Dnd.DropHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Navigations;
using ES_PowerTool.Shared.Services.OOE.Elements;
using Desktop.Shared.Core;
using ES_PowerTool.Shared.Dtos.OOE.Elements;
using Desktop.App.Core.Events.Publishing;
using Desktop.Shared.Core.DataTypes;

namespace ES_PowerTool.Ui.Dnd.DropHandlers
{
    public class TypeToCompositeTypeDropHandler : IDropHandler
    {
        public void Drop(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            string name = Guid.NewGuid().ToString().Substring(0, 7);
            ICompositeTypeElementCRUDService compositeTypeElementCRUDService = ServiceActivator.Get<ICompositeTypeElementCRUDService>();
            CompositeTypeElementDto compositeTypeElementDto = new CompositeTypeElementDto();
            compositeTypeElementDto.Description = name;
            compositeTypeElementDto.UniqueName = name;
            compositeTypeElementDto.RuntimeId = Guid.NewGuid();
            compositeTypeElementDto.ProjectId = targetTreeNavigationItem.ProjectId;
            compositeTypeElementDto.OwningTypeId = targetTreeNavigationItem.Id;
            compositeTypeElementDto.State = State.NEW;
            compositeTypeElementDto.ElementTypeReference = new ReferenceString(draggedTreeNavigationItem.Id, draggedTreeNavigationItem.Name);
            compositeTypeElementDto = compositeTypeElementCRUDService.Persist(compositeTypeElementDto);
            Publisher.GetInstance().Publish(PublishEvent.CreateCreationEvent(compositeTypeElementDto.Id, compositeTypeElementDto.OwningTypeId));
        }

        public bool IsActive(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            return (NavigationType.COMPOSITE_TYPE.Equals(draggedTreeNavigationItem.Type) || NavigationType.PRIMITIVE_TYPE.Equals(draggedTreeNavigationItem.Type)) 
                && NavigationType.COMPOSITE_TYPE.Equals(targetTreeNavigationItem.Type);
        }
    }
}
