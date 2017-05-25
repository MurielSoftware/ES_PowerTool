using Desktop.App.Core.Handlers;
using Desktop.App.Core.Jobs;
using Desktop.App.Core.Ui.Windows;
using Desktop.App.Core.Utils;
using Desktop.Shared.Core;
using Desktop.Ui.I18n;
using ES_PowerTool.ModelViews;
using ES_PowerTool.Shared.Dtos.Generate;
using ES_PowerTool.Shared.Services;
using ES_PowerTool.Ui.Windows;
using System;
using System.Windows.Threading;

namespace ES_PowerTool.Handlers
{
    public class GenerateLiquibaseHandler : BaseHandler
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            WindowsManager.GetInstance().ShowDialog<GenerateLiquibaseWindow>(new GenerateLiquibaseWindowModelView(executionEvent.GetFirstSelectedTreeNavigationItem()));
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
