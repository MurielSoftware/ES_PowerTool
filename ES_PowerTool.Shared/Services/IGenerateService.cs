using ES_PowerTool.Shared.Dtos.Generate;
using System;

namespace ES_PowerTool.Shared.Services
{
    public interface IGenerateService
    {
        GenerateDto Generate(Guid projectId);
    }
}
