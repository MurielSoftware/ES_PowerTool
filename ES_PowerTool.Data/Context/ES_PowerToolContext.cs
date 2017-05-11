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
            Database.SetInitializer<ES_PowerToolContext>(new DropCreateDatabaseIfModelChanges<ES_PowerToolContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CompositeTypeElement>().HasRequired(x => x.ElementType).WithMany().HasForeignKey(x => x.ElementTypeId).WillCascadeOnDelete(false);
            modelBuilder.Entity<CompositeTypeElement>().HasRequired(x => x.OwningType).WithMany().HasForeignKey(x => x.OwningTypeId).WillCascadeOnDelete(false);
        }

        public DbSet<CompositeType> CompositeTypes { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<CompositeTypeElement> CompositeTypeElements { get; set; }
    }
}
