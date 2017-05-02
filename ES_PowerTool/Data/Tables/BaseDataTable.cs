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
    public abstract class BaseDataTable : DataTable
    {
        public DataColumn IdColumn { get; private set; }

        //public new List<BaseDataRow> Rows { get { return base.Rows.Cast<BaseDataRow>().ToList(); } }


        public BaseDataTable()
        {
            BeginInit();
            InitClass();
            EndInit();
        }

        protected BaseDataTable(SerializationInfo info, StreamingContext context) : 
                    base(info, context) {
            InitVars();
        }

        protected virtual void InitVars()
        {
            IdColumn = Columns["Id"];
        }

        private void InitClass()
        {
            IdColumn = new DataColumn("Id", typeof(Guid), null, MappingType.Element);
            Columns.Add(IdColumn);

            Constraints.Add(new UniqueConstraint("Folder_PK", new DataColumn[] { IdColumn }, true));
            IdColumn.AllowDBNull = false;
            IdColumn.Unique = true;
        }

        //public T NewRow<T>() where T : BaseDataRow
        //{
        //    return (T)base.NewRow();
        //}

        //public void AddRow(BaseDataRow row)
        //{
        //    Rows.Add(row);
        //}
    }
}
