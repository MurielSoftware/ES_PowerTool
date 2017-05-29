using Desktop.Shared.Core.Navigations;

namespace Desktop.App.Core.Ui.Dnd.Validators
{
    public class NavigationItemIsNotEmptyValidator : IDragAndDropEqualityValidator
    {
        public bool IsValid(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem)
        {
            return draggedTreeNavigationItem != null && targetTreeNavigationItem != null;
        }
    }
}
