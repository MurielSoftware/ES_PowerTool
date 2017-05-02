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

namespace Desktop.Ui.Core.Builders
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
            return properties.GroupBy(x => ((LocalizedCategoryAttribute)x.GetCustomAttribute(typeof(LocalizedCategoryAttribute))).Category).ToDictionary(y => y.Key, y => y.ToList());
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
            map.Add(typeof(Enum), new EnumControlBuilder());
            map.Add(typeof(ReferenceString), new ReferenceControlBuilder());
            return map;
        }

    }
}
