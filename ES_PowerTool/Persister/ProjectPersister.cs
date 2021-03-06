﻿using Desktop.App.Core.Persisters;
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
using Desktop.Shared.Core;
using ES_PowerTool.Shared.Services.Projects;
using Desktop.Shared.Core.Jobs;

namespace ES_PowerTool.Persister
{
    public class ProjectPersister : BasePersister<ProjectDto>
    {
        private ProgressCounter _progressCounter;

        public ProjectPersister(ICRUDService<ProjectDto> service, ProgressCounter progressCounter, ProjectDto dto) 
            : base(service, dto)
        {
            _progressCounter = progressCounter;
        }


        public override void Persist()
        {
            BeforePersist();
            IProjectImportService projectImportService = ServiceActivator.Get<IProjectImportService>();
            projectImportService.Import(_progressCounter, GetDto());
            AfterPersist();
        }

        protected override void BeforePersist()
        {
            FilePath pathFolder = GetDto().PathFolder;
            FilePath pathType = GetDto().PathType;
            FilePath pathTypeElements = GetDto().PathTypeElement;
            FilePath pathPreset = GetDto().PathPreset;
            FilePath pathDefaultPreset = GetDto().PathDefaultPreset;
            FilePath pathTypeType = GetDto().PathTypeType;
            FilePath pathPresetElements = GetDto().PathPresetElement;

            if (pathFolder == null && pathType == null && pathTypeElements == null && pathPreset == null && pathDefaultPreset == null && pathTypeType == null && pathPresetElements == null)
            {
                return;
            }
            GetDto().CsvFolders = CSVReader.Read(GetDto().PathFolder.Path);
            GetDto().CsvTypes = CSVReader.Read(GetDto().PathType.Path);
            GetDto().CsvTypeElements = CSVReader.Read(GetDto().PathTypeElement.Path);
            GetDto().CsvPresets = CSVReader.Read(GetDto().PathPreset.Path);
            GetDto().CsvPresetElements = CSVReader.Read(GetDto().PathPresetElement.Path);
            GetDto().CsvDefaultPreset = CSVReader.Read(GetDto().PathDefaultPreset.Path);
            GetDto().CsvTypeType = CSVReader.Read(GetDto().PathTypeType.Path);
        }

        protected override void AfterPersist()
        {
        }
    }
}
