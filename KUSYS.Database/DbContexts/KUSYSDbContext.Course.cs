using KUSYS.Data.POCO;
using KUSYS.Database.ModelBuilders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Database.DbContexts
{
    public partial class KUSYSDbContext
    {
        public DbSet<Course> Courses { get; set; }

        private void ConfigureCourseEntities(ModelBuilder builder)
        {
            builder.ConfigureCourseBuilder();
        }
    }
}
