using Desktop.Data.Core.Context;
using Desktop.Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.DAL
{
    public class BaseRepository
    {
        private ModelContext _context;

        public BaseRepository(Connection connection)
        {
            _context = (ModelContext)connection.GetContext();
        }

        protected ModelContext GetContext()
        {
            return _context;
        }
    }
}
