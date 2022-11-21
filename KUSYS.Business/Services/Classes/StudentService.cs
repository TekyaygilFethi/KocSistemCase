using AutoMapper;
using KUSYS.Business.Repositories;
using KUSYS.Business.Services.Base;
using KUSYS.Business.Services.Interfaces;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Data.Business.Services.StudentService;
using KUSYS.Data.POCO;
using KUSYS.Helper.WebHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Business.Services.Classes
{
    public class StudentService : BaseService, IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Course> _courseRepository;

        public StudentService(IUnitOfWork uow, IConfiguration configuration, IMapper mapper) : base(configuration, uow, mapper)
        {
            _studentRepository = uow.GetRepository<Student>();
            _userRepository = uow.GetRepository<User>();
            _courseRepository = uow.GetRepository<Course>();
        }


        public List<GetAllStudentsResponseModel> GetAllStudents(int page = 0, int count = 10)
        {
            return _studentRepository.GetAllQueryable(true).Include(i=>i.Courses).ThenInclude(i=>i.Course).Select(s=>new GetAllStudentsResponseModel
            { 
                BirthDate = s.BirthDate.ToShortDateString(),
                Fullname = s.FirstName + " "+s.Lastname,
                StudentId = s.Id,
                CourseNames = string.Join(",",s.Courses.Select(s=>s.Course.CourseName).ToArray()),
                CourseIds = string.Join(",", s.Courses.Select(s => s.Course.CourseId).ToArray())
            }).Skip(page*count).Take(count).ToList();
        }

        public void AddStudent(CreateStudentModel createStudentModel)
        {
            string salt = _configuration.GetSection("Salt").Value;
            User u = new User()
            {
                Username = createStudentModel.Username,
                Password = CryptographyHelper.Encode(createStudentModel.Password + salt),
                Role = createStudentModel.Role
            };


            Student s = new Student
            {
                BirthDate = createStudentModel.BirthDate,
                FirstName = createStudentModel.FirstName,
                Lastname = createStudentModel.Lastname,
                User = u
            };


            _userRepository.Insert(u);
            _studentRepository.Insert(s);

            _uow.Save();
        }


        public void DeleteStudent(Guid studentId)
        {
            Guid userId = _studentRepository.Single(s => s.Id == studentId).UserId;
            User deletedUser = _userRepository.Single(s => s.Id == userId);

            _userRepository.Delete(deletedUser);

            _uow.Save();
        }


        public void UpdateStudent(UpdateStudentModel updateModel)
        {
            Student oldStudent = null;

            if (updateModel.CourseIds == null) updateModel.CourseIds = new List<string>();

            if (updateModel.CourseIds.Count == 0)
                oldStudent = _studentRepository.Single(s => s.Id == updateModel.Id);
            else
            {
                oldStudent = _studentRepository.Single(s => s.Id == updateModel.Id, i => i.Courses);
                oldStudent.Courses.Clear();
            }

            oldStudent.BirthDate = updateModel.BirthDate;
            oldStudent.ModifiedDate = DateTime.Now;
            oldStudent.FirstName = updateModel.FirstName;
            oldStudent.Lastname = updateModel.Lastname;

            var courseIds = _courseRepository.GetBy(w => updateModel.CourseIds.Contains(w.CourseId)).Select(s => s.CourseId).ToList();

            courseIds.ForEach(each =>
            {
                oldStudent.Courses.Add(new StudentCourses
                {
                    CourseId = each,
                    StudentId = oldStudent.Id
                });

            });

            _uow.Save();
        }

        public object GetStudentCourses(Guid studentId)
        {
            throw new NotImplementedException();
        }
    }
}
