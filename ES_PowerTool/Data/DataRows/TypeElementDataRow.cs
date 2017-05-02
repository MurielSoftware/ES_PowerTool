using ES_PowerTool.Data.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.DataRows
{
    public class TypeElementDataRow : BaseDataRow
    {
        private TypeElementDataTable _tableTypeElement;

        public string Description
        {
            get { return (string)this[_tableTypeElement.DescriptionColumn]; }
            set { this[_tableTypeElement.DescriptionColumn] = value; }
        }

        public Guid OwningTypeId
        {
            get { return (Guid)this[_tableTypeElement.OwningTypeIdColumn]; }
            set { this[_tableTypeElement.OwningTypeIdColumn] = value; }
        }

        public Guid ElementTypeId
        {
            get { return (Guid)this[_tableTypeElement.ElementTypeIdColumn]; }
            set { this[_tableTypeElement.ElementTypeIdColumn] = value; }
        }

        public bool Optional
        {
            get { return (bool)this[_tableTypeElement.OptionalColumn]; }
            set { this[_tableTypeElement.OptionalColumn] = value; }
        }

        public TypeElementDataRow(DataRowBuilder builder) 
            : base(builder)
        {
            _tableTypeElement = (TypeElementDataTable)Table;
        }
    }
}
