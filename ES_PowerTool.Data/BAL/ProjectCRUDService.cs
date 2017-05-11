using ES_PowerTool.Data.Model;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Shared.CSV;
using Desktop.Shared.Core.Attributes;
using System.Reflection;
using Desktop.Shared.Utils;
using Desktop.Shared.Core.Dtos;

namespace ES_PowerTool.Data.BAL
{
    public class ProjectCRUDService : GenericCRUDService<ProjectDto, Project>, IProjectCRUDService
    {
        public ProjectCRUDService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public override ProjectDto Persist(ProjectDto projectDto)
        {
            PersistEntities<Folder, FolderDto>(projectDto.CsvFolders);
            PersistEntities<CompositeType, CompositeTypeDto>(projectDto.CsvTypes);
            PersistEntities<CompositeTypeElement, CompositeTypeElementDto>(projectDto.CsvTypeElements);
            return projectDto;
        }

        private void PersistEntities<T, U>(CSVFile file)
            where T : BaseEntity
            where U : BaseDto
        {
            int rowIndex = 0;
            List<T> entities = new List<T>();
            foreach(CSVRow csvRow in file.GetRows())
            {
                Dictionary<string, string> rowToHeader = file.GetRowToHeader(rowIndex++);
                entities.Add(CreateEntity<T, U>(rowToHeader));
            }
            _genericRepository.PersistAsNews<T>(entities);
        }

        private T CreateEntity<T, U>(Dictionary<string, string> rowToHeader) 
            where T : BaseEntity 
            where U : BaseDto
        {
            T entity = Activator.CreateInstance<T>();
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
