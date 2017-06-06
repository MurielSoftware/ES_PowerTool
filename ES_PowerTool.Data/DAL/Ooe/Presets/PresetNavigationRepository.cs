using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Data.DAL.OOE.Presets
{
    public class PresetNavigationRepository : BaseRepository
    {
        internal PresetNavigationRepository(Connection connection) 
            : base(connection)
        {
        }

        internal List<TreeNavigationItem> FindPresetsToCompositeType(Guid compositeTypeId)
        {
            return GetContext().Set<Preset>()
                .Where(x => x.TypeId == compositeTypeId)
                .Select(x => new PresetTreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.PRESET, ProjectId = x.ProjectId, IsDefault = x.Type.DefaultPresetId == x.Id, HasRemoteChildren = x.Elements.Count > 0, State = x.State })
                .ToList()
                .Cast<TreeNavigationItem>()
                .ToList();
        }

        internal TreeNavigationItem FindSpecificPreset(Guid id)
        {
            return GetContext().Set<Preset>()
                .Where(x => x.Id == id)
                .Select(x => new PresetTreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.PRESET, ProjectId = x.ProjectId, IsDefault = x.Type.DefaultPresetId == x.Id, HasRemoteChildren = x.Elements.Count > 0, State = x.State })
                .SingleOrDefault();
        }

        internal List<TreeNavigationItem> FindPresetsToCompositeTypeElementElementType(Guid compositeTypeElementId)
        {
            //return GetContext().Set<Preset>()
            //    //.Where(x => x.Type.Children.Any(y => y.ElementTypeId == compositeTypeElementId))
            //    .Where(x => x.)
            //    .Select(x => new PresetTreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.PRESET, ProjectId = x.ProjectId, IsDefault = x.Type.DefaultPresetId == x.Id, HasRemoteChildren = x.Elements.Count > 0, BuiltIn = x.BuiltIn })
            //    .ToList()
            //    .Cast<TreeNavigationItem>()
            //    .ToList();
            Guid elementTypeId = GetContext().Set<CompositeTypeElement>().Where(x => x.Id == compositeTypeElementId).Select(x => x.ElementTypeId).SingleOrDefault();
            return GetContext().Set<Preset>()
                .Where(x => x.TypeId == elementTypeId)
                .Select(x => new PresetTreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.PRESET, ProjectId = x.ProjectId, IsDefault = x.Type.DefaultPresetId == x.Id, HasRemoteChildren = x.Elements.Count > 0, State = x.State })
                .ToList()
                .Cast<TreeNavigationItem>()
                .ToList();
            //return GetContext().Set<Preset>()
            //    .Join(GetContext().Set<CompositeType>, x => x.TypeId, y => y.Id, (x, y))
        }
    }
}
