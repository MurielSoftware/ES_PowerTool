using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Ui.Dnd.DropHandlers
{
    public interface IDropHandler
    {
        bool IsActive(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem);
        void Drop(TreeNavigationItem draggedTreeNavigationItem, TreeNavigationItem targetTreeNavigationItem);
    }
}
