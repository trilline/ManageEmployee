using AutoMapper;
using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.Attendances
{
    public class CreateAttendanceProfile : Profile
    {
        public CreateAttendanceProfile()
        {
            CreateMap<Attendance, CreateAttendance>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Employeeid))
                .ForMember(dest => dest.Arrivaldate, opt => opt.MapFrom(src => src.Arrivaldate))
                .ForMember(dest => dest.Departuredate, opt => opt.MapFrom(src => src.Departuredate))
                .ReverseMap();
        }
    }

}
