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
    public class TypeElementDataTable : BaseDataTable
    {
        public DataColumn DescriptionColumn { get; private set; }
        public DataColumn OwningTypeIdColumn { get; private set; }
        public DataColumn ElementTypeIdColumn { get; private set; }
        public DataColumn OptionalColumn { get; private set; }

        public TypeElementDataTable()
        {
            TableName = "TypeElement";
            BeginInit();
            InitClass();
            EndInit();
        }

        public TypeElementDataRow NewTypeElementDataRow()
        {
            return (TypeElementDataRow)NewRow();
        }

        protected override Type GetRowType()
        {
            return typeof(TypeElementDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new TypeElementDataRow(builder);
        }

        protected override void InitVars()
        {
            base.InitVars();
            DescriptionColumn = Columns["Description"];
            OwningTypeIdColumn = Columns["OwningTypeId"];
            ElementTypeIdColumn = Columns["ElementTypeId"];
            OptionalColumn = Columns["OptionalId"];
        }

        private void InitClass()
        {
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            Columns.Add(DescriptionColumn);
            OwningTypeIdColumn = new DataColumn("OwningTypeId", typeof(Guid), null, MappingType.Element);
            Columns.Add(OwningTypeIdColumn);
            ElementTypeIdColumn = new DataColumn("ElementTypeId", typeof(Guid), null, MappingType.Element);
            Columns.Add(ElementTypeIdColumn);
            OptionalColumn = new DataColumn("Optional", typeof(bool), null, MappingType.Element);
            Columns.Add(OptionalColumn);
        }
    }
}
