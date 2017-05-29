using Desktop.App.Core.Editors;
using System.Collections.Generic;
using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core;
using ES_PowerTool.Shared.Services.OOE.Types;
using ES_PowerTool.Shared.Dtos.OOE.Types;

namespace ES_PowerTool.Editors
{
    public class TypeReferenceDerivableEditor : BaseReferenceEditor<CompositeTypeDto>
    {
        public TypeReferenceDerivableEditor(CompositeTypeDto dto) 
            : base(dto)
        {
        }

        protected override List<TreeNavigationItem> DoGetProposals()
        {
            ICompositeTypeNavigationService compositeTypeNavigationService = ServiceActivator.Get<ICompositeTypeNavigationService>();
            return compositeTypeNavigationService.GetAllDerivableCompositeTypes();
        }
    }
}
