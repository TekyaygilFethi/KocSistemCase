using AutoMapper;
using KUSYS.Business.Repositories;
using KUSYS.Business.Services.Base;
using KUSYS.Business.Services.Interfaces;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Data.Business.Services.StudentService;
using KUSYS.Data.POCO;
using Microsoft.Extensions.Configuration;

namespace KUSYS.Business.Services.Classes
{
    public class CourseService : BaseService, ICourseService
    {
        private readonly IRepository<Course> _courseRepository;

        public CourseService(IUnitOfWork uow, IConfiguration configuration, IMapper mapper) : base(configuration, uow, mapper)
        {
            _courseRepository = uow.GetRepository<Course>();
        }

        public List<StudentEditCourseDto> GetCoursesForEdit()
        {
            return _courseRepository.GetAllQueryable().Select(s => new StudentEditCourseDto
            {
                CourseId = s.CourseId,
                CourseName = s.CourseName
            }).ToList();
        }
    }
}
