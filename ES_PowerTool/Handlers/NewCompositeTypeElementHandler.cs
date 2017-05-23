using Desktop.App.Core.Handlers;
using Desktop.Shared.Core;
using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Handlers
{
    public class NewCompositeTypeElementHandler : NewEntityHandler<CompositeTypeElementDto>
    {
        protected override CompositeTypeElementDto CreateNewDto(ExecutionEvent executionEvent)
        {
            CompositeTypeElementDto compositeTypeElementDto = base.CreateNewDto(executionEvent);
            compositeTypeElementDto.OwningTypeId = executionEvent.GetFirstSelectedTreeNavigationItem().Id;
            compositeTypeElementDto.ProjectId = executionEvent.GetFirstSelectedTreeNavigationItem().ProjectId;
            compositeTypeElementDto.State = State.NEW;
            return compositeTypeElementDto;
        }
    }
}
