using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Desktop.Ui.Core.Builders
{
    public class EnumControlBuilder : BaseControlBuilder, IControlBuilder
    {
        public UIElement GenerateUiControl(BaseDto dto, PropertyInfo propertyInfo, Grid grid, int rowIndex)
        {
            CreateLabel(propertyInfo, grid, rowIndex);

            ComboBox comboBox = new ComboBox();
            Binding binding = new Binding("Dto." + propertyInfo.Name);
            binding.Mode = BindingMode.TwoWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding.ValidatesOnDataErrors = true;
            binding.NotifyOnValidationError = true;
            comboBox.SetBinding(ComboBox.SelectedValueProperty, binding);
            comboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            comboBox.SelectedValuePath = "Key";
            comboBox.DisplayMemberPath = "Value";
            comboBox.VerticalContentAlignment = VerticalAlignment.Center;
            FillComboBoxByItems(comboBox, propertyInfo);
            grid.Children.Add(comboBox);
            Grid.SetRow(comboBox, rowIndex);
            Grid.SetColumn(comboBox, 1);

            return comboBox;
        }

        private void FillComboBoxByItems(ComboBox comboBox, PropertyInfo propertyInfo)
        {
            Dictionary<object, string> items = new Dictionary<object, string>();
            Array enumValues = Enum.GetValues(propertyInfo.PropertyType);
            foreach (object enumValue in enumValues)
            {
                items.Add(enumValue, Enum.GetName(propertyInfo.PropertyType, enumValue));
            }
            comboBox.ItemsSource = items;
        }
    }
}
