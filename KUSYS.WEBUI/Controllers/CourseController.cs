using AutoMapper;
using KUSYS.Business.Services.Classes;
using KUSYS.Business.Services.Interfaces;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Data.Business.Services.StudentService;
using KUSYS.Data.Web.Base;
using KUSYS.WEBUI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.WEBUI.Controllers
{
    [Route("course")]
    public class CourseController : BaseController
    {
        private ICourseService _courseService;

        public CourseController(IConfiguration configuration, IUnitOfWork uow, IMapper mapper) : base(configuration, uow, mapper)
        {
            _courseService = base.GetService<ICourseService>();
        }

        //Öğrenci düzenlerken tüm dersleri comboboxa getirmek için istek
        [Authorize(Roles = "Admin,User")]
        [HttpGet, Route("get-all-courses-for-edit")]
        public IActionResult GetAllCoursesForEdit()
        {
            var courses = _courseService.GetCoursesForEdit();

            return Ok(courses);
        }
    }
}
