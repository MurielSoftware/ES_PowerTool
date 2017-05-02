using ES_PowerTool.Data.DataRows;
using ES_PowerTool.Data.Repositories;
using ES_PowerTool.Data.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.Services
{
    public abstract class GenericService<T>
        where T : BaseDataRow
    {
        private Repository _repository = new Repository();

        public void Insert(T folder)
        {
            _repository.Insert<T>(folder);
        }

        public void Delete(Guid id)
        {
            _repository.Delete<T>(id);
        }

        public T Read(Guid id)
        {
            return _repository.Find<T>(id);
        }

        public List<T> ReadAll()
        {
            return _repository.FindAll<T>();
        }
    }
}
