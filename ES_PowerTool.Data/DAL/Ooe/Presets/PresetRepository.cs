using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ES_PowerTool.Data.DAL.OOE.Presets
{
    public class PresetRepository : BaseRepository
    {
        internal PresetRepository(Connection connection)
            : base(connection)
        {
        }

        internal List<Preset> GetPresetsToExport(Guid projectId)
        {
            return GetContext().Set<Preset>()
                .Where(x => x.ProjectId == projectId && x.State == State.NEW)
                .ToList();
        }

        internal List<Guid> FindPresetIdsToCompositeTypeIds(ICollection<Guid> typeIds)
        {
            return GetContext().Set<Preset>()
                .Where(x => typeIds.Contains(x.TypeId))
                .Select(x => x.Id)
                .ToList();
        }
    }
}
