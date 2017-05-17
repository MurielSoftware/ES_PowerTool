using Desktop.Data.Core.Attributes;
using Desktop.Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Converters
{
    /// <summary>
    /// Base converter provider, which is responsible for DTO to entity conversion and vice versa.
    /// </summary>
    /// <typeparam name="T">The source type</typeparam>
    /// <typeparam name="U">The target type</typeparam>
    public abstract class BaseConvertProvider<T, U>
    {
        protected List<IConverter<T, U>> _converters = new List<IConverter<T, U>>();

        public BaseConvertProvider()
        {
            RegisterConverters();
        }

        /// <summary>
        /// Converts the object.
        /// </summary>
        /// <param name="unitOfWork">The current unit of work</param>
        /// <param name="source">The source object</param>
        /// <returns>The target object</returns>
        public virtual U Convert(Connection connection, T source)
        {
            U target = CreateInstance(connection, source);
            if(source == null)
            {
                return default(U);
            }
            foreach(IConverter<T, U> converter in _converters)
            {
                ConvertProperties(connection, converter, source, target);
            }
            return target;
        }

        /// <summary>
        /// Converts the properties.
        /// </summary>
        /// <param name="unitOfWork">The current unit of work</param>
        /// <param name="converter">The current converter</param>
        /// <param name="source">The source object</param>
        /// <param name="target">The target object</param>
        protected virtual void ConvertProperties(Connection connection, IConverter<T, U> converter, T source, U target)
        {
            ICollection<PropertyInfo> propertiesToConvert = converter.GetPropertiesToConvert(source, target);
            foreach(PropertyInfo propertyToConvert in propertiesToConvert)
            {
                converter.Convert(connection, source, target, propertyToConvert);
            }
        }

        /// <summary>
        /// Registers the converters to be used.
        /// </summary>
        protected virtual void RegisterConverters()
        {
            _converters.Add(new PrimitiveAttributeConverter<T, U>());
        }

        /// <summary>
        /// Creates the instance of the target object.
        /// </summary>
        /// <param name="unitOfWork">The current unit of work</param>
        /// <param name="source">The source object</param>
        /// <returns>The instance of the target object</returns>
        protected virtual U CreateInstance(Connection connection, T source)
        {
            return Activator.CreateInstance<U>();
        }
    }
}
