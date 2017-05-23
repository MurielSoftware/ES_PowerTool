using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.ModelViews
{
    public class ModelViewsUtil
    {
        private static bool IsNotNullAndIsTreeNavigationItem(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            if(!(obj is List<TreeNavigationItem>))
            {
                return false;
            }
            return true;
        }

        public static bool IsBuiltIn(object obj)
        {
            if(!IsNotNullAndIsTreeNavigationItem(obj))
            {
                return false;
            }
            List<TreeNavigationItem> treeNavigationItems = (List<TreeNavigationItem>)obj;
            return true;
        }

        public static bool IsType(object obj, params NavigationType[] types)
        {
            if (!IsNotNullAndIsTreeNavigationItem(obj))
            {
                return false;
            }
            List<TreeNavigationItem> treeNavigationItems = (List<TreeNavigationItem>)obj;
            foreach (TreeNavigationItem treeNavigationItem in treeNavigationItems)
            {
                if(!types.Contains(treeNavigationItem.Type))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
