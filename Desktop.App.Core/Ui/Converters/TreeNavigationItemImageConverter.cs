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
    public class TreeNavigationItemImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (!(value is TreeNavigationItem))
            {
                return null;
            }
            TreeNavigationItem treeNavigationItem = value as TreeNavigationItem;
            if (NavigationType.PRESET.Equals(treeNavigationItem.Type))
            {
                PresetTreeNavigationItem presetTreeNavigationItem = (PresetTreeNavigationItem)treeNavigationItem;
                if (presetTreeNavigationItem.IsDefault)
                {
                    return new Uri("pack://application:,,,/Images/preset_default.png");
                }
            }
            return NavigationTypeToImage.NAVIGATION_TYPE_TO_IMAGE[treeNavigationItem.Type];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
