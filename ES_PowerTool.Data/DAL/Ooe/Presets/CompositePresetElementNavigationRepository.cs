using Desktop.Data.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Navigations;
using Desktop.Data.Core.Model;

namespace ES_PowerTool.Data.DAL.Ooe.Presets
{
    public class CompositePresetElementNavigationRepository : BaseRepository
    {
        public CompositePresetElementNavigationRepository(Connection connection) 
            : base(connection)
        {
        }

        internal List<TreeNavigationItem> FindCompositePresetElementsToPreset(Guid presetId)
        {
            return GetContext().Set<CompositePresetElement>()
                .Where(x => x.OwningPresetId == presetId)
                .Select(x => new CompositePresetElementTreeNavigationItem() { Id = x.Id, CompositeTypeElementName = x.CompositeTypeElement.Description, CompositeTypeElementElementTypeName = x.CompositeTypeElement.ElementType.Description, AssociatedPresetName = x.PresetForTypeElement.Name, Type = NavigationType.COMPOSITE_PRESET_ELEMENT })
                .ToList()
                .Cast<TreeNavigationItem>()
                .ToList();
        }

        internal TreeNavigationItem FindSpecificCompositePresetElement(Guid id)
        {
            return GetContext().Set<CompositePresetElement>()
                .Where(x => x.Id == id)
                .Select(x => new CompositePresetElementTreeNavigationItem() { Id = x.Id, CompositeTypeElementName = x.CompositeTypeElement.Description, CompositeTypeElementElementTypeName = x.CompositeTypeElement.ElementType.Description, AssociatedPresetName = x.PresetForTypeElement.Name, Type = NavigationType.COMPOSITE_PRESET_ELEMENT })
                .SingleOrDefault();
        }
    }
}
