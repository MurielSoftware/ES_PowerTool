using Desktop.App.Core.Handlers;
using Desktop.App.Core.Jobs;
using Desktop.App.Core.Ui.Windows;
using Desktop.App.Core.Utils;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Jobs;
using Desktop.Ui.I18n;
using ES_PowerTool.ModelViews;
using ES_PowerTool.Shared.Dtos.Generate;
using ES_PowerTool.Shared.Services.Generate;
using ES_PowerTool.Ui.Windows;
using Log4N.Logger;
using System;
using System.Windows.Threading;

namespace ES_PowerTool.Handlers
{
    public class GenerateCSVHandler : BaseHandler
    {
        private GenerateCSVWindow _generateWindow = new GenerateCSVWindow();
        private GenerateDto _generateDto = new GenerateDto();
        private Guid _projectId;

        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            _projectId = executionEvent.GetFirstSelectedTreeNavigationItem().ProjectId;
            try
            {
                ProgressCounter progressCounter = new ProgressCounter("Generate", "Generating the CSV files...", 1);
                LongRunningJob<GenerateDto> projectCreationJob = new LongRunningJob<GenerateDto>(GenerateAction, progressCounter);
                projectCreationJob.AddAction(delegate {
                    _generateWindow.Dispatcher.Invoke(AfterGenerateAction, DispatcherPriority.Normal);
                });
                projectCreationJob.Execute(_generateDto);
            }
            catch (Exception ex)
            {
                OnFailure(executionEvent);
            }
        }

        private void GenerateAction(ProgressCounter progressCounter, GenerateDto generateDto)
        {
            IGenerateCSVService generateService = ServiceActivator.Get<IGenerateCSVService>();
            _generateDto = generateService.Generate(_projectId);
        }

        private void AfterGenerateAction()
        {
            WindowsManager.GetInstance().ShowDialog<GenerateCSVWindow>(new GenerateCSVWindowModelView(_generateDto));
            OnSuccessful(null, Guid.Empty);
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
            MessageDialogUtils.ErrorMessage(MessageKeyConstants.ERROR_MESSAGE_GENERATE_CSV_FAILED);
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Log.Info("Generate CSV was success");
        }
    }
}
