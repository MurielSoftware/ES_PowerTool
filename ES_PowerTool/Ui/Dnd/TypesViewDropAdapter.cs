using Desktop.App.Core.Ui.Dnd;
using Desktop.App.Core.Ui.Dnd.Validators;
using Desktop.Shared.Core.Navigations;
using ES_PowerTool.Ui.Dnd.DropHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Ui.Dnd
{
    public class TypesViewDropAdapter : BaseDropAdapter
    {
        protected override void AddDragAndDropEqualityValidators()
        {
            base.AddDragAndDropEqualityValidators();
            _dragAndDropEqualityValidators.Add(new NavigationItemTypeCompatibleDragAndDropValidator(new List<NavigationType>() { NavigationType.PRIMITIVE_TYPE, NavigationType.COMPOSITE_TYPE, NavigationType.FOLDER }, new List<NavigationType>() { NavigationType.COMPOSITE_TYPE, NavigationType.FOLDER }));
        }

        protected override void AddDropHandlers()
        {
            _dropHandlers.Add(new TypeToCompositeTypeDropHandler());
            _dropHandlers.Add(new FolderToFolderDropHandler());
            _dropHandlers.Add(new TypeToFolderDropHandler());
        }
    }
}
