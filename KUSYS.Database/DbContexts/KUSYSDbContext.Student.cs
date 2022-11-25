using KUSYS.Data.POCO;
using KUSYS.Database.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.Database.DbContexts
{
    public partial class KUSYSDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Student> Students { get; set; }

        private void ConfigureStudentEntities(ModelBuilder builder)
        {
            builder.ConfigureStudentBuilder();
        }
    }
}
