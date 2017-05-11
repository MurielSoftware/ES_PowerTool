using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.CSV
{
    public class CSVHeader
    {
        public string Name { get; set; }
        public int Index { get; set; }

        public CSVHeader(string name, int index)
        {
            Name = name;
            Index = index;
        }
    }
}
