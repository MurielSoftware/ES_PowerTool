using Desktop.App.Core.Events.Publishing;
using Desktop.App.Core.Handlers;
using Desktop.Shared.Core;
using ES_PowerTool.Shared;
using ES_PowerTool.Shared.Services.OOE.Presets;
using Log4N.Logger;
using System;

namespace ES_PowerTool.Handlers
{
    public class DefaultHandler : BaseHandler
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            IPresetCRUDService presetCRUDService = ServiceActivator.Get<IPresetCRUDService>();
            presetCRUDService.SetAsDefault(executionEvent.GetFirstSelectedTreeNavigationItem().Id, executionEvent.GetMasterTreeNavigationItem().Id);
            OnSuccessful(executionEvent, IdConstants.PRESET_FOLDER_ID);
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
            Log.Error("Error during the set the default to the entity");
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Publisher.GetInstance().Publish(PublishEvent.CreateCreationEvent(affectedObjectId, executionEvent.GetFirstSelectedTreeNavigationItem().Id));
        }
    }
}
