using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Dtos;
using ES_PowerTool.Data.Converters.References.Utils;
using ES_PowerTool.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ES_PowerTool.Data.Converters.References.Reference.EntityToDto
{
    /// <summary>
    /// Base Entity to DTO reference converter.
    /// </summary>
    /// <typeparam name="T">The type of the source entity</typeparam>
    /// <typeparam name="U">The type of the target DTO</typeparam>
    public class ReferenceAttributeEntityToDtoConverter<T, U> : BaseReferenceAttributeConverter<T, U>
        where T : BaseEntity
        where U : BaseDto
    {
        public override ICollection<PropertyInfo> GetPropertiesToConvert(T source, U target)
        {
            return target.GetType().GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(ReferenceAttribute)) && !Attribute.IsDefined(x, typeof(ListReferenceAttribute)))
                .ToList();
        }

        public override void Convert(IUnitOfWork unitOfWork, T entity, U dto, PropertyInfo sourcePropertyInfo)
        {
            ReferenceAttribute referenceAttribute = sourcePropertyInfo.GetCustomAttribute<ReferenceAttribute>();
            ReferenceString referenceString = sourcePropertyInfo.GetValue(dto) as ReferenceString;
            Type referencedEntityType = GetReferencedEntityType(entity, referenceAttribute);
            IReferenceConverter referenceConverter = GetReferenceConverter(referenceAttribute, typeof(T), referencedEntityType);
            referenceConverter.Convert(unitOfWork, entity, dto, sourcePropertyInfo, referenceAttribute, referenceString);
        }

        private IReferenceConverter GetReferenceConverter(ReferenceAttribute referenceAttribute, Type sourceEntityType, Type referencedEntityType)
        {
            if (ReferenceConversionUtils.IsCollectionPropertyType(sourceEntityType, referenceAttribute))
            {
                return CreateReferenceConverter(typeof(MultiReferenceAttributeEntityToDtoConverter<,>), sourceEntityType, referencedEntityType);
            }
            return CreateReferenceConverter(typeof(SingleReferenceAttributeEntityToDtoConverter<,>), sourceEntityType, referencedEntityType);
        }
    }
}
