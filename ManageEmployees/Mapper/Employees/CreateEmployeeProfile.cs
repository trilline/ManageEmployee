using AutoMapper;
using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.Employees
{
    public class CreateEmployeeProfile : Profile
    {
        public CreateEmployeeProfile()
        {
            CreateMap<CreateEmployee, Employee>();
        }
    }
}
