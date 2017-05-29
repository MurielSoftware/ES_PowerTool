using Desktop.Shared.Core.Navigations;
using System.Collections.Generic;

namespace Desktop.App.Core.Ui.Dnd.Validators
{
    public class NavigationItemTypeCompatibleDragAndDropValidator : IDragAndDropEqualityValidator
    {
        private ICollection<NavigationType> _draggedNavigationItemTypes;
        private ICollection<NavigationType> _targetNavigationItemTypes;

        public NavigationItemTypeCompatibleDragAndDropValidator(ICollection<NavigationType> draggedNavigationItemTypes, ICollection<NavigationType> targetNavigationItemTypes)
        {
            _draggedNavigationItemTypes = draggedNavigationItemTypes;
            _targetNavigationItemTypes = targetNavigationItemTypes;
        }

        public NavigationItemTypeCompatibleDragAndDropValidator(NavigationType draggedNavigationItemType, NavigationType targetNavigationItemType)
            : this(new List<NavigationType>() { draggedNavigationItemType }, new List<NavigationType>() { targetNavigationItemType })
        {
        }

        public bool IsValid(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            return _draggedNavigationItemTypes.Contains(draggedTreeNavigationItem.Type)
                && _targetNavigationItemTypes.Contains(targetTreeNavigationItem.Type);
        }
    }
}
