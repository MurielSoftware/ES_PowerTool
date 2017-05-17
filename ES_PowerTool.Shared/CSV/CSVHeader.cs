using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.CSV
{
    public class CSVHeader
    {
       // private Dictionary<CSVValue, CSVValue> _headerToValue = new Dictionary<CSVValue, CSVValue>();
        private List<CSVValue> _header = new List<CSVValue>();
        private int _lastIndex = 0;

        public void AddHeader(CSVValue csvValue)
        {
         //   _headerToValue.Add(csvValue, null);
            _header.Add(csvValue);
        }

        public CSVValue GetHeader(int index)
        {
            return _header[index];
        }

        //public 
    }
}
