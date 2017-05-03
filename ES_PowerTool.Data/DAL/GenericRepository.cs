using ES_PowerTool.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.DAL
{
    public class GenericRepository : BaseRepository
    {
        internal T Find<T>(Guid id) where T : BaseEntity
        {
            long X = _modelContext.Set<CompositeType>().Count();
            _modelContext.Set<CompositeType>().Add(new CompositeType() { Id = id });
            _modelContext.SaveChanges();
            return _modelContext.Set<T>().Where(x => x.Id == id).SingleOrDefault();
        }
    }
}
