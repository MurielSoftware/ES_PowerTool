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
        protected ES_PowerToolDataContext _modelContext;

        internal BaseRepository()
        {
            _modelContext = new ES_PowerToolDataContext();
        }
    }
}
