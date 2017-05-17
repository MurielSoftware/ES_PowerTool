using Desktop.Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Services
{
    /// <summary>
    /// The Base service. All services should extend this service.
    /// </summary>
    public abstract class BaseService : IService
    {
        protected Connection _connection;

        public BaseService(Connection connection)
        {
            _connection = connection;
        }
    }
}
