using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Validations;
using Desktop.Shared.Utils;
using Desktop.Ui.I18n;
using ES_PowerTool.Shared.CSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.BAL.Projects.Import
{
    public abstract class ProjectTransferService
    {
        protected void PersistEntities<T, U>(Connection connection, Guid projectId, CSVFile file, string dtype = "")
            where T : BaseEntity
            where U : BaseDto
        {
            if (file == null)
            {
                return;
            }

            List<T> entities = new List<T>();
            CSVRow header = file.GetHeader();
            foreach (CSVRow row in file.GetValues())
            {
                CSVValue dtypeValue = file.GetValueToColumn(row, "DTYPE");
                if (dtypeValue != null && !string.IsNullOrEmpty(dtype))
                {
                    if (!dtype.Equals(dtypeValue.GetValue()))
                    {
                        continue;
                    }
                }
                entities.Add(CreateEntity<T, U>(file, row, projectId));
            }
            GenericRepository genericRepository = new GenericRepository(connection);
            genericRepository.PersistAsNews<T>(entities);
        }

        private T CreateEntity<T, U>(CSVFile file, CSVRow row, Guid projectId)
            where T : BaseEntity
            where U : BaseDto
        {
            T entity = Activator.CreateInstance<T>();
            if (typeof(IProjectAwareEntity).IsAssignableFrom(typeof(T)))
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
                if (currentValue == null)
                {
                    ValidationResult validationResult = new ValidationResult();
                    validationResult.Add(new ValidationMessage(ValidationType.ERROR, MessageKeyConstants.ERROR_MESSAGE_WRONG_CSV_FILE_ON_INPUT));
                    throw new ValidationException(validationResult);
                }
                object convertedValue = Converter.ConvertValue(csvAttributePropertyInfo.PropertyType, currentValue.GetValue());

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
