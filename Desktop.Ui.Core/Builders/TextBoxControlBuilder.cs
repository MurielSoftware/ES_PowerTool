using Desktop.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Desktop.Ui.Core.Builders
{
    public class TextBoxControlBuilder : BaseControlBuilder, IControlBuilder
    {
        public UIElement GenerateUiControl(BaseDto dto, PropertyInfo propertyInfo, Grid grid, int rowIndex)
        {
            CreateLabel(propertyInfo, grid, rowIndex);

            TextBox textBox = new TextBox();
            Binding binding = new Binding("Dto." + propertyInfo.Name);
            if (propertyInfo.IsDefined(typeof(ReadOnlyAttribute)))
            {
                binding.Mode = BindingMode.OneWay;
                textBox.IsReadOnly = true;
            }
            else
            {
                binding.Mode = BindingMode.TwoWay;
            }
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding.ValidatesOnDataErrors = true;
            binding.NotifyOnValidationError = true;
            textBox.SetBinding(TextBox.TextProperty, binding);
            textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            textBox.VerticalContentAlignment = VerticalAlignment.Center;

            grid.Children.Add(textBox);
            Grid.SetRow(textBox, rowIndex);
            Grid.SetColumn(textBox, 1);

            return textBox;
        }
    }
}
