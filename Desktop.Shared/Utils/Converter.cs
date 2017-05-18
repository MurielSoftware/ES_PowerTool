using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Utils
{
    public class Converter
    {
        public static T ConvertValue<T>(string value)
        {
            return (T)ConvertValue(typeof(T), value);
        }

        public static object ConvertValue(Type type, string value)
        {
            if("null".Equals(value.ToLower()))
            {
                return null;
            }
            if(type == typeof(Guid))
            {
                return Guid.Parse(value);
            }
            else if (type == typeof(Guid?))
            {
                return Guid.Parse(value);
            }
            else if(type == typeof(bool?))
            {
                return Boolean.Parse(value);
            }
            return Convert.ChangeType(value, type);
        }
    }
}
