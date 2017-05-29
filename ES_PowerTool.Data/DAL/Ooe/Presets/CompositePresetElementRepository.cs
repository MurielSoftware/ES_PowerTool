using Desktop.Data.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.Model;
using System.Collections.ObjectModel;

namespace ES_PowerTool.Data.DAL.OOE.Presets
{
    public class CompositePresetElementRepository : BaseRepository
    {
        internal CompositePresetElementRepository(Connection connection) 
            : base(connection)
        {
        }

        internal List<CompositePresetElement> FindCompositePresetElementToAssociatedPreset(Guid presetId)
        {
            return GetContext().Set<CompositePresetElement>()
                .Where(x => x.PresetForTypeElementId == presetId)
                .ToList();
        }

        internal List<CompositePresetElement> FindCompositePresetElementsToPresets(ICollection<Guid> presetIds)
        {
            return GetContext().Set<CompositePresetElement>()
                .Where(x => presetIds.Contains(x.OwningPresetId))
                .ToList();
        }
    }
}
