﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Navigations.Generate
{
    public class GenerateLiquibaseCompositeTypeElementTreeNavigationItem 
    {
        public CompositeTypeElementTreeNavigationItem CompositeTypeElementTreeNavigationItem { get; set; }
        public string ColumnName { get; set; }
        public bool Generate { get; set; }
    }
}
