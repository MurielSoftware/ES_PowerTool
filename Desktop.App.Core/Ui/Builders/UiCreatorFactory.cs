using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.DataTypes;
using Desktop.Ui.I18n;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Desktop.Shared.DataTypes;

namespace Desktop.App.Core.Ui.Builders
{
    public class UiCreatorFactory
    {
        private static Dictionary<Type, IControlBuilder> CONTROL_BUILDERS = CreateControlBuilders();

        public void Generate(Grid grid, BaseDto dto)
        {
            Dictionary<string, List<PropertyInfo>> groups = GetGroups(dto.GetType());
            CreateRowGridDefinitions(grid, groups.Count);
            CreateGroups(dto, grid, groups);
        }

        private Dictionary<string, List<PropertyInfo>> GetGroups(Type dtoType)
        {
            List<PropertyInfo> properties = dtoType.GetProperties().Where(x => !Attribute.IsDefined(x, typeof(BrowsableAttribute))).ToList();
            return properties
                .OrderBy(x => ((LocalizedCategoryAttribute)x.GetCustomAttribute(typeof(LocalizedCategoryAttribute))).SortValue)
                .GroupBy(x => ((LocalizedCategoryAttribute)x.GetCustomAttribute(typeof(LocalizedCategoryAttribute))).Category)
                .ToDictionary(y => y.Key, y => y.ToList());
        }

        private void CreateRowGridDefinitions(Grid grid, int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowDefinition);
            }
        }

        private List<GroupBox> CreateGroups(BaseDto dto, Grid grid, Dictionary<string, List<PropertyInfo>> groups)
        {
            List<GroupBox> groupBoxes = new List<GroupBox>();
            int i = 0;
            foreach (KeyValuePair<string, List<PropertyInfo>> group in groups)
            {
                GroupBox groupBox = new GroupBox();
                groupBox.Padding = new Thickness(5);
                groupBox.Header = ResourceUtils.GetMessage(group.Key);
                CreateGroupsControls(groupBox, dto, group.Value);
                grid.Children.Add(groupBox);
                Grid.SetRow(groupBox, i++);
            }
            return groupBoxes;
        }

        private void CreateGroupsControls(GroupBox groupBox, BaseDto dto, List<PropertyInfo> propertyInfos)
        {
            int i = 0;
            Grid grid = new Grid();
            CreateRowGridDefinitions(grid, propertyInfos.Count);
            ColumnDefinition columnDefinitionLabel = new ColumnDefinition();
            columnDefinitionLabel.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(columnDefinitionLabel);
            ColumnDefinition columnDefinitionInput = new ColumnDefinition();
            columnDefinitionInput.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(columnDefinitionInput);

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                IControlBuilder controlGenerator = null;
                if (propertyInfo.PropertyType.IsEnum)
                {
                    controlGenerator = CONTROL_BUILDERS[typeof(Enum)];
                }
                else if (propertyInfo.PropertyType.Equals(typeof(ReferenceString)))
                {
                    if(propertyInfo.GetCustomAttribute<ListReferenceAttribute>() != null)
                    {
                        controlGenerator = new ListReferenceControlBuilder();
                    }
                    else
                    {
                        controlGenerator = new ReferenceControlBuilder();
                    }
                }
                else
                {
                    controlGenerator = CONTROL_BUILDERS[propertyInfo.PropertyType];
                }
                controlGenerator.GenerateUiControl(dto, propertyInfo, grid, i);
                i++;
            }
            groupBox.Content = grid;
        }

        private static bool IsEnum(Type type)
        {
            return typeof(Enum).IsAssignableFrom(type);
        }

        private static Dictionary<Type, IControlBuilder> CreateControlBuilders()
        {
            Dictionary<Type, IControlBuilder> map = new Dictionary<Type, IControlBuilder>();
            map.Add(typeof(string), new TextBoxControlBuilder());
            map.Add(typeof(double), new TextBoxControlBuilder());
            map.Add(typeof(float), new TextBoxControlBuilder());
            map.Add(typeof(int), new TextBoxControlBuilder());
            map.Add(typeof(bool), new CheckBoxControlBuilder());
            map.Add(typeof(bool?), new CheckBoxControlBuilder());
            map.Add(typeof(Enum), new EnumControlBuilder());
            //map.Add(typeof(ReferenceString), new ReferenceControlBuilder());
            map.Add(typeof(FilePath), new FileBrowserControlBuilder());
            map.Add(typeof(Guid), new GuidControlBuilder());
            return map;
        }

    }
}
