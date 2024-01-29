using System;
using System.Collections.Generic;

namespace ManageEmployees.Entities;

public partial class Employee
{
    public int Employeeid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public string Email { get; set; } = null!;

    public string? Phonenumber { get; set; }

    public string? Position { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Leaverequest> Leaverequests { get; set; } = new List<Leaverequest>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
