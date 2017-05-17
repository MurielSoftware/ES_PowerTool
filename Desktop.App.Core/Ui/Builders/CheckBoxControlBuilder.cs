using Desktop.Shared.Core.Dtos;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Desktop.App.Core.Ui.Builders
{
    public class CheckBoxControlBuilder : BaseControlBuilder, IControlBuilder
    {
        public UIElement GenerateUiControl(BaseDto dto, PropertyInfo propertyInfo, Grid grid, int rowIndex)
        {
            CheckBox checkBox = new CheckBox();
            Binding binding = new Binding("Dto." + propertyInfo.Name);
            checkBox.SetBinding(CheckBox.IsCheckedProperty, binding);
            checkBox.Content = GetDisplayName(propertyInfo);
            checkBox.VerticalContentAlignment = VerticalAlignment.Center;
            checkBox.Margin = new Thickness(0, 2, 0, 2);
            grid.Children.Add(checkBox);
            Grid.SetRow(checkBox, rowIndex);
            Grid.SetColumn(checkBox, 1);
            return checkBox;
        }
    }
}
