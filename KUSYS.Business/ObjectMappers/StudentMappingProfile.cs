using AutoMapper;
using KUSYS.Data.Business.Services.StudentService;
using KUSYS.Data.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Business.ObjectMappers
{
    internal class StudentMappingProfile : Profile, IProfile
    {
        public StudentMappingProfile()
        {
            ConfigureMappings();
        }

        public void ConfigureMappings()
        {
            CreateMap<UpdateStudentModel, Student>()
                .ForMember(member => member.ModifiedDate, opt => opt.Ignore())
                .ForMember(member => member.Courses, opt => opt.Ignore())
                .ForMember(member => member.User, opt=>opt.Ignore())
                .ForMember(member => member.UserId, opt => opt.Ignore())
                .ForMember(member => member.Id, opt => opt.Ignore())
                .ReverseMap();


            //CreateMap<StudentCourses, StudentCourseUpdateModel>();



            //CreateMap<InvoiceDto, Invoice>()
            //    .ForMember(member => member.Membership, opt => opt.Ignore())
            //    .ForMember(member => member.MembershipId, opt => opt.Ignore())
            //    .ForMember(member => member.ModifiedDate, opt => opt.Ignore())
            //    .ReverseMap();

            //CreateMap<GetInvoicesRequestModel, GetInvoicesServiceRequestModel>()
            //    .ForMember(member => member.UserId, opt => opt.Ignore())
            //    .ReverseMap();


            //CreateMap<UpdateInvoiceRequestModel, Invoice>()
            //    .ForMember(member => member.IsDeleted, opt => opt.Ignore())
            //    .ForMember(member => member.InvoiceLines, opt => opt.Ignore())
            //    .ForMember(member => member.ModifiedDate, opt => opt.Ignore())
            //    .ForMember(member => member.DeletedDate, opt => opt.Ignore())
            //    .ForMember(member => member.Membership, opt => opt.Ignore())
            //    .ForMember(member => member.MembershipId, opt => opt.Ignore())
            //    .ReverseMap();


            //CreateMap<AddOrUpdateInvoiceLineRequestModel, InvoiceLine>()
            //    .ForMember(member => member.DeletedDate, opt => opt.Ignore())
            //    .ForMember(member => member.Id, opt => opt.Ignore())
            //    .ForMember(member => member.ModifiedDate, opt => opt.Ignore())
            //    .ForMember(member => member.DeletedDate, opt => opt.Ignore())
            //    .ForMember(member => member.IsDeleted, opt => opt.Ignore())
            //    .ForMember(member => member.Invoice, opt => opt.Ignore())
            //    .ForMember(member => member.InvoiceId, opt => opt.Ignore())
            //    .ReverseMap();



        }
    }
}
