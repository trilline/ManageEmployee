using AutoMapper;
using ManageEmployees.Dtos.Statusleaverequest;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.StatusLeaveRequests
{
    public class UpdateStatusLeaveRequestProfile : Profile
    {
        public UpdateStatusLeaveRequestProfile()
        {
            CreateMap<UpdateStatusLeaveRequest, Statusleaverequest>()
                .ForMember(dest => dest.Statuslabel, opt => opt.MapFrom(src => src.Statuslabel));
        }
    }
}
