using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.App.Core.Ui.Converters
{
    public class ListBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<TreeNavigationItem> treeNavigationItems = new ObservableCollection<TreeNavigationItem>();
            if(value == null)
            {
                return null;
            }
            Dictionary<Guid, string> dictionary = ReferenceString.Parse(value.ToString());
            foreach(KeyValuePair<Guid, string> referenceString in dictionary)
            {
                treeNavigationItems.Add(new TreeNavigationItem(referenceString.Key, referenceString.Value, NavigationType.FOLDER));
            }
            return treeNavigationItems;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
