using KUSYS.Data.Business.Services.StudentService;

namespace KUSYS.Business.Services.Interfaces
{
    public interface ICourseService
    {

        List<StudentEditCourseDto> GetCoursesForEdit();
    }
}
