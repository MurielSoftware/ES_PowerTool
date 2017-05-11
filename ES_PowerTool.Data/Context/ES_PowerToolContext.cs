using Desktop.Shared.Core.Context;
using ES_PowerTool.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.Context
{
    public class ES_PowerToolContext : DbContext, IDatabaseContext
    {
        public ES_PowerToolContext()
            : base("ES_PowerToolDb")
        {
        }

        public DbSet<CompositeType> CompositeTypes { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<CompositeTypeElement> CompositeTypeElements { get; set; }
    }
}
