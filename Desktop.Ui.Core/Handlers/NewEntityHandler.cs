using Desktop.Shared.Core.Dtos;
using Desktop.Ui.Core.Events.Publishing;
using Desktop.Ui.Core.Handlers;
using Desktop.Ui.Core.ModelViews;
using Desktop.Ui.Core.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Ui.Core.Handlers
{
    public class NewEntityHandler<T> : BaseHandler
        where T : BaseDto
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            WizardModelView<T> wizardModelView = new WizardModelView<T>();
            //BeforePersist(executionEvent, wizardModelView.Dto);
            bool dialogResult = WindowsManager.GetInstance().ShowWizard<Wizard>(wizardModelView);
            if (dialogResult)
            {
                //AfterPersist(executionEvent, wizardModelView.Dto);
                OnSuccessful(executionEvent, wizardModelView.Dto.Id);
            }
            return;
        }

        //protected virtual void AfterPersist(ExecutionEvent executionEvent, T dto)
        //{
        //}

        //protected virtual void BeforePersist(ExecutionEvent executionEvent, T dto)
        //{
        //}

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Publisher.GetInstance().Publish(PublishEvent.CreateCreationEvent(affectedObjectId, executionEvent.GetFirstTreeNavigationItem().Id));
        }
    }
}
