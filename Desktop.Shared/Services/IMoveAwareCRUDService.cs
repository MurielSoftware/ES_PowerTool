using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Services
{
    public interface IMoveAwareCRUDService
    {
        void Move(Guid sourceId, Guid targetId);
    }
}
