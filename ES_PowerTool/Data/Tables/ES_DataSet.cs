using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ES_PowerTool.Data.Tables
{
    [Serializable]
    [XmlRoot("ES_PT")]
    public class ES_DataSet : DataSet
    {
        private DataRelation _relationFolderType;
        private DataRelation _relationFolderFolder;
        private DataRelation _relationTypeElementOwningType;
        private DataRelation _relationTypeElementElementType;

        public FolderDataTable FolderDataTable { get; private set; }
        public TypeDataTable TypeDataTable { get; private set; }
        public TypeElementDataTable TypeElementDataTable { get; private set; }

        public ES_DataSet()
        {
            BeginInit();
            InitClass();
            EndInit();
        }

        private void InitClass()
        {
            FolderDataTable = new FolderDataTable();
            TypeDataTable = new TypeDataTable();
            TypeElementDataTable = new TypeElementDataTable();

            Tables.Add(FolderDataTable);
            Tables.Add(TypeDataTable);
            Tables.Add(TypeElementDataTable);

            _relationFolderType = new DataRelation("Folder_Type", 
                new DataColumn[] { FolderDataTable.IdColumn }, 
                new DataColumn[] { TypeDataTable.FolderIdColumn }, 
                false);
            _relationFolderFolder = new DataRelation("Folder_Folder",
                new DataColumn[] { FolderDataTable.IdColumn },
                new DataColumn[] { FolderDataTable.FolderIdColumn },
                false);
            _relationTypeElementOwningType = new DataRelation("TypeElement_OwningType",
                new DataColumn[] { TypeDataTable.IdColumn },
                new DataColumn[] { TypeElementDataTable.OwningTypeIdColumn },
                false);
            _relationTypeElementElementType = new DataRelation("TypeElement_ElementType",
                new DataColumn[] { TypeDataTable.IdColumn },
                new DataColumn[] { TypeElementDataTable.ElementTypeIdColumn },
                false);

            Relations.Add(_relationFolderType);
            Relations.Add(_relationFolderFolder);
            Relations.Add(_relationTypeElementOwningType);
            Relations.Add(_relationTypeElementElementType);
        }
    }
}
