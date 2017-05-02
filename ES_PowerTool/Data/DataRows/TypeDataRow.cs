using ES_PowerTool.Data.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.DataRows
{
    public class TypeDataRow : BaseDataRow
    {
        private TypeDataTable _tableType;

        public string Description
        {
            get { return (string)this[_tableType.DescriptionColumn]; }
            set { this[_tableType.DescriptionColumn] = value; }
        }

        public Guid FolderId
        {
            get { return (Guid)this[_tableType.FolderIdColumn]; }
            set { this[_tableType.FolderIdColumn] = value; }
        }

        public TypeDataRow(DataRowBuilder builder) 
            : base(builder)
        {
            _tableType = (TypeDataTable)Table;
        }
    }
}
