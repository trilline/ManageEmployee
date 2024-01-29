namespace ManageEmployees.Dtos.Leaverequest
{
    public class UpdateLeaveRequest
    {
        public int Employeeid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusLeaveRequestId { get; set; }
    }
}
