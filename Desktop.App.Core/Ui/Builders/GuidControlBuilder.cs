using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Desktop.App.Core.Ui.Builders
{
    public class GuidControlBuilder : BaseControlBuilder, IControlBuilder
    {
        public UIElement GenerateUiControl(BaseDto dto, PropertyInfo propertyInfo, Grid grid, int rowIndex)
        {
            CreateLabel(propertyInfo, grid, rowIndex);

            Grid fileBrowserGrid = new Grid();
            fileBrowserGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            fileBrowserGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            TextBox guidTextBox = CreateFileTextBox(propertyInfo);

            Button generateButton = CreateButton("Generate");
            generateButton.Click += delegate
            {
                Guid guid = Guid.NewGuid(); 
                propertyInfo.SetValue(dto, guid);
                guidTextBox.Text = guid.ToString();                
            };

            fileBrowserGrid.Children.Add(guidTextBox);
            fileBrowserGrid.Children.Add(generateButton);

            Grid.SetColumn(guidTextBox, 0);
            Grid.SetColumn(generateButton, 1);

            grid.Children.Add(fileBrowserGrid);
            Grid.SetRow(fileBrowserGrid, rowIndex);
            Grid.SetColumn(fileBrowserGrid, 1);

            return fileBrowserGrid;
        }

        private TextBox CreateFileTextBox(PropertyInfo propertyInfo)
        {
            TextBox textBox = new TextBox();
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.Margin = new Thickness(0, 4, 0, 4);
            Binding binding = new Binding("Dto." + propertyInfo.Name);
            textBox.SetBinding(TextBox.TextProperty, binding);
            return textBox;
        }

        private Button CreateButton(string label)
        {
            Button button = new Button();
            button.Content = label;
            button.Margin = new Thickness(2, 5, 2, 5);
            button.Padding = new Thickness(4, 0, 4, 0);
            button.Height = 22;
            return button;
        }
    }
}
