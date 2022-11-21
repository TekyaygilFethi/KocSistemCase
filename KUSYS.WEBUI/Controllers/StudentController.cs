using AutoMapper;
using KUSYS.Business.Services.Base;
using KUSYS.Business.Services.Classes;
using KUSYS.Business.Services.Interfaces;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Data.Business.Services.StudentService;
using KUSYS.WEBUI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.WEBUI.Controllers
{
    [Route("student")]
    public class StudentController : BaseController
    {
        private IStudentService _studentService;
        public StudentController(IConfiguration configuration, IUnitOfWork uow, IMapper mapper) : base(configuration, uow, mapper)
        {
            _studentService = base.GetService<StudentService>();
        }
        

        public IActionResult Index()
        {
            return View();
        }


        [HttpDelete, Route("delete-student")]
        public IActionResult DeleteStudent([FromQuery] Guid StudentId)
        {
            _studentService.DeleteStudent(StudentId);

            return Ok();
        }


        [HttpGet, Route("get-students")]
        public IActionResult GetStudents()
        {
            var students = _studentService.GetAllStudents();

            return Ok(students);
        }



        [HttpPut, Route("update-student")]
        public IActionResult UpdateStudent([FromBody]UpdateStudentModel requestModel)
        {
            if (ModelState.IsValid)
            {
                _studentService.UpdateStudent(requestModel);

                return Ok();
            }
            else
                return BadRequest();
        }


        public IActionResult GetCourses()
        {
            _studentService

            return Ok();
        }
    }
}
