using Desktop.App.Core.Handlers;
using Desktop.App.Core.Jobs;
using Desktop.App.Core.Ui.Windows;
using Desktop.Shared.Core;
using ES_PowerTool.ModelViews;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using ES_PowerTool.Ui.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ES_PowerTool.Handlers
{
    public class GenerateHandler : BaseHandler
    {
        private GenerateWindow _generateWindow = new GenerateWindow();
        private GenerateDto _generateDto = new GenerateDto();

        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            //GenerateWindow generateWindow = new GenerateWindow();

            try
            {
                LongRunningJob<GenerateDto> projectCreationJob = new LongRunningJob<GenerateDto>(Generate, "Generate");
                projectCreationJob.AddAction(delegate
                {
                    //generateWindow.ShowDialog();
                    //WindowsManager.GetInstance().ShowDialogThreadSafe(generateWindow);
                    // Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(OpenDialog));
                    //WindowsManager.GetInstance().ShowDialog<GenerateWindow>();
                    _generateWindow.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(OpenDialog));
                });
                projectCreationJob.Execute(_generateDto);
            }
            catch (Exception ex)
            {

            }
        }

        private void OpenDialog()
        {
            WindowsManager.GetInstance().ShowDialogThreadSafe(_generateWindow, new GenerateWindowModelView(_generateDto));
        }

        private void Generate(GenerateDto generateDto)
        {
            IGenerateService generateService = ServiceActivator.Get<IGenerateService>();
            generateDto = generateService.Generate();
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
