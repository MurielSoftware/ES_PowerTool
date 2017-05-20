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
using Desktop.Shared.DataTypes;

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
            FilePath pathFolder = GetDto().PathFolder;
            FilePath pathType = GetDto().PathType;
            FilePath pathTypeElements = GetDto().PathTypeElement;
            if(pathFolder == null && pathType == null && pathTypeElements == null)
            {
                return;
            }
            GetDto().CsvFolders = CSVReader.Read(GetDto().PathFolder.Path);
            GetDto().CsvTypes = CSVReader.Read(GetDto().PathType.Path);
            GetDto().CsvTypeElements = CSVReader.Read(GetDto().PathTypeElement.Path);
        }
    }
}
