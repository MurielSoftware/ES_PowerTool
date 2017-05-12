using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Services
{
    public interface ICRUDService<T> where T : BaseDto
    {
        T Read(Guid id);
        T Persist(T dto);
        void Delete(Guid id);
    }
}
