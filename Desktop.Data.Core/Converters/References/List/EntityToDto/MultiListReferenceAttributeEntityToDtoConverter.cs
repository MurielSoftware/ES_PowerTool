using System.Reflection;
using System;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.Model;
using Desktop.Data.Core.Converters.References.Utils;
using System.Collections.Generic;

namespace Desktop.Data.Core.Converters.References.List.EntityToDto
{
    /// <summary>
    /// Converts the list reference from Entity to DTO.
    /// </summary>
    /// <typeparam name="T">The type of the Entity</typeparam>
    /// <typeparam name="U">The type of the DTO</typeparam>
    public class MultiListReferenceAttributeEntityToDtoConverter<T, U> : IReferenceConverter
        where T : BaseEntity
        where U : BaseEntity
    {
        public void Convert(Connection connection, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
        {
            PropertyInfo referencedEntityPropertyInfo = sourceEntity.GetType().GetProperty(referenceAttribute.RefencedPropertyName);
            IEnumerable<U> referencedEntities = (IEnumerable<U>)referencedEntityPropertyInfo.GetValue(sourceEntity, null);

            if (referencedEntities == null)
            {
                return;
            }

            ReferenceString referencedString = new ReferenceString(string.Empty);
            foreach (U referencedEntity in referencedEntities)
            {
                referencedString.Append(referencedEntity.Id, referencedEntity.ToString());
            }
            sourcePropertyInfo.SetValue(dto, referencedString);
        }
        //public override void Convert(Connection connection, T entity, U dto, PropertyInfo sourcePropertyInfo)
        //{
        //    ReferenceAttribute referenceAttribute = sourcePropertyInfo.GetCustomAttribute<ReferenceAttribute>();
        //    ReferenceString referenceString = sourcePropertyInfo.GetValue(dto) as ReferenceString;
        //    Type referencedEntityType = GetReferencedEntityType(entity, referenceAttribute);
        //    IReferenceConverter referenceConverter = GetReferenceConverter(referenceAttribute, typeof(T), referencedEntityType);
        //    referenceConverter.Convert(connection, entity, dto, sourcePropertyInfo, referenceAttribute, referenceString);
        //}

        //private IReferenceConverter GetReferenceConverter(ReferenceAttribute referenceAttribute, Type sourceEntityType, Type referencedEntityType)
        //{
        //    if (ReferenceConversionUtils.IsCollectionPropertyType(sourceEntityType, referenceAttribute))
        //    {
        //        return null;
        //    }
        //    return CreateReferenceConverter(typeof(SingleListReferenceAttributeEntityToDto<,>), sourceEntityType, referencedEntityType);
        //}
    }
}
