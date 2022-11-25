using KUSYS.Data.POCO;
using KUSYS.Database.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.Database.DbContexts
{
    public partial class KUSYSDbContext
    {
        public DbSet<StudentCourses> StudentCourses { get; set; }

        private void ConfigureStudentCoursesEntities(ModelBuilder builder)
        {
            builder.ConfigureStudentCoursesBuilder();
        }
    }
}
