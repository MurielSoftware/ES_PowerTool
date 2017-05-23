using Desktop.Shared.Core.Validations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Desktop.App.Core.Ui.Converters
{
    public class MessageWindowIconConverter : IValueConverter
    {
        private static Dictionary<ValidationType, Uri> IMAGE_TO_VALIDATION_TYPE = CreateImageToValidationType();

        private static Dictionary<ValidationType, Uri> CreateImageToValidationType()
        {
            Dictionary<ValidationType, Uri> map = new Dictionary<ValidationType, Uri>();
            map.Add(ValidationType.ERROR, new Uri("pack://application:,,,/Images/error.png"));
            map.Add(ValidationType.INFO, new Uri("pack://application:,,,/Images/info.png"));
            map.Add(ValidationType.WARNING, new Uri("pack://application:,,,/Images/warning.png"));
            return map;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (!(value is ValidationType))
            {
                return null;
            }
            ValidationType validationType = (ValidationType)value;
            return IMAGE_TO_VALIDATION_TYPE[validationType];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
