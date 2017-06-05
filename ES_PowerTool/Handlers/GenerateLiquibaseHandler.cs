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
using Log4N.Logger;
using System;
using System.Windows.Threading;

namespace ES_PowerTool.Handlers
{
    public class GenerateLiquibaseHandler : BaseHandler
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            WindowsManager.GetInstance().ShowDialog<GenerateLiquibaseWindow>(new GenerateLiquibaseWindowModelView(executionEvent.GetFirstSelectedTreeNavigationItem()));
            OnSuccessful(executionEvent, Guid.Empty);
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
            Log.Error("Error during the liquibase generating");
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Log.Info("Generating of the liquibase was success");
        }
    }
}
