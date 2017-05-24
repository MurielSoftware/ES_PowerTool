using Desktop.App.Core.Handlers;
using Desktop.Shared.Core;
using ES_PowerTool.Shared.Dtos.OOE.Types;

namespace ES_PowerTool.Handlers
{
    public class NewCompositeTypeHandler : NewEntityHandler<CompositeTypeDto>
    {
        protected override CompositeTypeDto CreateNewDto(ExecutionEvent executionEvent)
        {
            CompositeTypeDto compositeTypeDto = base.CreateNewDto(executionEvent);
            compositeTypeDto.FolderId = executionEvent.GetFirstSelectedTreeNavigationItem().Id;
            compositeTypeDto.ProjectId = executionEvent.GetFirstSelectedTreeNavigationItem().ProjectId;
            compositeTypeDto.State = State.NEW;
            return compositeTypeDto;
        }
    }
}
