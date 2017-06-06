using Desktop.App.Core.Events.Selection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.ModelViews
{
    public abstract class BaseModelView : PropertyChangedProvider
    {
        private string _modelViewId;
        protected MasterSelectionChangeService _masterSelectionChangeService = new MasterSelectionChangeService();

        public BaseModelView(string modelViewId)
        {
            _modelViewId = modelViewId;
        }

        public string GetModelViewId()
        {
            return _modelViewId;
        }
    }
}
