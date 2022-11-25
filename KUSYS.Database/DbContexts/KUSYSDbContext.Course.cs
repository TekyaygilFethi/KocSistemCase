using KUSYS.Data.POCO;
using KUSYS.Database.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.Database.DbContexts
{
    public partial class KUSYSDbContext
    {
        public DbSet<Course> Courses { get; set; }
    }
}
