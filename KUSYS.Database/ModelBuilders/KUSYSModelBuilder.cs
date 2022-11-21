using KUSYS.Data.POCO;
using KUSYS.Helper.WebHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

namespace KUSYS.Database.ModelBuilders
{
    public static class KUSYSModelBuilder
    {
        public static void ConfigureStudentBuilder(this ModelBuilder builder)
        {
            builder.Entity<User>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<User>()
                .HasIndex(p => p.Username)
                .IsUnique();


            builder.Entity<Student>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            //Student User relationship
            builder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(fk => fk.UserId)
                .HasPrincipalKey<User>(pk => pk.Id);

            //Student Course relationship
            builder.Entity<Student>()
                 .HasMany(s => s.Courses)
                 .WithOne(o => o.Student)
                 .HasPrincipalKey(o => o.Id);

            //builder.Entity<Student>()
            //    .HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            //builder.Entity<Student>()
            //.Property(b => b.IsDeleted)
            //.HasDefaultValue(0);

        }

        public static void ConfigureStudentCoursesBuilder(this ModelBuilder builder)
        {
            builder.Entity<StudentCourses>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder.Entity<Course>()
                 .HasMany(s => s.Students)
                 .WithOne(o => o.Course)
                 .HasPrincipalKey(o => o.CourseId);

            //builder.Entity<StudentCourses>()
            //    .HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            //builder.Entity<StudentCourses>()
            //    .Property(b => b.IsDeleted)
            //    .HasDefaultValue(0);
        }

        public static void ConfigureCourseBuilder(this ModelBuilder builder)
        {
            //builder.Entity<Course>()
            //    .HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
        }


        public static void Seed(this ModelBuilder builder)
        {
            var _configuration = new ConfigurationBuilder().
               SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(@"appsettings.json", false, false)
               .AddEnvironmentVariables()
               .Build();


            #region Courses Insertion
            Course c1 = new Course
            {
                CourseId = "CSI101",
                CourseName = "Introduction to Computer Science"
            };

            Course c2 = new Course
            {
                CourseId = "CSI102",
                CourseName = "Algorithms"
            };

            Course c3 = new Course
            {
                CourseId = "MAT101",
                CourseName = "Calculus"
            };

            Course c4 = new Course
            {
                CourseId = "PHY101",
                CourseName = "Physics"
            };

            #endregion

            #region User Insertion
            var salt = _configuration.GetSection("Salt").Value;

            User u1 = new User
            {
                Id = Guid.NewGuid(),
                Username = "fethitekyaygil",
                Password = CryptographyHelper.Encode("1q2w3e4r" + salt),
                ModifiedDate = DateTime.Now,
                Role = UserRoleEnum.Admin
            };

            User u2 = new User
            {
                Id = Guid.NewGuid(),
                Username = "yaseminozen",
                Password = CryptographyHelper.Encode("123456" + salt),
                ModifiedDate = DateTime.Now,
                Role = UserRoleEnum.User
            };

            User u3 = new User
            {
                Id = Guid.NewGuid(),
                Username = "tahatekyaygil",
                Password = CryptographyHelper.Encode("987654" + salt),
                ModifiedDate = DateTime.Now,
                Role = UserRoleEnum.User
            };


            #endregion

            #region Student Insertion

            #region Student 1
            Student s1 = new Student
            {
                Id = Guid.NewGuid(),
                FirstName = "Fethi",
                Lastname = "Tekyaygil",
                BirthDate = new DateTime(1996, 6, 7),
                UserId = u1.Id
            };


            StudentCourses s1c1 = new StudentCourses { StudentId = s1.Id, CourseId = c1.CourseId };
            StudentCourses s1c3 = new StudentCourses { StudentId = s1.Id, CourseId = c3.CourseId };
            //List<StudentCourses> s1Courses = new List<StudentCourses>
            //{
            //    s1c1,
            //    s1c3
            //};

            //s1.Courses = s1Courses;
            //c1.Students.Add(s1c1);
            //c3.Students.Add(s1c3);

            #endregion

            #region Student 2
            Student s2 = new Student
            {
                Id = Guid.NewGuid(),
                FirstName = "Yasemin",
                Lastname = "Özen",
                BirthDate = new DateTime(1997, 5, 7),
                UserId = u2.Id
            };

            StudentCourses s2c2 = new StudentCourses { StudentId = s2.Id, CourseId = c2.CourseId };
            //List<StudentCourses> s2Courses = new List<StudentCourses>
            //{
            //    s2c2
            //};
            //s2.Courses = s2Courses;
            //c2.Students.Add(s2c2);

            #endregion

            #region Student 3
            Student s3 = new Student
            {
                Id = Guid.NewGuid(),
                FirstName = "Taha",
                Lastname = "Tekyaygil",
                BirthDate = new DateTime(2008, 10, 5),
                UserId = u3.Id
            };
            StudentCourses s3c1 = new StudentCourses { StudentId = s3.Id, CourseId = c1.CourseId };
            StudentCourses s3c2 = new StudentCourses { StudentId = s3.Id, CourseId = c2.CourseId };
            StudentCourses s3c3 = new StudentCourses { StudentId = s3.Id, CourseId = c3.CourseId };

            //StudentCourses s3c1 = new StudentCourses { Student = s3, Course = c1 };
            //StudentCourses s3c2 = new StudentCourses { Student = s3, Course = c2 };
            //StudentCourses s3c3 = new StudentCourses { Student = s3, Course = c3 };
            //List<StudentCourses> s3Courses = new List<StudentCourses>
            //{
            //    s3c1,
            //    s3c2,
            //    s3c3
            //};

            //s3.Courses = s3Courses;
            //c1.Students.Add(s3c1);
            //c2.Students.Add(s3c2);
            //c3.Students.Add(s3c3);

            #endregion

            #endregion

            builder.Entity<StudentCourses>().HasData(
                s1c1, s1c3, s2c2, s3c1, s3c2, s3c3);

            builder.Entity<Course>().HasData(
               c1, c2, c3, c4);

            builder.Entity<User>().HasData(
                u1, u2, u3);

            builder.Entity<Student>().HasData(
                s1, s2, s3);
        }
    }
}
