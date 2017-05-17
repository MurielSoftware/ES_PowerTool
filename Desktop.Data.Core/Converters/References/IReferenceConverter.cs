using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Attributes;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.DataTypes;
using Desktop.Shared.Core.Dtos;
using System.Reflection;

namespace Desktop.Data.Core.Converters.References
{
    public interface IReferenceConverter
    {
        /// <summary>
        /// Converts the reference.
        /// </summary>
        /// <param name="unitOfWork">The current unit of work</param>
        /// <param name="sourceEntity">The source entity</param>
        /// <param name="dto">The appropriate DTO</param>
        /// <param name="sourcePropertyInfo">The current property</param>
        /// <param name="referenceAttribute">The referenced attribute of the property</param>
        /// <param name="referenceString">The value of the referenced property</param>
        void Convert(Connection connection, BaseEntity sourceEntity, BaseDto dto, PropertyInfo sourcePropertyInfo, ReferenceAttribute referenceAttribute, ReferenceString referenceString);
    }
}
