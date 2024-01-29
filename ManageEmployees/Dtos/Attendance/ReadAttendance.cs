namespace ManageEmployees.Dtos.Attendance
{
    public class ReadAttendance
    {
        public int Attendanceid { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Arrivaldate { get; set; }
        public DateTime? Departuredate { get; set; }
    }
}
