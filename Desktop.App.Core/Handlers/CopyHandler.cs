using Log4N.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.App.Core.Handlers
{
    public class CopyHandler : BaseHandler
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            Clipboard.SetData("copy", executionEvent.GetSelectedTreeNavigationItems());
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
            Log.Error("Error during the copying");
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
        }
    }
}
