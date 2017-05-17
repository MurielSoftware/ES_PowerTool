using Desktop.Data.Core.Converters.References.List.DtoToEntity;
using Desktop.Data.Core.Converters.References.Reference.DtoToEntity;
using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Dtos;
using System;

namespace Desktop.Data.Core.Converters
{
    /// <summary>
    /// Base DTO To Entity convert provider.
    /// </summary>
    /// <typeparam name="T">The type of the DTO</typeparam>
    /// <typeparam name="U">The type of the Entity</typeparam>
    public class DtoToEntityConvertProvider<T, U> : BaseConvertProvider<T, U> 
        where T : BaseDto
        where U : BaseEntity
    {
        protected override void RegisterConverters()
        {
            base.RegisterConverters();
            _converters.Add(new ReferenceAttributeDtoToEntityConverter<T, U>());
            _converters.Add(new ListReferenceAttributeDtoToEntityConverter<T, U>());
        }

        protected override U CreateInstance(Connection connection, T source)
        {
            if (Guid.Empty.Equals(source.Id))
            {
                return base.CreateInstance(connection, source);
            }
            return GetUpdatedEntity(connection, source.Id);
        }

        private U GetUpdatedEntity(Connection connection, Guid id)
        {
            return new GenericRepository(connection).Find<U>(id);
        }
    }
}
