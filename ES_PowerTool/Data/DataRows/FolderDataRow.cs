using ES_PowerTool.Data.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.DataRows
{
    public class FolderDataRow : BaseDataRow
    {
        private FolderDataTable _tableFolder;

        public string Name
        {
            get { return (string)this[_tableFolder.NameColumn]; }
            set { this[_tableFolder.NameColumn] = value; }
        }

        public Guid FolderId
        {
            get { return (Guid)this[_tableFolder.FolderIdColumn]; }
            set { this[_tableFolder.FolderIdColumn] = value; }
        }

        public FolderDataRow(DataRowBuilder builder) 
            : base(builder)
        {
            _tableFolder = (FolderDataTable)Table;
        }
    }
}
