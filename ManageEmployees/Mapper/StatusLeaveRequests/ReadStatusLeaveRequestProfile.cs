using AutoMapper;
using ManageEmployees.Dtos.Statusleaverequest;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.StatusLeaveRequests
{
    public class ReadStatusLeaveRequestProfile : Profile
    {
        public ReadStatusLeaveRequestProfile()
        {
            CreateMap<Statusleaverequest, ReadStatusLeaveRequest>();
        }
    }
}
