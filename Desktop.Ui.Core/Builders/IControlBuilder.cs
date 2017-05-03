using Desktop.Shared.Core.Dtos;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Ui.Core.Builders
{
    public interface IControlBuilder
    {
        UIElement GenerateUiControl(BaseDto dto, PropertyInfo propertyInfo, Grid grid, int rowIndex);
    }
}
