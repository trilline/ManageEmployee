using AutoMapper;
using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Entities;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ManageEmployees.Mapper.Attendances
{
    public class ReadAttendanceProfile : Profile
    {
        public ReadAttendanceProfile()
        {
            CreateMap<Attendance, ReadAttendance>()
                .ForMember(dest => dest.Attendanceid, opt => opt.MapFrom(src => src.Attendanceid))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Employeeid))
                .ForMember(dest => dest.Arrivaldate, opt => opt.MapFrom(src => src.Arrivaldate))
                .ForMember(dest => dest.Departuredate, opt => opt.MapFrom(src => src.Departuredate));
        }
    }
}
