using AutoMapper;
using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;


namespace ManageEmployees.Mapper.Departments
{
    public class CreateDepartmentProfile : Profile
    {
        public CreateDepartmentProfile()
        {
            CreateMap<Department, CreateDepartment>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ReverseMap();
        }
    }
}
