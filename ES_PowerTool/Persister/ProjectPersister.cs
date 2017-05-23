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
            FilePath pathPreset = GetDto().PathPreset;
            FilePath pathDefaultPreset = GetDto().PathDefaultPreset;
            FilePath pathTypeType = GetDto().PathTypeType;

            if (pathFolder == null && pathType == null && pathTypeElements == null && pathPreset == null && pathDefaultPreset == null && pathTypeType == null)
            {
                return;
            }
            GetDto().CsvFolders = CSVReader.Read(GetDto().PathFolder.Path);
            GetDto().CsvTypes = CSVReader.Read(GetDto().PathType.Path);
            GetDto().CsvTypeElements = CSVReader.Read(GetDto().PathTypeElement.Path);
            GetDto().CsvPresets = CSVReader.Read(GetDto().PathPreset.Path);
            GetDto().CsvDefaultPreset = CSVReader.Read(GetDto().PathDefaultPreset.Path);
            GetDto().CsvTypeType = CSVReader.Read(GetDto().PathTypeType.Path);
        }
    }
}
