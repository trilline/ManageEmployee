namespace ManageEmployees.Dtos.Employee
{
    public class UpdateEmployee
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly? Birthday { get; set; }
        public string Email { get; set; }
        public string? Phonenumber { get; set; }
        public string? Position { get; set; }
    }
}
