using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Services
{
    public interface ICopyAwareCRUDService<T> where T : BaseDto
    {
        void Copy(T dto);
    }
}
