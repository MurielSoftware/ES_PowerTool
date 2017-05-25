﻿using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Services
{
    public interface IGenerateLiquibaseService
    {
        List<GenerateLiquibaseCompositeTypeElementTreeNavigationItem> GetCompositeTypeElementsToGenerate(Guid projectId);
        string GenerateCompositeTypeElements(List<GenerateLiquibaseCompositeTypeElementTreeNavigationItem>compositeTypeElementTreeNavigationItemsToGenerate);
    }
}
