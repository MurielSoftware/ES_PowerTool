using ES_PowerTool.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.Context
{
    public class ES_PowerToolDataContext : DbContext
    {
        public ES_PowerToolDataContext()
            : base("ES_PowerToolDataContext")
        {

        }

        public DbSet<CompositeType> CompositeTypes { get; set; }
    }
}
