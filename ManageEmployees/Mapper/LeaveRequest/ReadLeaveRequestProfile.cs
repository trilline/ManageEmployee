using AutoMapper;
using ManageEmployees.Dtos.Leaverequest;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.LeaveRequest
{
    public class ReadLeaveRequestProfile : Profile
    {
        public ReadLeaveRequestProfile()
        {
            CreateMap<Leaverequest, ReadLeaveRequest>()
                .ForMember(dest => dest.LeaveRequestId, opt => opt.MapFrom(src => src.Leaverequestid))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Employeeid))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Startdate))
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.Requestdate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Enddate))
                .ForMember(dest => dest.StatusLeaveRequestId, opt => opt.MapFrom(src => src.Statusleaverequestid))
                .ForMember(dest => dest.StatusLeaveRequestLabel, opt => opt.MapFrom(src => src.Statusleaverequest.Statuslabel));
        }
    }
}
