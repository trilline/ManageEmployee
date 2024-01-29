namespace ManageEmployees.Dtos.Attendance
{
    public class UpdateAttendance
    {
        public int EmployeeId { get; set; }
        public DateTime Arrivaldate { get; set; }
        public DateTime? Departuredate { get; set; }
    }
}
