using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
using ES_PowerTool.Data.Model;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Data.DAL;

namespace ES_PowerTool.Data.Converters.References.Reference.DtoToEntity
{
    /// <summary>
    /// Converts the multi references from DTO To Entity
    /// </summary>
    /// <typeparam name="T">The type of the source entity</typeparam>
    /// <typeparam name="U">The type of the referenced entity</typeparam>
    public class MultiReferenceAttributeDtoToEntityConverter<T, U> : IReferenceConverter
        where T : BaseEntity
        where U : BaseEntity
    {
        public void Convert(IUnitOfWork unitOfWork, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
        {
            PropertyInfo targetProperty = sourceEntity.GetType().GetProperty(referenceAttribute.RefencedPropertyName);
            List<Guid> referencedIds = referenceString.GetIds();
            ICollection<U> referencedEntities = (ICollection<U>)targetProperty.GetValue(sourceEntity);

            if (IsReferenciesCreated(referencedIds, referencedEntities))
            {
                CreateMultiReferences(unitOfWork, sourceEntity, targetProperty, referencedIds, referencedEntities);
            }
            else
            {
                UpdateMultiReference(unitOfWork, sourceEntity, targetProperty, referencedIds, referencedEntities);
            }
        }

        private bool IsReferenciesCreated(List<Guid> referencedIds, ICollection<U> referencedEntities)
        {
            return referencedIds != null && referencedIds.Count > 0 && referencedEntities == null;
        }

        private void CreateMultiReferences(IUnitOfWork unitOfWork, object entity, PropertyInfo targetProperty, List<Guid> referencedIds, ICollection<U> referencedEntities)
        {
            targetProperty.SetValue(entity, CollectReferencedEntities(unitOfWork, targetProperty, referencedIds));//GetReferencedTargetPropertyGenericType(targetProperty));
        }

        private void UpdateMultiReference(IUnitOfWork unitOfWork, BaseEntity entity, PropertyInfo targetProperty, List<Guid> referencedIds, ICollection<U> referencedEntities)
        {
            GenericRepository genericRepository = new GenericRepository(unitOfWork);
            IList currentReferencedEntities = CollectReferencedEntities(unitOfWork, targetProperty, referencedIds);
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

        private IList CollectReferencedEntities(IUnitOfWork unitOfWork, PropertyInfo targetProperty, List<Guid> referencedIds)
        {
            GenericRepository genericRepository = new GenericRepository(unitOfWork);
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
