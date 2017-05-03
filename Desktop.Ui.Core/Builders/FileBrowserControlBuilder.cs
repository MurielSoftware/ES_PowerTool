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

namespace Desktop.Ui.Core.Builders
{
    public class FileBrowserControlBuilder : BaseControlBuilder, IControlBuilder
    {
        private Label _referenceLabel;

        public UIElement GenerateUiControl(BaseDto dto, PropertyInfo propertyInfo, Grid grid, int rowIndex)
        {
            //_dto = dto;
            //_propertyInfo = propertyInfo;

            CreateLabel(propertyInfo, grid, rowIndex);

            Grid fileBrowserGrid = new Grid();
            fileBrowserGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            fileBrowserGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            fileBrowserGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            CreateFileLabel(propertyInfo);

            Button referenceButton = CreateButton("Browse", BrowseButtonClick);

            fileBrowserGrid.Children.Add(_referenceLabel);
            fileBrowserGrid.Children.Add(referenceButton);

            Grid.SetColumn(_referenceLabel, 0);
            Grid.SetColumn(referenceButton, 1);

            grid.Children.Add(fileBrowserGrid);
            Grid.SetRow(fileBrowserGrid, rowIndex);
            Grid.SetColumn(fileBrowserGrid, 1);

            return fileBrowserGrid;
        }

        private void CreateFileLabel(PropertyInfo propertyInfo)
        {
            _referenceLabel = new Label();
            Binding binding = new Binding("Dto." + propertyInfo.Name);
            _referenceLabel.SetBinding(Label.ContentProperty, binding);
        }

        private Button CreateButton(string label, RoutedEventHandler click)
        {
            Button button = new Button();
            button.Content = label;
            button.Margin = new Thickness(2, 5, 2, 5);
            button.Padding = new Thickness(4, 0, 4, 0);
            button.Height = 22;
            button.Click += click;
            return button;
        }

        private void BrowseButtonClick(object sender, RoutedEventArgs e)
        {
            //_propertyInfo.SetValue(_dto, null);
            //_referenceLabel.Content = string.Empty;
        }
    }
}
