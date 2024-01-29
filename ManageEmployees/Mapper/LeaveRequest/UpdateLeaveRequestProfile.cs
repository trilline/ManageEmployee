using AutoMapper;
using ManageEmployees.Dtos.Leaverequest;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.LeaveRequest
{
    public class UpdateLeaveRequestProfile : Profile
    {
        public UpdateLeaveRequestProfile()
        {
            CreateMap<UpdateLeaveRequest, Leaverequest>()
                .ForMember(dest => dest.Startdate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Employeeid, opt => opt.MapFrom(src => src.Employeeid))
                .ForMember(dest => dest.Enddate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Statusleaverequestid, opt => opt.MapFrom(src => src.StatusLeaveRequestId));
        }
    }
}
