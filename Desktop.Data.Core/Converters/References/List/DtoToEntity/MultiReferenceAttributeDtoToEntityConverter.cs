using Desktop.Data.Core.Context;
using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Converters.References.List.DtoToEntity
{
    public class MultiReferenceAttributeDtoToEntityConverter<T, U> : IReferenceConverter
        where T : BaseEntity
        where U : BaseEntity
    {
        public void Convert(Connection connection, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
        {
            PropertyInfo targetProperty = sourceEntity.GetType().GetProperty(referenceAttribute.RefencedPropertyName);
            List<Guid> referencedIds = referenceString.GetIds();
            ICollection<U> referencedEntities = (ICollection<U>)targetProperty.GetValue(sourceEntity);

            if (IsReferenciesCreated(referencedIds, referencedEntities))
            {
                CreateMultiReferences(connection, sourceEntity, targetProperty, referencedIds, referencedEntities);
            }
            else
            {
                UpdateMultiReference(connection, sourceEntity, targetProperty, referencedIds, referencedEntities);
            }
        }

        private bool IsReferenciesCreated(List<Guid> referencedIds, ICollection<U> referencedEntities)
        {
            return referencedIds != null && referencedIds.Count > 0 && referencedEntities == null;
        }

        private void CreateMultiReferences(Connection connection, object entity, PropertyInfo targetProperty, List<Guid> referencedIds, ICollection<U> referencedEntities)
        {
            targetProperty.SetValue(entity, CollectReferencedEntities(connection, targetProperty, referencedIds));//GetReferencedTargetPropertyGenericType(targetProperty));
        }

        private void UpdateMultiReference(Connection connection, BaseEntity entity, PropertyInfo targetProperty, List<Guid> referencedIds, ICollection<U> referencedEntities)
        {
            GenericRepository genericRepository = new GenericRepository(connection);
            IList currentReferencedEntities = CollectReferencedEntities(connection, targetProperty, referencedIds);
            ICollection<U> targetPropertyValue = (ICollection<U>)targetProperty.GetValue(entity);
            targetPropertyValue.Clear();
            genericRepository.Attach(entity.GetType(), entity);
            foreach (U currentReferencedEntity in currentReferencedEntities)
            {
                genericRepository.Attach<U>(currentReferencedEntity);
            }
            targetPropertyValue.Clear();
            foreach (U currentReferencedEntity in currentReferencedEntities)
            {
                targetPropertyValue.Add(currentReferencedEntity);
            }
        }

        private IList CollectReferencedEntities(Connection connection, PropertyInfo targetProperty, List<Guid> referencedIds)
        {
            GenericRepository genericRepository = new GenericRepository(connection);
            IList referencedEntities = new List<U>();
            foreach (Guid referencedId in referencedIds)
            {
                referencedEntities.Add(genericRepository.FindTracking<U>(referencedId));
            }
            return referencedEntities;
        }

        private Type GetReferencedTargetPropertyGenericType(PropertyInfo targetProperty)
        {
            return targetProperty.PropertyType.GetGenericArguments().FirstOrDefault();
        }
    }
}
