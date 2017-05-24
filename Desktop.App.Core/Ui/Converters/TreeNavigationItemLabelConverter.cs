using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.App.Core.Ui.Converters
{
    public class TreeNavigationItemLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return null;
            }
            if(!(value is TreeNavigationItem))
            {
                return null;
            }
            TreeNavigationItem treeNavigationItem = value as TreeNavigationItem;
            if(NavigationType.TYPE_ELEMENT.Equals(treeNavigationItem.Type))
            {
                CompositeTypeElementTreeNavigationItem compositeTypeElementTreeNavigationItem = (CompositeTypeElementTreeNavigationItem)treeNavigationItem;
                return compositeTypeElementTreeNavigationItem.Name + " : " + compositeTypeElementTreeNavigationItem.ElementTypeName;
            }
            else if(NavigationType.COMPOSITE_PRESET_ELEMENT.Equals(treeNavigationItem.Type))
            {
                CompositePresetElementTreeNavigationItem compositePresetElementTreeNavigationItem = (CompositePresetElementTreeNavigationItem)treeNavigationItem;
                return compositePresetElementTreeNavigationItem.CompositeTypeElementName + " : " + compositePresetElementTreeNavigationItem.CompositeTypeElementElementTypeName + " <- " + compositePresetElementTreeNavigationItem.AssociatedPresetName;
            }
            return treeNavigationItem.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
