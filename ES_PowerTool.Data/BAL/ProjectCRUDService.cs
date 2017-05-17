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
            PersistEntities<CompositeType, CompositeTypeDto>(persistedProjectDto.Id, projectDto.CsvTypes);
            PersistEntities<CompositeTypeElement, CompositeTypeElementDto>(persistedProjectDto.Id, projectDto.CsvTypeElements);
            return persistedProjectDto;
        }

        private void PersistEntities<T, U>(Guid projectId, CSVFile file)
            where T : BaseEntity
            where U : BaseDto
        {
            int rowIndex = 0;
            List<T> entities = new List<T>();
            foreach(CSVRow csvRow in file.GetRows())
            {
                Dictionary<string, string> rowToHeader = file.GetRowToHeader(rowIndex++);
                entities.Add(CreateEntity<T, U>(projectId, rowToHeader));
            }
            _genericRepository.PersistAsNews<T>(entities);
        }

        private T CreateEntity<T, U>(Guid projectId, Dictionary<string, string> rowToHeader) 
            where T : BaseEntity 
            where U : BaseDto
        {
            T entity = Activator.CreateInstance<T>();
            if(typeof(IProjectAwareEntity).IsAssignableFrom(typeof(T)))
            {
                ((IProjectAwareEntity)entity).ProjectId = projectId;
            }
            List<PropertyInfo> csvAttributePropertyInfos = typeof(U).GetProperties().Where(x => Attribute.IsDefined(x, typeof(CSVAttribute))).ToList();

            foreach(PropertyInfo csvAttributePropertyInfo in csvAttributePropertyInfos)
            {
                CSVAttribute csvAttribute = csvAttributePropertyInfo.GetCustomAttribute<CSVAttribute>();
                string value = rowToHeader[csvAttribute.Name];
                object convertedValue = Converter.ConvertValue(csvAttributePropertyInfo.PropertyType, value);

                PropertyInfo entityPropertyInfo = entity.GetType().GetProperty(csvAttributePropertyInfo.Name);
                entityPropertyInfo.SetValue(entity, convertedValue);
            }
            return entity;
        }
    }
}
