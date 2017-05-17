using Desktop.Data.Core.Converters.References.List;
using Desktop.Data.Core.Converters.References.List.EntityToDto;
using Desktop.Data.Core.Converters.References.Reference.EntityToDto;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Converters
{
    /// <summary>
    /// Base Entity To DTO convert provider.
    /// </summary>
    /// <typeparam name="T">The type of the Entity</typeparam>
    /// <typeparam name="U">The type of the DTO</typeparam>
    public class EntityToDtoConvertProvider<T, U> : BaseConvertProvider<T, U>
        where T : BaseEntity
        where U : BaseDto
    {
        protected override void RegisterConverters()
        {
            base.RegisterConverters();
            _converters.Add(new ReferenceAttributeEntityToDtoConverter<T, U>());
            _converters.Add(new ListReferenceAttributeEntityToDtoConverter<T, U>());
        }

        protected override U CreateInstance(Connection connection, T source)
        {
            return base.CreateInstance(connection, source);
        }
    }
}
