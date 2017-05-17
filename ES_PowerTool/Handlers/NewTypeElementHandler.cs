using Desktop.App.Core.Handlers;
using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Handlers
{
    public class NewTypeElementHandler : NewEntityHandler<CompositeTypeElementDto>
    {
        protected override CompositeTypeElementDto CreateNewDto(ExecutionEvent executionEvent)
        {
            CompositeTypeElementDto compositeTypeElementDto = base.CreateNewDto(executionEvent);
            compositeTypeElementDto.OwningTypeId = executionEvent.GetFirstTreeNavigationItem().Id;
            return compositeTypeElementDto;
        }
    }
}
