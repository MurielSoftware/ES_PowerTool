using Desktop.Shared.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Services
{
    public interface INavigationService
    {
        List<TreeNavigationItem> GetRoots(NavigationContext navigationContext);
        List<TreeNavigationItem> GetChildren(NavigationContext navigationContext, TreeNavigationItem parentTreeNavigationItem);
        TreeNavigationItem Reload(TreeNavigationItem treeNavigationItem);
    }
}
