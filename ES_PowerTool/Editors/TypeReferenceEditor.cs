using Desktop.App.Core.Editors;
using System.Collections.Generic;
using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core;
using ES_PowerTool.Shared.Services.OOE.Types;
using ES_PowerTool.Shared.Dtos.OOE.Elements;

namespace ES_PowerTool.Editors
{
    public class TypeReferenceEditor : BaseReferenceEditor<CompositeTypeElementDto>
    {
        public TypeReferenceEditor(CompositeTypeElementDto dto) 
            : base(dto)
        {
        }

        protected override List<TreeNavigationItem> DoGetProposals()
        {
            ICompositeTypeNavigationService compositeTypeNavigationService = ServiceActivator.Get<ICompositeTypeNavigationService>();
            return compositeTypeNavigationService.GetAllTypes();
        }
    }
}
