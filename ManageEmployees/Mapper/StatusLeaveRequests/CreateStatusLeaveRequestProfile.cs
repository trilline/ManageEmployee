using AutoMapper;
using ManageEmployees.Dtos.Statusleaverequest;
using ManageEmployees.Entities;

namespace ManageEmployees.Mapper.StatusLeaveRequests
{
    public class CreateStatusLeaveRequestProfile : Profile
    {
        public CreateStatusLeaveRequestProfile()
        {
            CreateMap<CreateStatusLeaveRequest, Statusleaverequest>();
        }
    }
}
