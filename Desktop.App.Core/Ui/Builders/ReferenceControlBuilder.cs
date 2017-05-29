using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.DataTypes;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Desktop.Shared.Core.Navigations;
using Desktop.App.Core.Editors;
using Desktop.Shared.Core.Attributes;
using Desktop.App.Core.Utils;
using System.Windows.Media.Imaging;

namespace Desktop.App.Core.Ui.Builders
{
    public class ReferenceControlBuilder : BaseControlBuilder, IControlBuilder
    {
        private static Uri RemoveReferenceImage = new Uri("pack://application:,,,/Images/remove.png");

        public UIElement GenerateUiControl(BaseDto dto, PropertyInfo propertyInfo, Grid grid, int rowIndex)
        {
            CreateLabel(propertyInfo, grid, rowIndex);

            Grid referenceGrid = new Grid();
            referenceGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            referenceGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            referenceGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            Label referenceLabel = CreateReferenceLabel(dto, propertyInfo);

            Button referenceButton = CreateButton("...", delegate 
            {
                IReferenceEditor referenceEditor = CreateReferenceEditor(dto, propertyInfo); //(BaseReferenceEditor)Activator.CreateInstance(Type.GetType(editorAttribute.CompleteAssembly));
                TreeNavigationItem selectedTreeNavigationItem = DialogUtils.OpenReferenceWindow(referenceEditor.GetProposals);
                if (selectedTreeNavigationItem != null)
                {
                    ReferenceString referenceString = new ReferenceString(selectedTreeNavigationItem.Id, selectedTreeNavigationItem.Name);
                    propertyInfo.SetValue(dto, referenceString);
                    referenceLabel.Content = referenceString.GetValue();
                }
            });

            Image image = new Image();
            image.Source = new BitmapImage(RemoveReferenceImage);
            Button removeReferenceButton = CreateButton(image, delegate 
            {
                propertyInfo.SetValue(dto, null);
                referenceLabel.Content = string.Empty;
            });

            referenceGrid.Children.Add(referenceLabel);
            referenceGrid.Children.Add(referenceButton);
            referenceGrid.Children.Add(removeReferenceButton);

            Grid.SetColumn(referenceLabel, 0);
            Grid.SetColumn(referenceButton, 1);
            Grid.SetColumn(removeReferenceButton, 2);

            grid.Children.Add(referenceGrid);
            Grid.SetRow(referenceGrid, rowIndex);
            Grid.SetColumn(referenceGrid, 1);

            return referenceGrid;
        }

        private Label CreateReferenceLabel(BaseDto dto, PropertyInfo propertyInfo)
        {
            Label label = new Label();
            label.Margin = new Thickness(0, 2, 0, 2);
            ReferenceString referenceString = (ReferenceString)propertyInfo.GetValue(dto);
            if(referenceString != null)
            {
                label.Content = referenceString.GetValue();
            }
            return label;
        }

        private Button CreateButton(object label, RoutedEventHandler click)
        {
            Button button = new Button();
            button.Content = label;
            button.Margin = new Thickness(2, 5, 2, 5);
            button.Padding = new Thickness(4, 0, 4, 0);
            button.Width = 22;
            button.Height = 22;
            button.Click += click;
            return button;
        }

        private IReferenceEditor CreateReferenceEditor(BaseDto dto, PropertyInfo propertyInfo)
        {
            ReferenceEdirorAttribute editorAttribute = (ReferenceEdirorAttribute)propertyInfo.GetCustomAttribute(typeof(ReferenceEdirorAttribute));
            Type baseReferenceType = typeof(BaseReferenceEditor<>).MakeGenericType(dto.GetType());
            return (IReferenceEditor)Activator.CreateInstance(Type.GetType(editorAttribute.CompleteAssembly), new object[] { dto });
        }
    }
}
