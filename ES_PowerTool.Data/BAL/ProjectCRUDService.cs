using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Shared.CSV;
using Desktop.Shared.Core.Attributes;
using System.Reflection;
using Desktop.Shared.Utils;
using Desktop.Shared.Core.Dtos;
using Desktop.Data.Core.Model;
using Desktop.Data.Core.BAL;
using Desktop.Shared.Core;

namespace ES_PowerTool.Data.BAL
{
    public class ProjectCRUDService : GenericCRUDService<ProjectDto, Project>, IProjectCRUDService
    {
        public ProjectCRUDService(Connection connection) 
            : base(connection)
        {
        }

        public override ProjectDto Persist(ProjectDto projectDto)
        {
            ProjectDto persistedProjectDto = base.Persist(projectDto);
            PersistEntities<Folder, FolderDto>(persistedProjectDto.Id, projectDto.CsvFolders);
            PersistEntities<CompositeType, CompositeTypeDto>(persistedProjectDto.Id, projectDto.CsvTypes, "COM");
            PersistEntities<PrimitiveType, PrimitiveTypeDto>(persistedProjectDto.Id, projectDto.CsvTypes, "PRI");
            PersistEntities<CompositeTypeElement, CompositeTypeElementDto>(persistedProjectDto.Id, projectDto.CsvTypeElements);
            PersistEntities<Preset, PresetDto>(persistedProjectDto.Id, projectDto.CsvPresets);
            SetDefaultPresetsToTypes(projectDto.CsvDefaultPreset);
            SetSuperTypes(projectDto.CsvTypeType);
            return persistedProjectDto;
        }

        private void PersistEntities<T, U>(Guid projectId, CSVFile file, string dtype = "")
            where T : BaseEntity
            where U : BaseDto
        {
            if(file == null)
            {
                return;
            }

            List<T> entities = new List<T>();
            CSVRow header = file.GetHeader();
            foreach(CSVRow row in file.GetValues())
            {
                CSVValue dtypeValue = file.GetValueToColumn(row, "DTYPE");
                if(dtypeValue != null)
                {
                    if(!dtype.Equals(dtypeValue.GetValue()))
                    {
                        continue;
                    }
                }
                entities.Add(CreateEntity<T, U>(file, row, projectId));
            }
            _genericRepository.PersistAsNews<T>(entities);
        }

        private T CreateEntity<T, U>(CSVFile file, CSVRow row, Guid projectId)
            where T : BaseEntity
            where U : BaseDto
        {
            T entity = Activator.CreateInstance<T>();
            if(typeof(IProjectAwareEntity).IsAssignableFrom(typeof(T)))
            {
                ((IProjectAwareEntity)entity).ProjectId = projectId;
            }
            if (typeof(IStateAwareEntity).IsAssignableFrom(typeof(T)))
            {
                ((IStateAwareEntity)entity).State = State.BUILTIN;
            }

            List<PropertyInfo> csvAttributePropertyInfos = typeof(U).GetProperties().Where(x => Attribute.IsDefined(x, typeof(CSVAttributeAttribute))).ToList();
            foreach (PropertyInfo csvAttributePropertyInfo in csvAttributePropertyInfos)
            {
                CSVAttributeAttribute csvAttribute = csvAttributePropertyInfo.GetCustomAttribute<CSVAttributeAttribute>();
                CSVValue currentValue = file.GetValueToColumn(row, csvAttribute.Name);
                object convertedValue = Converter.ConvertValue(csvAttributePropertyInfo.PropertyType, currentValue.GetValue());

                PropertyInfo entityPropertyInfo = entity.GetType().GetProperty(csvAttributePropertyInfo.Name);
                if(entityPropertyInfo != null)
                {
                    entityPropertyInfo.SetValue(entity, convertedValue);
                }
            }
            return entity;
        }

        private void SetDefaultPresetsToTypes(CSVFile file)
        {
            foreach (CSVRow row in file.GetValues())
            {
                CSVValue typeIdValue = file.GetValueToColumn(row, "ID");
                CSVValue defaultPresetIdValue = file.GetValueToColumn(row, "DEFAULT_PRESET_ID");
                Guid typeId = Converter.ConvertValue<Guid>(typeIdValue.GetValue());
                Guid defaultPresetId = Converter.ConvertValue<Guid>(defaultPresetIdValue.GetValue());

                CompositeType compositeType = _genericRepository.Find<CompositeType>(typeId);
                compositeType.DefaultPresetId = defaultPresetId;
                _genericRepository.Persist<CompositeType>(compositeType);
            }
        }

        private void SetSuperTypes(CSVFile file)
        {
            foreach (CSVRow row in file.GetValues())
            {
                CSVValue subTypeIdValue = file.GetValueToColumn(row, "SUB_TYPE_ID");
                CSVValue superTypeIdValue = file.GetValueToColumn(row, "SUPER_TYPE_ID");
                Guid subTypeId = Converter.ConvertValue<Guid>(subTypeIdValue.GetValue());
                Guid superTypeId = Converter.ConvertValue<Guid>(superTypeIdValue.GetValue());

                CompositeType subCompositeType = _genericRepository.Find<CompositeType>(subTypeId);
                CompositeType superCompositeType = _genericRepository.Find<CompositeType>(superTypeId);
                if(subCompositeType.SuperTypes == null)
                {
                    subCompositeType.SuperTypes = new List<CompositeType>();
                }
                subCompositeType.SuperTypes.Add(superCompositeType);
                _genericRepository.Persist<CompositeType>(subCompositeType);
            }
        }
    }
}
