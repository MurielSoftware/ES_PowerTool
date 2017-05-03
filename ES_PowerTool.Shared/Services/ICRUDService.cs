using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.Services
{
    public interface ICRUDService<T> where T : BaseDto
    {
        T Read(Guid id);
    }
}
