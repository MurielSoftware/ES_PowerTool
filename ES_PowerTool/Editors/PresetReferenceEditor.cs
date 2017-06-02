using Desktop.App.Core.Editors;
using ES_PowerTool.Shared.Dtos.OOE.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Navigations;
using ES_PowerTool.Shared.Services.OOE.Types;
using Desktop.Shared.Core;

namespace ES_PowerTool.Editors
{
    public class PresetReferenceEditor : BaseReferenceEditor<CompositePresetElementDto>
    {
        public PresetReferenceEditor(CompositePresetElementDto dto) 
            : base(dto)
        {
        }

        protected override List<TreeNavigationItem> DoGetProposals()
        {
            ICompositeTypeNavigationService compositeTypeNavigationService = ServiceActivator.Get<ICompositeTypeNavigationService>();
            return compositeTypeNavigationService.GetPresetsToCompositeTypeElement(GetDto().CompositeTypeElementId);
        }
    }
}
