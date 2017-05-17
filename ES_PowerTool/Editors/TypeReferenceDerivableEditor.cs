using Desktop.App.Core.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core;
using ES_PowerTool.Shared.Services;

namespace ES_PowerTool.Editors
{
    public class TypeReferenceDerivableEditor : BaseReferenceEditor
    {
        protected override List<TreeNavigationItem> DoGetProposals()
        {
            ICompositeTypeNavigationService compositeTypeNavigationService = ServiceActivator.Get<ICompositeTypeNavigationService>();
            return compositeTypeNavigationService.GetAllDerivableCompositeTypes();
        }
    }
}
