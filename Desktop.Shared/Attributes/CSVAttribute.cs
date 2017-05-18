using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Attributes
{
    public class CSVAttributeAttribute : Attribute
    {
        public string Name { get; private set; }
        public int ColumnOrder { get; private set; }

        public CSVAttributeAttribute(string name, int columnOrder)
        {
            Name = name;
            ColumnOrder = columnOrder;
        }
    }
}
