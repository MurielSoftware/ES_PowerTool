using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Desktop.Shared.Core.Dtos;
using System.Windows.Data;
using Desktop.App.Core.Utils;
using Desktop.Shared.DataTypes;

namespace Desktop.App.Core.Ui.Builders
{
    public class FileBrowserControlBuilder : BaseControlBuilder, IControlBuilder
    {
        public UIElement GenerateUiControl(BaseDto dto, PropertyInfo propertyInfo, Grid grid, int rowIndex)
        {
            CreateLabel(propertyInfo, grid, rowIndex);

            Grid fileBrowserGrid = new Grid();
            fileBrowserGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            fileBrowserGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            TextBox fileTextBox = CreateFileTextBox(propertyInfo);

            Button referenceButton = CreateButton("Browse");
            referenceButton.Click += delegate
            {
                string fileName = SystemDialogUtils.ShowOpenFileDialog("*.csv|*.csv");
                if (fileName != null)
                {
                    FilePath filePath = new FilePath(fileName);
                    propertyInfo.SetValue(dto, filePath);
                    fileTextBox.Text = filePath.ToString();
                }
            };

            fileBrowserGrid.Children.Add(fileTextBox);
            fileBrowserGrid.Children.Add(referenceButton);

            Grid.SetColumn(fileTextBox, 0);
            Grid.SetColumn(referenceButton, 1);

            grid.Children.Add(fileBrowserGrid);
            Grid.SetRow(fileBrowserGrid, rowIndex);
            Grid.SetColumn(fileBrowserGrid, 1);

            return fileBrowserGrid;
        }

        private TextBox CreateFileTextBox(PropertyInfo propertyInfo)
        {
            TextBox textBox = new TextBox();
            textBox.IsReadOnly = true;
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
