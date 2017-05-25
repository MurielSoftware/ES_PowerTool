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
        public CompositePresetElementRepository(Connection connection) 
            : base(connection)
        {
        }

        public List<CompositePresetElement> FindCompositePresetElementToAssociatedPreset(Guid presetId)
        {
            return GetContext().Set<CompositePresetElement>()
                .Where(x => x.PresetForTypeElementId == presetId)
                .ToList();
        }

        public List<CompositePresetElement> FindCompositePresetElementsToPresets(ICollection<Guid> presetIds)
        {
            return GetContext().Set<CompositePresetElement>()
                .Where(x => presetIds.Contains(x.OwningPresetId))
                .ToList();
        }
    }
}
