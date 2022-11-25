using AutoMapper;
using KUSYS.Business.Repositories;
using KUSYS.Business.Services.Base;
using KUSYS.Business.Services.Interfaces;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Data.Business.Services.StudentService;
using KUSYS.Data.Exceptions;
using KUSYS.Data.POCO;
using KUSYS.Data.Web.Base;
using KUSYS.Helper.WebHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static KUSYS.Data.Permissions.Permissions;
using System.Xml.Linq;

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

        public GetAllStudentsResponseModel GetAllStudents(bool isAdmin, Guid? studentId = null, int page = 1, int count = 10)
        {
            //pager için tüm kullanıcı sayısı alınır
            int recordCount = _studentRepository.GetCount();

            //kullanıcıları getirirken 2 seçeneğimiz var.
            //Kişi user ise sadece kendi kurslarını görüntüleyebilir
            //kişi admin ise her kullanıcının kurslarını görebilir
            var students = _studentRepository.GetAllQueryable().Include(i => i.Courses).ThenInclude(i => i.Course).Select(s => new StudentDto
            {
                BirthDate = s.BirthDate.ToShortDateString(),
                Fullname = s.FirstName + " " + s.Lastname,
                StudentId = s.Id,
                CourseNames = isAdmin ? string.Join(",", s.Courses.Select(s => s.Course.CourseName).ToArray()) : s.Id == studentId ? string.Join(",", s.Courses.Select(s => s.Course.CourseName).ToArray()) : "YETKİNİZ YOK",
                CourseIds = isAdmin ? string.Join(",", s.Courses.Select(s => s.Course.CourseId).ToArray()) : s.Id == studentId ? string.Join(",", s.Courses.Select(s => s.Course.CourseId).ToArray()) : "YETKİNİZ YOK",
                Username = s.User.Username
            }).Skip((page - 1) * count).Take(count).ToList();

            return new GetAllStudentsResponseModel
            {
                Students = students,
                TotalPageCount = recordCount
            };
        }

        //Ajax ile çağrıldığı için custom ResponseObject dönüyor. Cevabın içindeki propertylere göre javascript tarafında aksiyon alınıyor
        public ResponseObject<string> CreateStudentAjax(CreateStudentModel createStudentModel)
        {
            ResponseObject<string> response = new ResponseObject<string>();

            try
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
                    FirstName = createStudentModel.Firstname,
                    Lastname = createStudentModel.Lastname,
                    User = u
                };


                _userRepository.Insert(u);
                _studentRepository.Insert(s);

                _uow.Save();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.StatusCode = 400;
            }

            return response;
        }

        //Ajax ile çağrıldığı için custom ResponseObject dönüyor. Cevabın içindeki propertylere göre javascript tarafında aksiyon alınıyor
        public ResponseObject<string> DeleteStudentAjax(Guid studentId)
        {
            ResponseObject<string> response = new ResponseObject<string>();
            try
            {
                Guid userId = _studentRepository.Single(s => s.Id == studentId).UserId;
                User deletedUser = _userRepository.Single(s => s.Id == userId);

                _userRepository.Delete(deletedUser);

                _uow.Save();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.StatusCode = 400;
            }

            return response;
        }

        //Ajax ile çağrıldığı için custom ResponseObject dönüyor. Cevabın içindeki propertylere göre javascript tarafında aksiyon alınıyor
        public ResponseObject<StudentEditDto> UpdateStudentAjax(UpdateStudentModel updateModel)
        {
            ResponseObject<StudentEditDto> response = new ResponseObject<StudentEditDto>();
            try
            {
                Student editedStudent = null;

                if (updateModel.CourseIds == null) updateModel.CourseIds = new List<string>();

                if (updateModel.CourseIds.Count == 0)
                    editedStudent = _studentRepository.Single(s => s.Id == updateModel.Id);
                else
                {
                    editedStudent = _studentRepository.Single(s => s.Id == updateModel.Id, i => i.Courses);

                    //Bu sadece senaryomuza hizmet ettiği için yapıldı. Normalde tüm navigation property silinmeden userın bağlı olduğu kurslar kontrol edilir.
                    //Ama senaryoda kullanıcının tüm dersleri döndürüldüğü için önce kullanıcı dersleri dbden siliniyor sonra tekrar ekleniyor
                    editedStudent.Courses.Clear();
                }

                editedStudent.BirthDate = updateModel.BirthDate;
                editedStudent.ModifiedDate = DateTime.Now;
                editedStudent.FirstName = updateModel.Firstname;
                editedStudent.Lastname = updateModel.Lastname;

                var courseIds = _courseRepository.GetBy(w => updateModel.CourseIds.Contains(w.CourseId)).Select(s => s.CourseId).ToList();

                //her kurs için öğrenci bağlamasını yapılıyor
                courseIds.ForEach(each =>
                {
                    editedStudent.Courses.Add(new StudentCourses
                    {
                        CourseId = each,
                        StudentId = editedStudent.Id
                    });

                });

                _uow.Save();


                response.Data = new StudentEditDto
                {
                    Name = editedStudent.FirstName,
                    Surname = editedStudent.Lastname,
                    StudentId = editedStudent.Id,
                    BirthDate = editedStudent.BirthDate,
                    CourseIds = editedStudent.Courses.Select(s => s.CourseId)
                };

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.StatusCode = 400;
            }
            return response;
        }

        public StudentEditDto GetStudentForEdit(Guid studentId)
        {
            var response = _studentRepository.GetBy(b => b.Id == studentId)
                .Include(i => i.Courses)
                .ThenInclude(ti => ti.Course)
                .Select(s => new StudentEditDto
                {
                    Name = s.FirstName,
                    Surname = s.Lastname,
                    StudentId = s.Id,
                    BirthDate = s.BirthDate,
                    CourseIds = s.Courses.Select(s => s.CourseId)
                }).SingleOrDefault();

            if (response == null)
                throw new AppException("Düzenlenecek öğrenci bilgisi bulunamadı! Id:" + studentId.ToString());


            return response;
        }

    }
}
