using ES_PowerTool.Shared.Dtos.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Services.Generate
{
    public interface IGenerateGuidService
    {
        GenerateGuidDto Generate(GenerateGuidDto generateGuidDto);
    }
}
