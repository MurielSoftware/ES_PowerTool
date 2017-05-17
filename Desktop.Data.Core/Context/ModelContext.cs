using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Context;
using System.Data.Entity;

namespace Desktop.Data.Core.Context
{
    public class ModelContext : DbContext, IDatabaseContext
    {
        public ModelContext(string connectionString)
        {
            Database.Connection.ConnectionString = connectionString;
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
        public DbSet<Project> Projects { get; set; }
    }
}
