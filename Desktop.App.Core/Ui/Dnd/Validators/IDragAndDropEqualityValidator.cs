using Desktop.Shared.Core.Navigations;

namespace Desktop.App.Core.Ui.Dnd.Validators
{
    public interface IDragAndDropEqualityValidator
    {
        bool IsValid(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem);
    }
}
