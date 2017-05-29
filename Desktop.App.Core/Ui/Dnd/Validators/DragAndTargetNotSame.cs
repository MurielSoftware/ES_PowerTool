using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Navigations;

namespace Desktop.App.Core.Ui.Dnd.Validators
{
    public class DragAndTargetNotSame : IDragAndDropEqualityValidator
    {
        public bool IsValid(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            return !draggedTreeNavigationItem.Id.Equals(targetTreeNavigationItem.Id);
        }
    }
}
