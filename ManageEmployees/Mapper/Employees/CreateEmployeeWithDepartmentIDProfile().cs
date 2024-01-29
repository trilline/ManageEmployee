using AutoMapper;
using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.Employees
{
    public class CreateEmployeeWithDepartmentIDProfile : Profile
    {

        public CreateEmployeeWithDepartmentIDProfile()
        {
            CreateMap<CreateEmployeeWithDepartmentID, Employee>();
           //.ForMember(dest => dest.Departments., opt => opt.MapFrom(src => src.de));
        }
    }
}
