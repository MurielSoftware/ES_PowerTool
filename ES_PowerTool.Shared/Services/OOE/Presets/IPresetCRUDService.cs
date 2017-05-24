using Desktop.Shared.Core.Services;
using ES_PowerTool.Shared.Dtos.OOE.Presets;
using System;

namespace ES_PowerTool.Shared.Services.OOE.Presets
{
    public interface IPresetCRUDService : ICRUDService<PresetDto>
    {
        void SetAsDefault(Guid presetId, Guid owningTypeId);
    }
}
