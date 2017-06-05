using Desktop.App.Core.Events.Publishing;
using Desktop.App.Core.Handlers;
using Desktop.Shared.Core.Context;
using ES_PowerTool.ModelViews;
using Log4N.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Handlers
{
    public class CloseProjectHandler : BaseHandler
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            Connection.GetInstance().CloseConnection();
            OnSuccessful(executionEvent, Guid.Empty);
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
            Log.Error("Error during the closing the project");
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Publisher.GetInstance().ServerChanged(null);
            ModelViewsUtil.ProjectIsActive = false;
        }
    }
}
