using ES_PowerTool.Data.DataRows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.Tables
{
    [Serializable]
    public class TypeDataTable : BaseDataTable
    {
        public DataColumn DescriptionColumn { get; private set; }
        public DataColumn FolderIdColumn { get; private set; }

        public TypeDataTable()
        {
            TableName = "Type";
            BeginInit();
            InitClass();
            EndInit();
        }

        public TypeDataRow NewTypeDataRow()
        {
            return (TypeDataRow)NewRow();
        }

        protected override Type GetRowType()
        {
            return typeof(TypeDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new TypeDataRow(builder);
        }

        protected override void InitVars()
        {
            base.InitVars();
            DescriptionColumn = Columns["Description"];
            FolderIdColumn = Columns["FolderId"];
        }

        private void InitClass()
        {
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            Columns.Add(DescriptionColumn);
            FolderIdColumn = new DataColumn("FolderId", typeof(Guid), null, MappingType.Element);
            Columns.Add(FolderIdColumn);
        }
    }
}
