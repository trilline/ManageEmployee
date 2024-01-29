using AutoMapper;
using ManageEmployees.Dtos.Leaverequest;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.LeaveRequest
{
    public class CreateLeaveRequestProfile : Profile
    {
        public CreateLeaveRequestProfile()
        {
            CreateMap<CreateLeaveRequest, Leaverequest>()
                .ForMember(dest => dest.Employeeid, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.Startdate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Enddate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Statusleaverequestid, opt => opt.MapFrom(src => src.StatusLeaveRequestId));
        }
    }
}
