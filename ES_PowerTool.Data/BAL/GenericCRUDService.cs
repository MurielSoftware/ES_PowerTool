using Desktop.Shared.Core.Dtos;
using ES_PowerTool.Data.DAL;
using ES_PowerTool.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.BAL
{
    public class GenericCRUDService<T, U>
        where T : BaseDto
        where U : BaseEntity
    {
        protected GenericRepository _genericRepository = new GenericRepository();

        public T Read(Guid id)
        {
            U t = _genericRepository.Find<U>(id);
            return null;
        }
    }
}
