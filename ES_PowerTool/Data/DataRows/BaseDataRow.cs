using ES_PowerTool.Data.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.DataRows
{
    public abstract class BaseDataRow : DataRow
    {
        public Guid Id {
            get { return (Guid)this[((BaseDataTable)Table).IdColumn]; }
            set { this[((BaseDataTable)Table).IdColumn] = value; }
        }

        public BaseDataRow(DataRowBuilder builder) 
            : base(builder)
        {
        }
    }
}
