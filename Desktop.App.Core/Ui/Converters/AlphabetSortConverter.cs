using Desktop.Shared.Core.Navigations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.App.Core.Ui.Converters
{
    public class AlphabetSortConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IList))
            {
                return null;
            }

            IList list = value as IList;
            ListCollectionView listCollectionView = new ListCollectionView(list);

            //   SortDescription nameSortDescription = new SortDescription(parameter.ToString(), ListSortDirection.Ascending);
            //  listCollectionView.SortDescriptions.Add(nameSortDescription);
            listCollectionView.CustomSort = new CustomSorting();
            return listCollectionView;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        internal class CustomSorting : IComparer
        {
            public int Compare(object x, object y)
            {
                TreeNavigationItem firstTreeNavigationItem = x as TreeNavigationItem;
                TreeNavigationItem secondTreeNavigationItem = y as TreeNavigationItem;

                int compare = firstTreeNavigationItem.Type.CompareTo(secondTreeNavigationItem.Type);
                if (compare == 0)
                {
                    if (firstTreeNavigationItem.Name != null)
                    {
                        return firstTreeNavigationItem.Name.CompareTo(secondTreeNavigationItem.Name);
                    }
                    return firstTreeNavigationItem.ToString().CompareTo(secondTreeNavigationItem.ToString());
                }
                return compare;
            }
        }
    }
}
