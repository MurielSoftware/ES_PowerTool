﻿using ES_PowerTool.Shared.Dtos;
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
using ES_PowerTool.Shared.Dtos.OOE;
using ES_PowerTool.Shared.Dtos.OOE.Types;
using ES_PowerTool.Shared.Dtos.OOE.Elements;
using ES_PowerTool.Shared.Dtos.OOE.Presets;
using Desktop.Shared.Core.Validations;
using Desktop.Ui.I18n;
using ES_PowerTool.Data.BAL.Setting;

namespace ES_PowerTool.Data.BAL.Projects
{
    public class ProjectCRUDService : GenericCRUDService<ProjectDto, Project>, IProjectCRUDService
    {
        private ProjectValidationService _projectValidationService;
        //private SettingsCRUDService _settingsCRUDService;

        public ProjectCRUDService(Connection connection) 
            : base(connection)
        {
            _projectValidationService = new ProjectValidationService(connection);
            //_settingsCRUDService = new SettingsCRUDService(connection);
        }

        //public override ProjectDto Persist(ProjectDto projectDto)
        //{
        //    ProjectDto persistedProjectDto = base.Persist(projectDto);
        //    PersistEntities<Folder, FolderDto>(persistedProjectDto.Id, projectDto.CsvFolders);
        //    PersistEntities<CompositeType, CompositeTypeDto>(persistedProjectDto.Id, projectDto.CsvTypes, "COM");
        //    PersistEntities<PrimitiveType, PrimitiveTypeDto>(persistedProjectDto.Id, projectDto.CsvTypes, "PRI");
        //    PersistEntities<CompositeTypeElement, CompositeTypeElementDto>(persistedProjectDto.Id, projectDto.CsvTypeElements);
        //    PersistEntities<Preset, PresetDto>(persistedProjectDto.Id, projectDto.CsvPresets);
        //    PersistEntities<CompositePresetElement, CompositePresetElementDto>(persistedProjectDto.Id, projectDto.CsvPresetElements);
        //    SetDefaultPresetsToTypes(projectDto.CsvDefaultPreset);
        //    SetSuperTypes(projectDto.CsvTypeType);
        //    _settingsCRUDService.CreateAndPresistDefaultSettings();
        //    return persistedProjectDto;
        //}

        protected override void ValidationBeforePersist(ProjectDto projectDto)
        {
            base.ValidationBeforePersist(projectDto);
            ValidationResult validationResult = _projectValidationService.CollectValidationResultBeforePersist(projectDto);
            if (!validationResult.IsEmpty())
            {
                throw new ValidationException(validationResult);
            }
        }

        //private void PersistEntities<T, U>(Guid projectId, CSVFile file, string dtype = "")
        //    where T : BaseEntity
        //    where U : BaseDto
        //{
        //    if(file == null)
        //    {
        //        return;
        //    }

        //    List<T> entities = new List<T>();
        //    CSVRow header = file.GetHeader();
        //    foreach(CSVRow row in file.GetValues())
        //    {
        //        CSVValue dtypeValue = file.GetValueToColumn(row, "DTYPE");
        //        if(dtypeValue != null && !string.IsNullOrEmpty(dtype))
        //        {
        //            if(!dtype.Equals(dtypeValue.GetValue()))
        //            {
        //                continue;
        //            }
        //        }
        //        entities.Add(CreateEntity<T, U>(file, row, projectId));
        //    }
        //    _genericRepository.PersistAsNews<T>(entities);
        //}

        //private T CreateEntity<T, U>(CSVFile file, CSVRow row, Guid projectId)
        //    where T : BaseEntity
        //    where U : BaseDto
        //{
        //    T entity = Activator.CreateInstance<T>();
        //    if(typeof(IProjectAwareEntity).IsAssignableFrom(typeof(T)))
        //    {
        //        ((IProjectAwareEntity)entity).ProjectId = projectId;
        //    }
        //    if (typeof(IStateAwareEntity).IsAssignableFrom(typeof(T)))
        //    {
        //        ((IStateAwareEntity)entity).State = State.BUILTIN;
        //    }

        //    List<PropertyInfo> csvAttributePropertyInfos = typeof(U).GetProperties().Where(x => Attribute.IsDefined(x, typeof(CSVAttributeAttribute))).ToList();
        //    foreach (PropertyInfo csvAttributePropertyInfo in csvAttributePropertyInfos)
        //    {
        //        CSVAttributeAttribute csvAttribute = csvAttributePropertyInfo.GetCustomAttribute<CSVAttributeAttribute>();
        //        CSVValue currentValue = file.GetValueToColumn(row, csvAttribute.Name);
        //        if(currentValue == null)
        //        {
        //            ValidationResult validationResult = new ValidationResult();
        //            validationResult.Add(new ValidationMessage(ValidationType.ERROR, MessageKeyConstants.ERROR_MESSAGE_WRONG_CSV_FILE_ON_INPUT));
        //            throw new ValidationException(validationResult);
        //        }
        //        object convertedValue = Converter.ConvertValue(csvAttributePropertyInfo.PropertyType, currentValue.GetValue());

        //        PropertyInfo entityPropertyInfo = entity.GetType().GetProperty(csvAttributePropertyInfo.Name);
        //        if(entityPropertyInfo != null)
        //        {
        //            entityPropertyInfo.SetValue(entity, convertedValue);
        //        }
        //    }
        //    return entity;
        //}

        //private void SetDefaultPresetsToTypes(CSVFile file)
        //{
        //    foreach (CSVRow row in file.GetValues())
        //    {
        //        CSVValue typeIdValue = file.GetValueToColumn(row, "ID");
        //        CSVValue defaultPresetIdValue = file.GetValueToColumn(row, "DEFAULT_PRESET_ID");
        //        Guid typeId = Converter.ConvertValue<Guid>(typeIdValue.GetValue());
        //        Guid defaultPresetId = Converter.ConvertValue<Guid>(defaultPresetIdValue.GetValue());

        //        CompositeType compositeType = _genericRepository.Find<CompositeType>(typeId);
        //        compositeType.DefaultPresetId = defaultPresetId;
        //        _genericRepository.Persist<CompositeType>(compositeType);
        //    }
        //}

        //private void SetSuperTypes(CSVFile file)
        //{
        //    foreach (CSVRow row in file.GetValues())
        //    {
        //        CSVValue subTypeIdValue = file.GetValueToColumn(row, "SUB_TYPE_ID");
        //        CSVValue superTypeIdValue = file.GetValueToColumn(row, "SUPER_TYPE_ID");
        //        Guid subTypeId = Converter.ConvertValue<Guid>(subTypeIdValue.GetValue());
        //        Guid superTypeId = Converter.ConvertValue<Guid>(superTypeIdValue.GetValue());

        //        CompositeType subCompositeType = _genericRepository.Find<CompositeType>(subTypeId);
        //        CompositeType superCompositeType = _genericRepository.Find<CompositeType>(superTypeId);
        //        if(subCompositeType.SuperTypes == null)
        //        {
        //            subCompositeType.SuperTypes = new List<CompositeType>();
        //        }
        //        subCompositeType.SuperTypes.Add(superCompositeType);
        //        _genericRepository.Persist<CompositeType>(subCompositeType);
        //    }
        //}
    }
}
