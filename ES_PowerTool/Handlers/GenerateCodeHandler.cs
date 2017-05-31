using Desktop.App.Core.Handlers;
using Desktop.App.Core.Ui.Windows;
using ES_PowerTool.ModelViews;
using ES_PowerTool.Ui.Windows;
using System;

namespace ES_PowerTool.Handlers
{
    public class GenerateCodeHandler : BaseHandler
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            WindowsManager.GetInstance().ShowDialog<GenerateCodeWindow>(new GenerateCodeWindowModelView(executionEvent.GetFirstSelectedTreeNavigationItem()));
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
            throw new NotImplementedException();
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            throw new NotImplementedException();
        }
    }
}
