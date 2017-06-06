using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core;

namespace Desktop.App.Core.Ui.Dnd.Validators
{
    public class BuiltInDragAndDropValidator : IDragAndDropEqualityValidator
    {
        public bool IsValid(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            return !State.BUILTIN.Equals(targetTreeNavigationItem.State);
        }
    }
}
