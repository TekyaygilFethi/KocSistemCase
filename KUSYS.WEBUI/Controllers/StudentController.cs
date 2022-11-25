using AutoMapper;
using KUSYS.Business.Caching.Base;
using KUSYS.Business.Services.Classes;
using KUSYS.Business.Services.Interfaces;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Data.Business.Services.StudentService;
using KUSYS.Data.POCO;
using KUSYS.Data.Web.Base;
using KUSYS.WEBUI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental.ProjectCache;
using StackExchange.Redis;
using static KUSYS.Data.Permissions.Permissions;

namespace KUSYS.WEBUI.Controllers
{
    [Route("student")]
    public class StudentController : BaseController
    {
        private IStudentService _studentService;
        private readonly ICacheService _cacheService;
        public StudentController(IConfiguration configuration, IUnitOfWork uow, IMapper mapper) : base(configuration, uow, mapper)
        {
            _cacheService = uow.GetCacheService();

            _studentService = base.GetService<IStudentService>();
        }

        #region Create Student
        [Authorize(Roles = "Admin")]
        [HttpGet, Route("create-student")]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, Route("create-student")]
        public IActionResult CreateStudent(CreateStudentModel createStudentModel)
        {
            if (ModelState.IsValid)
            {
                var data = _studentService.CreateStudentAjax(createStudentModel);

                //data responseObject tipinde, içindeki issuccess'e göre javascript tarafında aksiyon alınacak.
                return Ok(data);
            }

            return BadRequest("Model uygun değil!");
        }
        #endregion

        #region Delete Student
        [Authorize(Roles = "Admin")]
        [HttpDelete, Route("delete-student")]
        public IActionResult DeleteStudent([FromQuery] Guid StudentId)
        {
            var deleteResponse = _studentService.DeleteStudentAjax(StudentId);

            return Ok(deleteResponse);
        }
        #endregion

        #region Student List
        [Authorize(Roles = "Admin, User")]
        [HttpGet, Route("get-students")]
        public IActionResult GetStudents(int page = 1, int count = 10)
        {
            bool isAdmin = User.IsInRole(UserRoleEnum.Admin.ToString());
            Guid? studentId = null;
            if (!isAdmin)
                studentId = new Guid(User.Claims.Single(s => s.Type == "StudentId").Value);

            var students = _studentService.GetAllStudents(isAdmin, studentId, page, count);


            #region Pager Settings
            //how many total pages are there?
            int totalPageCount = (students.TotalPageCount + count - 1) / count;

            TempData["CurrentPage"] = page;
            TempData["ItemCount"] = count;
            TempData["TotalPageCount"] = totalPageCount;
            TempData["IsAdmin"] = isAdmin;

            //For pager navigation numbers, lower limit
            int lowerLimit = page - 5;
            lowerLimit = lowerLimit <= 0 ? 1 : lowerLimit;

            //For pager navigation numbers, upper limit
            int upperLimit = page + 5;
            upperLimit = upperLimit >= totalPageCount ? totalPageCount : upperLimit;

            //Num page elements is the count of how many valid pages are there. For instance my aim is show 5 pages at most in pager ...2,3,4,5,6... but if the
            //upper limit is 5 (the pager can have 5 pages at most) then my numPageElements should be 4 and the pages should be ...2,3,4,5...
            int numPageElements = (upperLimit - lowerLimit) + 1;
            numPageElements = numPageElements == 0 ? 1 : numPageElements;

            TempData["LowerLimit"] = lowerLimit;
            TempData["NumPageElements"] = numPageElements;
            #endregion

            return View(students.Students);
        }
        #endregion

        #region Update Student

        [Authorize(Roles = "Admin,User")]
        [HttpGet, Route("update-student")]
        public IActionResult UpdateStudent([FromQuery] Guid studentId)
        {
            StudentEditDto updatedStudent = _cacheService.Get<StudentEditDto>(GLOBALS.EDIT_STUDENT_CACHE_NAME.Replace("{0}", studentId.ToString()));
            if (updatedStudent == null) updatedStudent = _studentService.GetStudentForEdit(studentId);

            return View(updatedStudent);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut, Route("update-student")]
        public IActionResult UpdateStudent([FromBody] UpdateStudentModel requestModel)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Admin") || (User.IsInRole("User") && User.Claims.Single(s => s.Type == "StudentId").Value == requestModel.Id.ToString()))
                {
                    var updateResponse = _studentService.UpdateStudentAjax(requestModel);

                    _cacheService.Set<StudentEditDto>(GLOBALS.EDIT_STUDENT_CACHE_NAME.Replace("{0}", requestModel.Id.ToString()), updateResponse.Data);
                    return Ok(updateResponse);
                }
                else
                    return BadRequest("Yetkiniz yok!");
            }

            return BadRequest("Model uygun değil!");

        }

        #endregion
    }
}
