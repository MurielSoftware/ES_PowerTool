﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Desktop.Ui.Core.Builders
{
    public abstract class BaseControlBuilder
    {
        protected void CreateLabel(PropertyInfo propertyInfo, Grid grid, int rowIndex)
        {
            Label label = new Label();
            label.Content = GetDisplayName(propertyInfo);
            grid.Children.Add(label);
            Grid.SetRow(label, rowIndex);
            Grid.SetColumn(label, 0);
        }

        protected string GetDisplayName(PropertyInfo propertyInfo)
        {
            return TypeUtils.GetAttribute<LocalizedDisplayNameAttribute>(propertyInfo).DisplayName;
        }
    }
}