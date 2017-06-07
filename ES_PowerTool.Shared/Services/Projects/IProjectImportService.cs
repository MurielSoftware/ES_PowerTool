using Desktop.Shared.Core.Jobs;
using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Services.Projects
{
    public interface IProjectImportService
    {
        void Import(ProgressCounter progressCounter, ProjectDto projectDto);
    }
}
