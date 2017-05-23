using Desktop.App.Core.Events.Publishing;
using Desktop.App.Core.Handlers;
using Desktop.Shared.Core;
using ES_PowerTool.Shared;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Publisher.GetInstance().Publish(PublishEvent.CreateCreationEvent(affectedObjectId, executionEvent.GetFirstSelectedTreeNavigationItem().Id));
        }
    }
}
