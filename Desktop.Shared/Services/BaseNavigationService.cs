using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Navigations;

namespace Desktop.Shared.Core.Services
{
    public class BaseNavigationService : BaseService
    {
        public BaseNavigationService(Connection connection) 
            : base(connection)
        {
        }

        protected virtual void ExtendTreeNavigationItems(List<TreeNavigationItem> treeNavigationItems, TreeNavigationItem parentTreeNavigationItem)
        {
            TreeNavigationItem loadingTreeNavigationItem = new TreeNavigationItem(Guid.Empty, "Loading...", NavigationType.FOLDER);

            foreach (TreeNavigationItem treeNavigationItem in treeNavigationItems)
            {
                if (treeNavigationItem.HasRemoteChildren)
                {
                    treeNavigationItem.Children.Add(loadingTreeNavigationItem);
                }
                if (parentTreeNavigationItem != null)
                {
                    treeNavigationItem.Parent = parentTreeNavigationItem;
                }
            }
        }
    }
}
