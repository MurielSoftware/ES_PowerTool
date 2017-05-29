using Desktop.Shared.Core.Navigations;

namespace Desktop.App.Core.Ui.Dnd.Validators
{
    public interface IDragAndDropValidator
    {
        bool IsValid(TreeNavigationItem treeNavigationItem);
    }
}
