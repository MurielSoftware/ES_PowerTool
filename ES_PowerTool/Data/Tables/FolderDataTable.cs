using ES_PowerTool.Data.DataRows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.Tables
{
    [Serializable]
    public class FolderDataTable : BaseDataTable
    {
        public DataColumn NameColumn { get; private set; }
        public DataColumn FolderIdColumn { get; private set; }

        public FolderDataTable()
        {
            TableName = "Folder";
            BeginInit();
            InitClass();
            EndInit();
        }

        public FolderDataRow NewFolderDataRow()
        {
            return (FolderDataRow)NewRow();
        }

        protected override Type GetRowType()
        {
            return typeof(FolderDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new FolderDataRow(builder);
        }

        protected override void InitVars()
        {
            base.InitVars();
            NameColumn = Columns["Name"];
            FolderIdColumn = Columns["FolderId"];
        }

        private void InitClass()
        {
            NameColumn = new DataColumn("Name", typeof(string), null, MappingType.Element);
            Columns.Add(NameColumn);
            FolderIdColumn = new DataColumn("FolderId", typeof(Guid), null, MappingType.Element);
            Columns.Add(FolderIdColumn);
        }
    }
}
