using Desktop.App.Core.Handlers;
using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Handlers
{
    public class NewTypeHandler : NewEntityHandler<CompositeTypeDto>
    {
        protected override CompositeTypeDto CreateNewDto(ExecutionEvent executionEvent)
        {
            CompositeTypeDto compositeTypeDto = base.CreateNewDto(executionEvent);
            compositeTypeDto.FolderId = executionEvent.GetFirstTreeNavigationItem().Id;
            return compositeTypeDto;
        }
    }
}
