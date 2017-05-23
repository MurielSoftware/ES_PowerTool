using Desktop.App.Core.Ui.Windows;
using Desktop.Shared.Core.Dtos;
using Desktop.App.Core.Events.Publishing;
using Desktop.App.Core.ModelViews;
using System;

namespace Desktop.App.Core.Handlers
{
    public class NewEntityHandler<T> : BaseHandler
        where T : BaseDto
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            WizardModelView<T> wizardModelView = CreateWizardModelView(executionEvent);
            bool dialogResult = WindowsManager.GetInstance().ShowWizard<Wizard>(wizardModelView);
            if (dialogResult)
            {
                OnSuccessful(executionEvent, wizardModelView.Dto.Id);
            }
            return;
        }

        protected virtual WizardModelView<T> CreateWizardModelView(ExecutionEvent executionEvent)
        {
            return new WizardModelView<T>(CreateNewDto(executionEvent));
        }

        protected virtual T CreateNewDto(ExecutionEvent executionEvent)
        {
            return Activator.CreateInstance<T>();
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Publisher.GetInstance().Publish(PublishEvent.CreateCreationEvent(affectedObjectId, executionEvent.GetFirstSelectedTreeNavigationItem().Id));
        }
    }
}
