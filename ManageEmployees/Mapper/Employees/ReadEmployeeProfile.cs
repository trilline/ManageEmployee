using AutoMapper;
using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.Employees
{
    public class ReadEmployeeProfile : Profile
    {
        public ReadEmployeeProfile()
        {
            CreateMap<Employee, ReadEmployee>()
                .ForMember(dest => dest.DepartmentLabel, opt => opt.MapFrom(src => src.Departments.FirstOrDefault().Name));
        }
    }
}
