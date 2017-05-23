using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Data.BAL
{
    public class PresetCRUDService : GenericCRUDService<PresetDto, Preset>, IPresetCRUDService
    {
        public PresetCRUDService(Connection connection) 
            : base(connection)
        {
        }

        public void SetAsDefault(Guid presetId, Guid owningTypeId)
        {
            CompositeType owningType = _genericRepository.Find<CompositeType>(owningTypeId);
            if(owningType.DefaultPresetId.Equals(presetId))
            {
                return;
            }
            owningType.DefaultPresetId = presetId;
            _genericRepository.Persist<CompositeType>(owningType);
        }

        protected override Preset DoPersist(Preset preset)
        {
            preset = base.DoPersist(preset);
            CompositeType type = preset.Type;
            if (!type.DefaultPresetId.HasValue)
            {
                type.DefaultPresetId = preset.Id;
                _genericRepository.Persist<CompositeType>(type);
            }
            return preset;
        }
    }
}
