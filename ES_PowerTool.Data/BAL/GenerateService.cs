using Desktop.Shared.Core.Services;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.CSV;
using ES_PowerTool.Data.DAL.OOE.Presets;
using ES_PowerTool.Data.DAL.OOE.Elements;
using ES_PowerTool.Data.DAL.OOE;
using ES_PowerTool.Data.DAL.OOE.Types;
using ES_PowerTool.Shared.Dtos.Generate;
using ES_PowerTool.Shared.Dtos.OOE.Types;
using ES_PowerTool.Shared.Dtos.OOE;
using ES_PowerTool.Shared.Dtos.OOE.Elements;
using ES_PowerTool.Shared.Dtos.OOE.Presets;

namespace ES_PowerTool.Data.BAL
{
    public class GenerateService : BaseService, IGenerateService
    {
        private FolderRepository _folderRepository;
        private CompositeTypeRepository _compositeTypeRepository;
        private CompositeTypeElementRepository _compositeTypeElementRepository;
        private PresetRepository _presetRepository;

        public GenerateService(Connection connection) 
            : base(connection)
        {
            _folderRepository = new FolderRepository(connection);
            _compositeTypeRepository = new CompositeTypeRepository(connection);
            _compositeTypeElementRepository = new CompositeTypeElementRepository(connection);
            _presetRepository = new PresetRepository(connection);
        }

        public GenerateDto Generate(Guid projectId)
        {
            GenerateDto generateDto = new GenerateDto();
            List<Folder> folders = _folderRepository.FindFoldersToExport(projectId);
            List<CompositeType> compositeTypes = _compositeTypeRepository.GetCompositeTypesToExport(projectId);
            List<CompositeTypeElement> compositeTypeElements = _compositeTypeElementRepository.FindCompositeTypeElementsToExport(projectId);
            List<Preset> presets = _presetRepository.GetPresetsToExport(projectId);
            List<DefaultPresetGenearateDto> defaultPresetGenerateDtos = CreateDefaultPresetGenerateDtos(compositeTypes);
            List<JoinTypeTypeGenerateDto> joinTypeTypeGenerateDtos = CreateJoinTypeTypeGenerateDtos(compositeTypes);


            generateDto.GeneratedCSVFolder = CSVWriter.Write<FolderDto>(folders);
            generateDto.GeneratedCSVType = CSVWriter.Write<CompositeTypeDto>(compositeTypes);
            generateDto.GeneratedCSVTypeElement = CSVWriter.Write<CompositeTypeElementDto>(compositeTypeElements);
            generateDto.GeneratedCSVPreset = CSVWriter.Write<PresetDto>(presets);
            generateDto.GeneratedCSVDefaultPreset = CSVWriter.Write<DefaultPresetGenearateDto>(defaultPresetGenerateDtos);
            generateDto.GeneratedCSVTypeType = CSVWriter.Write<JoinTypeTypeGenerateDto>(joinTypeTypeGenerateDtos);
            return generateDto;
        }

        private List<DefaultPresetGenearateDto> CreateDefaultPresetGenerateDtos(List<CompositeType> compositeTypes)
        {
            List<DefaultPresetGenearateDto> defaultPresetGenerateDtos = new List<DefaultPresetGenearateDto>();
            foreach (CompositeType compositeType in compositeTypes)
            {
                if (compositeType.DefaultPresetId.HasValue)
                {
                    DefaultPresetGenearateDto defaultPresetGenerateDto = new DefaultPresetGenearateDto();
                    defaultPresetGenerateDto.Id = compositeType.Id;
                    defaultPresetGenerateDto.DefaultPresetId = compositeType.DefaultPresetId.Value;
                    defaultPresetGenerateDtos.Add(defaultPresetGenerateDto);
                }
            }
            return defaultPresetGenerateDtos;
        }

        private List<JoinTypeTypeGenerateDto> CreateJoinTypeTypeGenerateDtos(List<CompositeType> compositeTypes)
        {
            List<JoinTypeTypeGenerateDto> joinTypeTypeGenerateDtos = new List<JoinTypeTypeGenerateDto>();
            foreach (CompositeType compositeType in compositeTypes)
            {

                ICollection<CompositeType> superTypes = compositeType.SuperTypes;
                if(superTypes != null)
                {                
                    foreach(CompositeType superType in superTypes)
                    {
                        JoinTypeTypeGenerateDto joinTypeTypeGenerateDto = new JoinTypeTypeGenerateDto();
                        joinTypeTypeGenerateDto.SubTypeId = compositeType.Id;
                        joinTypeTypeGenerateDto.SuperTypeId = superType.Id;
                        joinTypeTypeGenerateDto.SortValue = 0;
                        joinTypeTypeGenerateDtos.Add(joinTypeTypeGenerateDto);
                    }
                }
            }
            return joinTypeTypeGenerateDtos;
        }
    }
}
