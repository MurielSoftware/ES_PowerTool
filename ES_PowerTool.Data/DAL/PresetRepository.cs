using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ES_PowerTool.Data.DAL
{
    public class PresetRepository : BaseRepository
    {
        public PresetRepository(Connection connection)
            : base(connection)
        {
        }

        public List<Preset> GetPresetsToExport(Guid projectId)
        {
            return GetContext().Set<Preset>()
                .Where(x => x.ProjectId == projectId && x.State == State.NEW)
                .ToList();
        }
    }
}
