using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Data.Business.Services.StudentService
{
    public class GetAllStudentsResponseModel
    {
        public Guid StudentId { get; set; }

        public string Fullname { get; set; }

        public string BirthDate { get; set; }

        public string CourseIds { get; set; }

        public string CourseNames { get; set; }
    }
}
