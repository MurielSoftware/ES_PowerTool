using Desktop.Data.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.Model;

namespace ES_PowerTool.Data.DAL.Ooe.Presets
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
    }
}
