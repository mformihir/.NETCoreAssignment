using AutoMapper;
using HRM.Business.Models;
using HRM.Data.Models;

namespace HRM.Business.Automapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<DepartmentBusinessModel, Department>();
            CreateMap<Department, DepartmentBusinessModel>();
            CreateMap<Employee, EmployeeBusinessModel>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.Manager, opt => opt.MapFrom(src => src.Manager != null ? src.Manager.Name : ""));
            CreateMap<EmployeeBusinessModel, Employee>();
        }
    }
}
