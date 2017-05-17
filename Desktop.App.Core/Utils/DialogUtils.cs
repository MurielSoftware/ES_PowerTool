using Desktop.App.Core.ModelViews;
using Desktop.App.Core.Ui.Windows;
using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.App.Core.Utils
{
    public class DialogUtils
    {
        public static TreeNavigationItem OpenReferenceWindow(Func<Task<List<TreeNavigationItem>>> actionToGetProposals)
        {
            BaseReferenceWindowModelView referenceWindowModelView = new BaseReferenceWindowModelView();
            referenceWindowModelView.LoadProposals(new List<TreeNavigationItem>() { new TreeNavigationItem(Guid.Empty, "Loading...", NavigationType.FOLDER) });
            ReferenceWindow baseReferenceWindow = new ReferenceWindow(referenceWindowModelView, actionToGetProposals);
            baseReferenceWindow.Owner = Application.Current.MainWindow;
            baseReferenceWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (baseReferenceWindow.ShowDialog() == true)
            {
                return referenceWindowModelView.SelectedObject;
            }
            return null;
        }
    }
}
