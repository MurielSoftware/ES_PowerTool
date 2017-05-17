using Desktop.App.Core.Events.Publishing;
using Desktop.App.Core.ModelViews;
using Desktop.App.Core.Ui.Windows;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Handlers
{
    public class UpdateEntityHandler<T> : BaseHandler
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
            return new WizardModelView<T>(LoadDto(executionEvent));
        }

        protected virtual T LoadDto(ExecutionEvent executionEvent)
        {

            ICRUDService<T> crudService = (ICRUDService<T>)ServiceActivator.Get(HandlerUtils.DTO_TO_SERVICE[typeof(T)]);
            //Connection.GetInstance().StartTransaction();
            T dto = crudService.Read(executionEvent.GetFirstTreeNavigationItem().Id);
            //Connection.GetInstance().EndTransaction();
            return dto;
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Publisher.GetInstance().Publish(PublishEvent.CreateUpdateEvent(affectedObjectId, executionEvent.GetFirstTreeNavigationItem().Id));
        }
    }
}
