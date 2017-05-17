using Desktop.App.Core.Persisters;
using ES_PowerTool.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Services;
using ES_PowerTool.Shared.CSV;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Persister
{
    public class ProjectPersister : BasePersister<ProjectDto>
    {
        public ProjectPersister(ICRUDService<ProjectDto> service, ProjectDto dto) 
            : base(service, dto)
        {
        }

        protected override void BeforePersist()
        {
            GetDto().CsvFolders = CSVFile.Load(GetDto().PathFolder.Path);
            GetDto().CsvTypes = CSVFile.Load(GetDto().PathType.Path);
            GetDto().CsvTypeElements = CSVFile.Load(GetDto().PathTypeElement.Path);
        }
    }
}
