using Desktop.Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Desktop.App.Core.Ui.Builders
{
    public abstract class BaseControlBuilder
    {
        protected void CreateLabel(PropertyInfo propertyInfo, Grid grid, int rowIndex)
        {
            Label label = new Label();
            label.Content = GetDisplayName(propertyInfo) + ":";
            label.Margin = new System.Windows.Thickness(0, 2, 0, 2);
            grid.Children.Add(label);
            Grid.SetRow(label, rowIndex);
            Grid.SetColumn(label, 0);

        }

        protected string GetDisplayName(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<LocalizedDisplayNameAttribute>().DisplayName;
        }
    }
}
