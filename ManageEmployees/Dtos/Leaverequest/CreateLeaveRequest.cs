namespace ManageEmployees.Dtos.Leaverequest
{
    public class CreateLeaveRequest
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusLeaveRequestId { get; set; }
    }
}
