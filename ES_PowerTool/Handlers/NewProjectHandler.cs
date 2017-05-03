using Desktop.Ui.Core.Handlers;
using Desktop.Ui.Core.Windows;
using ES_PowerTool.ModelViews;
using ES_PowerTool.Shared;
using ES_PowerTool.Shared.Services;
using System;

namespace ES_PowerTool.Handlers
{
    public class NewProjectHandler : BaseHandler
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            NewProjectModelView newProjectModelView = new NewProjectModelView();
            WindowsManager.GetInstance().ShowWizard<Wizard>(newProjectModelView);
            ServiceActivator serviceActivator = new ServiceActivator();

            ICompositeTypeCRUDService compositeTypeCRUDService = serviceActivator.Get<ICompositeTypeCRUDService>();
            compositeTypeCRUDService.Read(Guid.NewGuid());
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent)
        {
        }
    }
}
