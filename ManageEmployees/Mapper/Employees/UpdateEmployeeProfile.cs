using AutoMapper;
using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.Employees
{
    public class UpdateEmployeeProfile : Profile
    {
        public UpdateEmployeeProfile()
        {
            CreateMap<UpdateEmployee, Employee>();
        }
    }
}
