using ES_PowerTool.Data.DataRows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ES_PowerTool.Data.Tables;

namespace ES_PowerTool.Data.Repositories
{
    public class Repository
    {
        private ES_DataSet _dataSet = new ES_DataSet();

        public void Insert<T>(BaseDataRow dataRow) where T : BaseDataRow
        {
            if (Guid.Empty.Equals(dataRow.Id))
            {
                dataRow.Id = Guid.NewGuid();
                _dataSet.Tables[dataRow.Table.TableName].Rows.Add(dataRow);
            }
            else
            {

            }
            _dataSet.AcceptChanges();
        }

        public void Delete<T>(Guid id) where T : BaseDataRow
        {
            BaseDataRow dataRow = Find<T>(id);
            dataRow.Delete();
            _dataSet.AcceptChanges();
        }

        public T Find<T>(Guid id) where T : BaseDataRow
        {
            return (T) _dataSet.Tables[typeof(T).Name].Rows.Find(id);
        }

        public List<T> FindAll<T>() where T : BaseDataRow
        {
            return _dataSet.Tables[typeof(T).Name].Rows.Cast<T>().ToList();
        }
    }
}
