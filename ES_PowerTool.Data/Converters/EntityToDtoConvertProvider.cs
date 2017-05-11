using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Dtos;
using ES_PowerTool.Data.Converters.References.List.EntityToDto;
using ES_PowerTool.Data.Converters.References.Reference.EntityToDto;
using ES_PowerTool.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.Converters
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
            _converters.Add(new ListReferenceAttributeEntityToDto<T, U>());
        }

        protected override U CreateInstance(IUnitOfWork unitOfWork, T source)
        {
            return base.CreateInstance(unitOfWork, source);
        }
    }
}
