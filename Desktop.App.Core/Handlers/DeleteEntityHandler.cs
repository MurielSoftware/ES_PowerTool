using Desktop.Shared.Core;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core.Services;
using Desktop.App.Core.Events.Publishing;
using System;
using System.Collections.Generic;
using Desktop.Shared.Core.Validations;
using System.Windows;
using Desktop.Ui.I18n;
using Desktop.App.Core.Utils;
using Desktop.App.Core.Ui.Windows;
using Desktop.App.Core.ModelViews;
using Desktop.Shared.Core.Context;
using Log4N.Logger;

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
            }
        }

        protected virtual void Delete(ExecutionEvent executionEvent, Guid id)
        {
            ICRUDService<T> crudService = (ICRUDService<T>)ServiceActivator.Get(HandlerUtils.DTO_TO_SERVICE[typeof(T)]);
            Connection.GetInstance().StartTransaction();
            crudService.Delete(id);
            Connection.GetInstance().EndTransaction();
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
            Log.Error("Error during the deletion");
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Log.Info(string.Format("Entity '{0}' was deleted", affectedObjectId));
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
