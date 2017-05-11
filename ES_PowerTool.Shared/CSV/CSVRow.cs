using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.CSV
{
    public class CSVRow
    {
        private List<string> _values = new List<string>();

        public CSVRow(List<string> values)
        {
            _values = values;
        }

        public void AddValue(string value)
        {
            _values.Add(value);
        }

        public List<string> GetValues()
        {
            return _values;
        }
    }
}
