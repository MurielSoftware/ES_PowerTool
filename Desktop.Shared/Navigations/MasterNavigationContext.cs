using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Navigations
{
    public class MasterNavigationContext : NavigationContext
    {
        private TreeNavigationItem _masterNavigationItem;

        private MasterNavigationContext(TreeNavigationItem treeNavigationItem)
        {
            _masterNavigationItem = treeNavigationItem;
        }

        public TreeNavigationItem GetMasterNavigationItem()
        {
            return _masterNavigationItem;
        }

        public static MasterNavigationContext CreateMasterNavigationContext(TreeNavigationItem treeNavigationItem)
        {
            return new MasterNavigationContext(treeNavigationItem);
        }
    }
}
