using Desktop.Shared;
using Desktop.Ui.Core.ModelViews;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.ModelViews
{
    public class CompositeTypeTreeModelView : BaseTreeModelView
    {
        public CompositeTypeTreeModelView() 
            : base("CompositeTypeTreeModelView")
        {
        }

        public override void SetService()
        {
            _service = ServiceActivator.Get<ICompositeTypeNavigationService>();
        }
    }
}
