using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Dtos.OOE.Presets;
using ES_PowerTool.Shared.Services.OOE.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core;

namespace ES_PowerTool.Data.BAL.OOE.Presets
{
    public class CompositePresetElementCRUDService : GenericCRUDService<CompositePresetElementDto, CompositePresetElement>, ICompositePresetElementCRUDService
    {
        public CompositePresetElementCRUDService(Connection connection) 
            : base(connection)
        {
        }

        public CompositePresetElement PersistFromCompositeTypeElement(CompositeTypeElement compositeTypeElement, CompositeType elementType, Preset owningPreset)
        {
            CompositePresetElement compositePresetElement = new CompositePresetElement();
            compositePresetElement.CompositeTypeElementId = compositeTypeElement.Id;
            compositePresetElement.OwningPresetId = owningPreset.Id;
            compositePresetElement.Dtype = "CP";
            compositePresetElement.IncludeInInstantiation = true;
            compositePresetElement.PresetForTypeElementId = elementType.DefaultPresetId.Value;
            compositePresetElement.State = State.NEW;
            compositePresetElement = _genericRepository.Persist<CompositePresetElement>(compositePresetElement);
            return compositePresetElement;
        }
    }
}
