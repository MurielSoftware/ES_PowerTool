using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.DAL;

namespace ES_PowerTool.Data.DAL
{
    public class CompositeTypeRepository : BaseRepository
    {
        public CompositeTypeRepository(Connection connection) 
            : base(connection)
        {
        }
    }
}
