using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ES_PowerTool.Data;
using ES_PowerTool.Data.Tables;
using ES_PowerTool.Data.DataRows;

namespace ES_PowerTool.Test.Data
{
    [TestClass]
    public class DataIntegrationTest
    {
        private static Guid FOLDER_ID = Guid.NewGuid();
        private static Guid TYPE_ID = Guid.NewGuid();
        private static Guid TYPE_ELEMENT_ID = Guid.NewGuid();
        private static string FOLDER_NAME = "folder";
        private static string TYPE_DECRIPTION = "type";
        private static string TYPE_ELEMENT_DESCRIPTION = "typeElement";

        [TestMethod]
        public void TestSave()
        {
            ES_DataSet ds = new ES_DataSet();
            FolderDataRow folderRow = ds.FolderDataTable.NewFolderDataRow();
            folderRow.Id = FOLDER_ID;
            folderRow.Name = FOLDER_NAME;
            TypeDataRow typeRow = ds.TypeDataTable.NewTypeDataRow();
            typeRow.Id = TYPE_ID;
            typeRow.Description = TYPE_DECRIPTION;
            typeRow.FolderId = FOLDER_ID;
            TypeElementDataRow typeElementRow = ds.TypeElementDataTable.NewTypeElementDataRow();
            typeElementRow.Id = TYPE_ELEMENT_ID;
            typeElementRow.Description = TYPE_ELEMENT_DESCRIPTION;
            typeElementRow.OwningTypeId = TYPE_ID;
            typeElementRow.ElementTypeId = TYPE_ID;

            ds.FolderDataTable.Rows.Add(folderRow);
            ds.TypeDataTable.Rows.Add(typeRow);
            ds.TypeElementDataTable.Rows.Add(typeElementRow);

            ds.AcceptChanges();
            ds.WriteXml("test.xml");
        }

        [TestMethod]
        public void TestLoad()
        {
            ES_DataSet ds = new ES_DataSet();
            ds.ReadXml("test.xml");
        }
    }
}
