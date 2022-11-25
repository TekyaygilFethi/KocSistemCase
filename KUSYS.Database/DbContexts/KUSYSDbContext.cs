using KUSYS.Database.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.Database.DbContexts
{
    public partial class KUSYSDbContext : DbContext
    {
        public KUSYSDbContext(DbContextOptions options) : base(options) { }

        public KUSYSDbContext()
          : base() { }

        public KUSYSDbContext(string connectionString)
          : base(SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options)
        { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ConfigureStudentEntities(builder);
            ConfigureStudentCoursesEntities(builder);

            builder.Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            builder.EnableSensitiveDataLogging();
        }

        //public override int SaveChanges()
        //{
        //    UpdateSoftDeleteStatuses();
        //    return base.SaveChanges();
        //}

        //public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    UpdateSoftDeleteStatuses();
        //    return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //}

        //private void UpdateSoftDeleteStatuses()
        //{
        //    foreach (var entry in ChangeTracker.Entries())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.CurrentValues["IsDeleted"] = false;
        //                break;
        //            case EntityState.Deleted:
        //                entry.State = EntityState.Modified;
        //                entry.CurrentValues["IsDeleted"] = true;
        //                break;
        //        }
        //    }
        //}
    }
}
