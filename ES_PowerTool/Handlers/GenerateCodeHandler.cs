using Desktop.App.Core.Handlers;
using Desktop.App.Core.Ui.Windows;
using ES_PowerTool.ModelViews;
using ES_PowerTool.Ui.Windows;
using Log4N.Logger;
using System;

namespace ES_PowerTool.Handlers
{
    public class GenerateCodeHandler : BaseHandler
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            WindowsManager.GetInstance().ShowDialog<GenerateCodeWindow>(new GenerateCodeWindowModelView(executionEvent.GetFirstSelectedTreeNavigationItem()));
            OnSuccessful(executionEvent, Guid.Empty);
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
            Log.Error("Error during the code generating");
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Log.Info("Code generating was success");
        }
    }
}
