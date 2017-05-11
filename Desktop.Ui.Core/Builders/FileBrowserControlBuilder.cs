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
using Desktop.Ui.Core.Utils;
using Desktop.Shared.DataTypes;

namespace Desktop.Ui.Core.Builders
{
    public class FileBrowserControlBuilder : BaseControlBuilder, IControlBuilder
    {
        //private Label _referenceLabel;
        //private BaseDto _dto;
        //private PropertyInfo _propertyInfo;

        public UIElement GenerateUiControl(BaseDto dto, PropertyInfo propertyInfo, Grid grid, int rowIndex)
        {
            //_dto = dto;
            //_propertyInfo = propertyInfo;

            CreateLabel(propertyInfo, grid, rowIndex);

            Grid fileBrowserGrid = new Grid();
            fileBrowserGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            fileBrowserGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            fileBrowserGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            Label fileLabel = CreateFileLabel(propertyInfo);

            Button referenceButton = CreateButton("Browse", null);
            referenceButton.Click += delegate
            {
                string fileName = SystemDialogUtils.ShowOpenFileDialog("*.csv|*.csv");
                if (fileName != null)
                {
                    FilePath filePath = new FilePath(fileName);
                    propertyInfo.SetValue(dto, filePath);
                    fileLabel.Content = filePath.ToString();
                }
            };

            fileBrowserGrid.Children.Add(fileLabel);
            fileBrowserGrid.Children.Add(referenceButton);

            Grid.SetColumn(fileLabel, 0);
            Grid.SetColumn(referenceButton, 1);

            grid.Children.Add(fileBrowserGrid);
            Grid.SetRow(fileBrowserGrid, rowIndex);
            Grid.SetColumn(fileBrowserGrid, 1);

            return fileBrowserGrid;
        }

        private Label CreateFileLabel(PropertyInfo propertyInfo)
        {
            Label referenceLabel = new Label();
            Binding binding = new Binding("Dto." + propertyInfo.Name);
            referenceLabel.SetBinding(Label.ContentProperty, binding);
            return referenceLabel;
        }

        private Button CreateButton(string label, RoutedEventHandler click)
        {
            Button button = new Button();
            button.Content = label;
            button.Margin = new Thickness(2, 5, 2, 5);
            button.Padding = new Thickness(4, 0, 4, 0);
            button.Height = 22;
            //button.Click += click;
            return button;
        }

        private void BrowseButtonClick(BaseDto dto, PropertyInfo propertyInfo, Label fileLabel)
        {
            string fileName = SystemDialogUtils.ShowOpenFileDialog("*.csv|*.csv");
            if (fileName != null)
            {
                FilePath filePath = new FilePath(fileName);
                propertyInfo.SetValue(dto, filePath);
                fileLabel.Content = filePath.ToString();
            }
        }
    }
}
