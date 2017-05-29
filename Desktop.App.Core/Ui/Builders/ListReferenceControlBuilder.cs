using Desktop.App.Core.Editors;
using Desktop.App.Core.Ui.Converters;
using Desktop.App.Core.Utils;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Navigations;
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
    public class ListReferenceControlBuilder : BaseControlBuilder, IControlBuilder
    {
        public UIElement GenerateUiControl(BaseDto dto, PropertyInfo propertyInfo, Grid grid, int rowIndex)
        {
            CreateLabel(propertyInfo, grid, rowIndex);

            Grid referenceGrid = new Grid();
            referenceGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            referenceGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            referenceGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            ListBox referenceList = CreateReferenceList(dto, propertyInfo);

            StackPanel stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10, 0, 5, 0);
            stackPanel.Orientation = Orientation.Vertical;

            Button referenceButton = CreateButton("Add", delegate
            {
      //          ReferenceEdirorAttribute editorAttribute = (ReferenceEdirorAttribute)propertyInfo.GetCustomAttribute(typeof(ReferenceEdirorAttribute));
                IReferenceEditor baseReferenceEditor = CreateReferenceEditor(dto, propertyInfo); //(BaseReferenceEditor)Activator.CreateInstance(Type.GetType(editorAttribute.CompleteAssembly));
                TreeNavigationItem selectedTreeNavigationItem = DialogUtils.OpenReferenceWindow(baseReferenceEditor.GetProposals);
                if (selectedTreeNavigationItem != null)
                {
                    ReferenceString referenceString = (ReferenceString)propertyInfo.GetValue(dto);
                    if (referenceString == null)
                    {
                        referenceString = new ReferenceString();
                    }
                    referenceString.Append(selectedTreeNavigationItem.Id, selectedTreeNavigationItem.Name);
                    propertyInfo.SetValue(dto, referenceString);                    
                    referenceList.Items.Add(selectedTreeNavigationItem);
                }
            });

            Button removeReferenceButton = CreateButton("Remove", delegate
            {
                TreeNavigationItem selectedTreeNavigationItem = referenceList.SelectedItem as TreeNavigationItem;
                ReferenceString referenceString = (ReferenceString)propertyInfo.GetValue(dto);
                referenceString.Remove(selectedTreeNavigationItem.Id);
                referenceList.Items.Remove(selectedTreeNavigationItem);
            });

            stackPanel.Children.Add(referenceButton);
            stackPanel.Children.Add(removeReferenceButton);

            referenceGrid.Children.Add(referenceList);
            referenceGrid.Children.Add(stackPanel);

            Grid.SetColumn(referenceList, 0);
            Grid.SetColumn(stackPanel, 1);

            grid.Children.Add(referenceGrid);
            Grid.SetRow(referenceGrid, rowIndex);
            Grid.SetColumn(referenceGrid, 1);

            return referenceGrid;
        }

        private ListBox CreateReferenceList(BaseDto baseDto, PropertyInfo propertyInfo)
        {
            ListBox listBox = new ListBox();
            listBox.Margin = new Thickness(2, 5, 2, 5);
            ReferenceString referenceString = (ReferenceString)propertyInfo.GetValue(baseDto);
            if(referenceString != null)
            {
                Dictionary<Guid, string> dictionary = ReferenceString.Parse(referenceString.ToString());
                foreach (KeyValuePair<Guid, string> value in dictionary)
                {
                    listBox.Items.Add(new TreeNavigationItem(value.Key, value.Value, NavigationType.FOLDER));
                }
            }
            listBox.Height = 120;
            return listBox;
        }

        private Button CreateButton(string label, RoutedEventHandler click)
        {
            Button button = new Button();
            button.Content = label;
            button.Margin = new Thickness(2, 5, 2, 5);
            button.Padding = new Thickness(4, 0, 4, 0);
            button.Height = 24;
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
