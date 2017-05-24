using Desktop.Shared.Core.Validations;
using Desktop.Ui.I18n;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.App.Core.Ui.Converters
{
    public class ResourceKeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (value is ValidationMessage)
            {
                ValidationMessage validationMessage = (ValidationMessage)value;
                return ResourceUtils.GetMessage(validationMessage.ResourceKey, validationMessage.Parameters);
            }
            return ResourceUtils.GetMessage(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
