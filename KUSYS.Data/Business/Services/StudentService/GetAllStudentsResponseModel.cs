namespace KUSYS.Data.Business.Services.StudentService
{
    public class GetAllStudentsResponseModel
    {
        public int TotalPageCount { get; set; }

        public List<StudentDto> Students { get; set; }
    }
}
