using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.App.Core.Ui.Converters
{
    public class StringToBooleanConverter : IValueConverter
    {
        private static string TRUE = "true";
        private static string FALSE = "false";
        //private static List<string> listOfFalseValues = CreateListOfFalseValues();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null || !(value is string))
            {
                return false;
            }
            if(TRUE.Equals(value))
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is bool))
            {
                return false;
            }
            bool booleanValue = (bool)value;
            if(booleanValue)
            {
                return TRUE;
            }
            return FALSE;
        }
    }
}
