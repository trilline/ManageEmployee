using AutoMapper;
using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.Attendances
{
    public class UpdateAttendanceProfile : Profile
    {
        public UpdateAttendanceProfile()
        {
            CreateMap<Attendance, UpdateAttendance>()
                .ForMember(dest => dest.Arrivaldate, opt => opt.MapFrom(src => src.Arrivaldate))
                .ForMember(dest => dest.Departuredate, opt => opt.MapFrom(src => src.Departuredate))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Employeeid));
                
        }
    }

}
