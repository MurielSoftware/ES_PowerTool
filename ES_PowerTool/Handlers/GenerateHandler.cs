using Desktop.App.Core.Handlers;
using Desktop.App.Core.Jobs;
using Desktop.App.Core.Ui.Windows;
using Desktop.Shared.Core;
using ES_PowerTool.ModelViews;
using ES_PowerTool.Shared.Dtos.Generate;
using ES_PowerTool.Shared.Services;
using ES_PowerTool.Ui.Windows;
using System;
using System.Windows.Threading;

namespace ES_PowerTool.Handlers
{
    public class GenerateHandler : BaseHandler
    {
        private GenerateWindow _generateWindow = new GenerateWindow();
        private GenerateDto _generateDto = new GenerateDto();
        private Guid _projectId;

        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            _projectId = executionEvent.GetFirstSelectedTreeNavigationItem().ProjectId;
            try
            {
                LongRunningJob<GenerateDto> projectCreationJob = new LongRunningJob<GenerateDto>(GenerateAction, "Generate");
                projectCreationJob.AddAction(delegate {
                    _generateWindow.Dispatcher.Invoke(AfterGenerateAction, DispatcherPriority.Normal);
                });
                projectCreationJob.Execute(_generateDto);
            }
            catch (Exception ex)
            {

            }
            //WindowsManager.GetInstance().ShowDialog<GenerateWindow>(new GenerateWindowModelView(generateDto));
        }

        private void GenerateAction(GenerateDto generateDto)
        {
            IGenerateService generateService = ServiceActivator.Get<IGenerateService>();
            _generateDto = generateService.Generate(_projectId);
        }

        private void AfterGenerateAction()
        {
            WindowsManager.GetInstance().ShowDialog<GenerateWindow>(new GenerateWindowModelView(_generateDto)); 
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
