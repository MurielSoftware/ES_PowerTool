using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.References;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Desktop.Ui.Core.Builders
{
    public class ReferenceControlBuilder : BaseControlBuilder, IControlBuilder
    {
        private PropertyInfo _propertyInfo;
        private Label _referenceLabel;
        private BaseDto _dto;

        public UIElement GenerateUiControl(BaseDto dto, PropertyInfo propertyInfo, Grid grid, int rowIndex)
        {
            _dto = dto;
            _propertyInfo = propertyInfo;

            CreateLabel(propertyInfo, grid, rowIndex);

            Grid referenceGrid = new Grid();
            referenceGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            referenceGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            referenceGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            CreateReferenceLabel(propertyInfo);

            Button referenceButton = CreateButton("...", ReferenceButtonClick);
            Button removeReferenceButton = CreateButton("x", RemoveReferenceButtonClick);

            referenceGrid.Children.Add(_referenceLabel);
            referenceGrid.Children.Add(referenceButton);
            referenceGrid.Children.Add(removeReferenceButton);

            Grid.SetColumn(_referenceLabel, 0);
            Grid.SetColumn(referenceButton, 1);
            Grid.SetColumn(removeReferenceButton, 2);

            grid.Children.Add(referenceGrid);
            Grid.SetRow(referenceGrid, rowIndex);
            Grid.SetColumn(referenceGrid, 1);

            return referenceGrid;
        }

        private void CreateReferenceLabel(PropertyInfo propertyInfo)
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
            button.Width = 22;
            button.Height = 22;
            button.Click += click;
            return button;
        }

        private void ReferenceButtonClick(object sender, RoutedEventArgs e)
        {
            //EditorAttribute editorAttribute = (EditorAttribute)_propertyInfo.GetCustomAttribute(typeof(EditorAttribute));
            //ReferenceEditor ReferenceEditor = (ReferenceEditor)Activator.CreateInstance(Type.GetType(editorAttribute.EditorTypeName));
            //TreeNavigationItem selectedTreeNavigationItem = DialogUtils.OpenReferenceWindow(baseReferenceEditor.GetProposals);
            //if (selectedTreeNavigationItem != null)
            //{
            //    ReferenceString referenceString = new ReferenceString(selectedTreeNavigationItem.Id, selectedTreeNavigationItem.Name);
            //    _propertyInfo.SetValue(_dto, referenceString);
            //    _referenceLabel.Content = referenceString.GetValue();
            //}
        }

        private void RemoveReferenceButtonClick(object sender, RoutedEventArgs e)
        {
            _propertyInfo.SetValue(_dto, null);
            _referenceLabel.Content = string.Empty;
        }
    }
}
