using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Ui.Dnd.Validators
{
    public class AncestorToDescendantsDropValidator : IDragAndDropEqualityValidator
    {
        public bool IsValid(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            List<Guid> ancestorTreeNavigationItemIds = targetTreeNavigationItem.GetPathIdsToRoot();
            return !ancestorTreeNavigationItemIds.Contains(draggedTreeNavigationItem.Id);
        }
    }
}
