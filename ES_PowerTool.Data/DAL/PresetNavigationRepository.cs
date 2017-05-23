using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Data.DAL
{
    public class PresetNavigationRepository : BaseRepository
    {
        public PresetNavigationRepository(Connection connection) 
            : base(connection)
        {
        }

        internal List<PresetTreeNavigationItem> FindPresetsToCompositeType(Guid compositeTypeId)
        {
            return GetContext().Set<Preset>()
                .Where(x => x.TypeId == compositeTypeId)
                .Select(x => new PresetTreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.PRESET, ProjectId = x.ProjectId, IsDefault = x.Type.DefaultPresetId == x.Id })
                .ToList();
        }

        internal TreeNavigationItem FindSpecificPreset(Guid id)
        {
            return GetContext().Set<Preset>()
                .Where(x => x.Id == id)
                .Select(x => new PresetTreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.PRESET, ProjectId = x.ProjectId, IsDefault = x.Type.DefaultPresetId == x.Id })
                .SingleOrDefault();
        }
    }
}
