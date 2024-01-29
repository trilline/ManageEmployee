namespace ManageEmployees.Dtos.Leaverequest
{
    public class ReadLeaveRequest
    {
        public int LeaveRequestId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestDate { get; set; }
        public int StatusLeaveRequestId { get; set; }
        public string StatusLeaveRequestLabel { get; set; }
    }
}
