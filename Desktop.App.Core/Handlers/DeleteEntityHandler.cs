using Desktop.Shared.Core;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core.Services;
using Desktop.App.Core.Events.Publishing;
using Desktop.App.Core.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Validations;
using System.Windows;
using Desktop.Ui.I18n;
using Desktop.App.Core.Utils;
using Desktop.App.Core.Ui.Windows;
using Desktop.App.Core.ModelViews;

namespace Desktop.App.Core.Handlers
{
    public class DeleteEntityHandler<T> : BaseHandler
        where T : BaseDto
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            try
            {
                ICollection<Guid> idsToRemove = TreeNavigationItem.CollectIds(executionEvent.GetSelectedTreeNavigationItems());
                string questionMessage = GetQuestionMessage(idsToRemove.Count, executionEvent.GetFirstSelectedTreeNavigationItem().Name);
                if (MessageBoxResult.No.Equals(MessageDialogUtils.Question(questionMessage, executionEvent.GetFirstSelectedTreeNavigationItem().Name)))
                {
                    return;
                }

                foreach (Guid idToRemove in idsToRemove)
                {
                    Delete(executionEvent, idToRemove);
                    OnSuccessful(executionEvent, idToRemove);
                }
            }
            catch (ValidationException ex)
            {
                WindowsManager.GetInstance().ShowDialog<MessageWindow>(new MessageWindowModelView(ex.GetValidationResult()));
                //MessageDialogUtils.ErrorMessage(ValidationResultUtils.FormatValidationMessage(ex.GetValidationResult()));
            }
        }

        protected virtual void Delete(ExecutionEvent executionEvent, Guid id)
        {
            ICRUDService<T> crudService = (ICRUDService<T>)ServiceActivator.Get(HandlerUtils.DTO_TO_SERVICE[typeof(T)]);
            T dto = crudService.Read(id);
            crudService.Delete(id);
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Publisher.GetInstance().Publish(PublishEvent.CreateDeletionEvent(affectedObjectId, null));
        }

        private string GetQuestionMessage(int itemsCountToRemove, string name)
        {
            string questionMessage = MessageKeyConstants.QUESTION_DO_YOU_WANT_TO_REMOVE_OBJECTS_MESSAGE;
            if (itemsCountToRemove == 1)
            {
                questionMessage = ResourceUtils.GetMessage(MessageKeyConstants.QUESTION_DO_YOU_WANT_TO_REMOVE_OBJECT_MESSAGE, name);
            }
            return questionMessage;
        }
    }
}
