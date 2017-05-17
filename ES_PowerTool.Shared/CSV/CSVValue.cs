using Desktop.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.CSV
{
    public class CSVValue
    {
        private string _value;

        public CSVValue(string value)
        {
            _value = RemoveQuotesIfNeccessary(value);
        }

        public string GetValue()
        {
            return _value;
        }

        public object GetConvertedValue(Type type)
        {
            return Converter.ConvertValue(type, _value);
        }

        private static string RemoveQuotesIfNeccessary(string value)
        {
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                string valueWithoutQuotes = value.Substring(1, value.Length - 2);
                return valueWithoutQuotes;
            }
            return value;
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null)
        //    {
        //        return false;
        //    }
        //    if (!(obj is CSVValue))
        //    {
        //        return false;
        //    }

        //    CSVValue rsd = obj as CSVValue;
        //    return _value.Equals(rsd.GetValue());
        //}

        //public override int GetHashCode()
        //{
        //    return _value.GetHashCode();
        //}
    }
}
