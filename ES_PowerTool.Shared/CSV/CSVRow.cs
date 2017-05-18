using Desktop.Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.CSV
{
    public class CSVRow
    {
        private List<CSVValue> _values = new List<CSVValue>();

        public CSVRow()
        {
        }

        public CSVRow(List<string> values)
        {
            foreach(string value in values)
            {
                _values.Add(new CSVValue(value));
            }
        }

        public void AddValue(CSVValue value)
        {
            _values.Add(value);
        }

        public void Insert(int index, CSVValue value)
        {
            _values.Insert(index, value);
        }

        public List<CSVValue> GetValues()
        {
            return _values;
        }

        public CSVValue GetValue(string value)
        {
            return _values.Where(x => x.GetValue() == value).SingleOrDefault();
        }

        public int GetIndexOf(string value)
        {
            return _values.FindIndex(x => x.GetValue() == value);
        }

        public CSVValue GetValue(int index)
        {
            return _values[index];
        }

        public bool Contains(string value)
        {
            return _values.Where(x => x.GetValue() == value).Count() > 0;
        }
    }
}
