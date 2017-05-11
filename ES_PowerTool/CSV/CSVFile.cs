using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.CSV
{
    public class CSVFile
    {
        private Dictionary<CSVHeader, string> _valueToColumn = new Dictionary<CSVHeader, string>();
        private Dictionary<int, CSVHeader> _headers = new Dictionary<int, CSVHeader>();
        private List<CSVRow> _rows = new List<CSVRow>();

        public void AddHeader(CSVHeader csvHeader)
        {
            _headers.Add(csvHeader.Index, csvHeader);
            _valueToColumn.Add(csvHeader, null);
        }

        public void AddRow(CSVRow csvRow)
        {
            _rows.Add(csvRow);
        }

        public Dictionary<CSVHeader, string> GetRowToHeader(int index)
        {
            List<string> values = _rows[index].GetValues();
            int valueIndex = 0;
            foreach(string value in values)
            {
                CSVHeader csvHeader = _headers[valueIndex];
                _valueToColumn[csvHeader] = value;
            }
            return _valueToColumn;
        }

        public void Load()
        {

        }
    }
}
