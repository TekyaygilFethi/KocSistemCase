using KUSYS.Data.Business.Services.StudentService;
using KUSYS.Data.Web.Base;

namespace KUSYS.Business.Services.Interfaces
{
    public interface IStudentService
    {
        GetAllStudentsResponseModel GetAllStudents(bool isAdmin, Guid? studentId = null, int page = 1, int count = 10);
        StudentEditDto GetStudentForEdit(Guid studentId);
        ResponseObject<string> CreateStudentAjax(CreateStudentModel createStudentModel);

        ResponseObject<StudentEditDto> UpdateStudentAjax(UpdateStudentModel updateModel);

        ResponseObject<string> DeleteStudentAjax(Guid id);
    }
}
