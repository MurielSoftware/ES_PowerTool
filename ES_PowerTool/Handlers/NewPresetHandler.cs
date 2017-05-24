using Desktop.App.Core.Events.Publishing;
using Desktop.App.Core.Handlers;
using Desktop.Shared.Core;
using ES_PowerTool.Shared;
using ES_PowerTool.Shared.Dtos.OOE.Presets;
using System;

namespace ES_PowerTool.Handlers
{
    public class NewPresetHandler : NewEntityHandler<PresetDto>
    {
        protected override PresetDto CreateNewDto(ExecutionEvent executionEvent)
        {
            PresetDto presetDto = base.CreateNewDto(executionEvent);
            presetDto.TypeId = executionEvent.GetFirstSelectedTreeNavigationItem().Id;
            presetDto.ProjectId = executionEvent.GetFirstSelectedTreeNavigationItem().ProjectId;
            presetDto.State = State.NEW;
            return presetDto;
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Publisher.GetInstance().Publish(PublishEvent.CreateCreationEvent(IdConstants.PRESET_FOLDER_ID, executionEvent.GetFirstSelectedTreeNavigationItem().Id));
        }
    }
}
