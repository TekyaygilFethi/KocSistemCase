using KUSYS.Business.Services.Base;
using KUSYS.Data.Business.Services.StudentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Business.Services.Interfaces
{
    public interface IStudentService
    {
        List<GetAllStudentsResponseModel> GetAllStudents(int page = 0, int count = 10);
        void UpdateStudent(UpdateStudentModel updateModel);

        void DeleteStudent(Guid id);

        object GetStudentCourses(Guid studentId);
    }
}
