using AutoMapper;
using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.Departments
{
    public class ReadDepartmentProfile : Profile
    {
        public ReadDepartmentProfile()
        {
            CreateMap<Department, ReadDepartment>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        }
    }
}
