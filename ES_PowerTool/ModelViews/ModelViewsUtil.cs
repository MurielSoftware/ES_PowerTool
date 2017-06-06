using Desktop.Shared.Core;
using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Utils;
using ES_PowerTool.Settings;
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
            if (treeNavigationItems.Count == 0)
            {
                return false;
            }
            bool allowEditImportedElements = Converter.ConvertValue<bool>(SettingsProvider.GetInstance().GetSettings().AllowEditImportedElements.Value);
            if(allowEditImportedElements)
            {
                return false;
            }
            foreach (TreeNavigationItem treeNavigationItem in treeNavigationItems)
            {
                if (!State.BUILTIN.Equals(treeNavigationItem.State))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsType(object obj, params NavigationType[] types)
        {
            if (!IsNotNullAndIsTreeNavigationItem(obj))
            {
                return false;
            }
            List<TreeNavigationItem> treeNavigationItems = (List<TreeNavigationItem>)obj;
            if(treeNavigationItems.Count == 0)
            {
                return false;
            }
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
