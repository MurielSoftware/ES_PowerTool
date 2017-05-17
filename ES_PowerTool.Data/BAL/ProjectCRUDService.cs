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
            PersistEntities<Folder>(persistedProjectDto.Id, projectDto.CsvFolders);
            PersistEntities<CompositeType>(persistedProjectDto.Id, projectDto.CsvTypes, "COM");
            PersistEntities<PrimitiveType>(persistedProjectDto.Id, projectDto.CsvTypes, "PRI");
            PersistEntities<CompositeTypeElement>(persistedProjectDto.Id, projectDto.CsvTypeElements);
            return persistedProjectDto;
        }

        private void PersistEntities<T>(Guid projectId, CSVFile file, string dtype = "")
            where T : BaseEntity
        {
            int rowIndex = 0;
            List<T> entities = new List<T>();
            foreach(CSVRow csvRow in file.GetRows())
            {
                Dictionary<string, string> rowToHeader = file.GetRowToHeader(rowIndex++);
                if(rowToHeader.ContainsKey("DTYPE"))
                {
                    if(!dtype.Equals(rowToHeader["DTYPE"]))
                    {
                        continue;
                    }
                }
                entities.Add(CreateEntity<T>(projectId, rowToHeader));
            }
            _genericRepository.PersistAsNews<T>(entities);
        }

        private T CreateEntity<T>(Guid projectId, Dictionary<string, string> rowToHeader) 
            where T : BaseEntity 
        {
            T entity = Activator.CreateInstance<T>();
            if(typeof(IProjectAwareEntity).IsAssignableFrom(typeof(T)))
            {
                ((IProjectAwareEntity)entity).ProjectId = projectId;
            }
            if(typeof(IStateAwareEntity).IsAssignableFrom(typeof(T)))
            {
                ((IStateAwareEntity)entity).State = State.BUILTIN;
            }
            List<PropertyInfo> csvAttributePropertyInfos = typeof(T).GetProperties().Where(x => Attribute.IsDefined(x, typeof(CSVAttributeAttribute))).ToList();

            foreach(PropertyInfo csvAttributePropertyInfo in csvAttributePropertyInfos)
            {
                CSVAttributeAttribute csvAttribute = csvAttributePropertyInfo.GetCustomAttribute<CSVAttributeAttribute>();
                string value = rowToHeader[csvAttribute.Name];
                object convertedValue = Converter.ConvertValue(csvAttributePropertyInfo.PropertyType, value);

                PropertyInfo entityPropertyInfo = entity.GetType().GetProperty(csvAttributePropertyInfo.Name);
                if (entityPropertyInfo != null)
                {
                    entityPropertyInfo.SetValue(entity, convertedValue);
                }
            }
            return entity;
        }
    }
}
