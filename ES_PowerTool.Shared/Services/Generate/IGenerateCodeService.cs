using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core.Navigations.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Services.Generate
{
    public interface IGenerateCodeService
    {
        GenerateCodeTypeTreeNavigationItem GetTypeToGenerate(Guid owningTypeId);
        GenerateCodeTypeTreeNavigationItem Generate(GenerateCodeTypeTreeNavigationItem generateCodeTypeTreeNavigationItem);
    }
}
