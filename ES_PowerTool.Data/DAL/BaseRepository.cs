using Desktop.Shared.Core.Context;
using ES_PowerTool.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.DAL
{
    public class BaseRepository
    {
        private ES_PowerToolContext _context;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _context = new ES_PowerToolContext();
        }

        protected ES_PowerToolContext GetContext()
        {
            return _context;
        }
    }
}
