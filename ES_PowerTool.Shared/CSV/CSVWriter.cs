using Desktop.Shared.Core.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.CSV
{
    public class CSVWriter
    {
        public static string Write<T>(IEnumerable collection)
        {
            string header = CreateHeader<T>();
            string values = CreateValues<T>(collection);
            return header + values;
        }

        private static string CreateHeader<T>()
        {
            List<PropertyInfo> propertyInfos = GetCSVAttributeProperties<T>();
            StringBuilder sb = new StringBuilder();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                CSVAttributeAttribute csvAttribute = propertyInfo.GetCustomAttribute<CSVAttributeAttribute>();
                sb.Append(csvAttribute.Name);
                sb.Append(CSVFile.SEPARATOR);
            }
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine();
            return sb.ToString();
        }

        private static string CreateValues<T>(IEnumerable collection)
        {
            List<PropertyInfo> propertyInfos = GetCSVAttributeProperties<T>();
            StringBuilder values = new StringBuilder();
            foreach (object t in collection)
            {
                StringBuilder row = new StringBuilder();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    PropertyInfo targetPropertyInfo = t.GetType().GetProperty(propertyInfo.Name);
                    object value = targetPropertyInfo.GetValue(t);
                    if (value == null)
                    {
                        row.Append("null");
                    }
                    else if (isNotQuoteAttribute(propertyInfo.PropertyType))
                    {
                        row.Append(value.ToString().ToLower());
                    }
                    else
                    {
                        row.AppendFormat("\"{0}\"", value);
                    }
                    row.Append(CSVFile.SEPARATOR);
                }
                row.Remove(row.Length - 1, 1);
                row.AppendLine();
                values.Append(row);
            }
            return values.ToString();
        }

        private static List<PropertyInfo> GetCSVAttributeProperties<T>()
        {
            return typeof(T).GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(CSVAttributeAttribute)))
                .OrderBy(x => x.GetCustomAttribute<CSVAttributeAttribute>().ColumnOrder)
                .ToList();
        }

        private static bool isNotQuoteAttribute(Type type)
        {
            return type == typeof(bool);
        }
    }
}
