using Desktop.Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.CSV
{
    public class CSVFile
    {
        public const string SEPARATOR = ";";

        private List<CSVRow> _values = new List<CSVRow>();
        CSVRow _header = new CSVRow();

        public void SetHeader(CSVRow header)
        {
            _header = header;
        }

        public CSVRow GetHeader()
        {
            return _header;
        }

        public void AddValue(CSVRow value)
        {
            _values.Add(value);
        }

        public void SetValues(List<CSVRow> values)
        {
            _values = values;
        }

        public List<CSVRow> GetValues()
        {
            return _values;
        }

        public CSVValue GetValueToColumn(CSVRow row, string columnName)
        {
            if (!_header.Contains(columnName))
            {
                return null;
            }
            int index = _header.GetIndexOf(columnName);
            return row.GetValue(index);
        }

        public void AddValueToColumn(CSVRow row, string columnName, string value)
        {
            int index = _header.GetIndexOf(columnName);
            row.Insert(index, new CSVValue(value));
        }
    }
}
