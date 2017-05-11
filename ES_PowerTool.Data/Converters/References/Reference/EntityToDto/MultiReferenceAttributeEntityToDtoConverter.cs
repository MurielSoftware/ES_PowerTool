using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ES_PowerTool.Data.Model;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Data.Converters.References.Reference.EntityToDto
{
    /// <summary>
    /// Converts the multi reference from Entity to DTO.
    /// </summary>
    /// <typeparam name="T">The type of the source entity</typeparam>
    /// <typeparam name="U">The type of the referenced entity</typeparam>
    public class MultiReferenceAttributeEntityToDtoConverter<T, U> : IReferenceConverter
        where T : BaseEntity
        where U : BaseEntity
    {
        public void Convert(IUnitOfWork unitOfWork, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString)
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
    }
}
