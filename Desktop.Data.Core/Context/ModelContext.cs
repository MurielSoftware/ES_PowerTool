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
            //modelBuilder.Entity<Preset>().HasRequired(x => x.CompositeType).WithMany().HasForeignKey(x => x.CompositeTypeId).WillCascadeOnDelete(false);
            modelBuilder.Entity<CompositeType>().HasMany(x => x.Presets).WithRequired().HasForeignKey(x => x.TypeId).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Preset>().HasRequired(x => x.).WithMany().HasForeignKey(x => x.OwningTypeId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>().HasMany(x => x.Folders).WithRequired().HasForeignKey(x => x.ProjectId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Project>().HasMany(x => x.ModelObjectTypes).WithRequired().HasForeignKey(x => x.ProjectId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Project>().HasMany(x => x.CompositeTypeElements).WithRequired().HasForeignKey(x => x.ProjectId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Project>().HasMany(x => x.Presets).WithRequired().HasForeignKey(x => x.ProjectId).WillCascadeOnDelete(false);


            modelBuilder.Entity<ModelObjectType>()
                .Map<CompositeType>(m => m.Requires("Discriminator").HasValue(CompositeType.DISC))
                .Map<PrimitiveType>(m => m.Requires("Discriminator").HasValue(PrimitiveType.DISC));
        }

        public DbSet<ModelObjectType> ModelObjectTypes { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<CompositeTypeElement> CompositeTypeElements { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Preset> Preset { get; set; }
    }
}
